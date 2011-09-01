using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnAuthSession {

        enum Permissions {
            Banned    = -1,
            Explorer  =  0,
            Privledged, // Will allow player to login when logins are restricted
            CCR,        // Player is exempt from anti-cheat detection
            SuperAdmin, // Future...
        }

        Guid fAcctGuid;
        uint fChallenge;
        Permissions fPermissions;

        private void ICreatePlayer() {
            pnCli2Auth_PlayerCreateRequest req = new pnCli2Auth_PlayerCreateRequest();
            req.Read(fStream);
            ENetError status = ENetError.kNetPending;

            // An empty Guid signifies we haven't logged in
            if (fAcctGuid == Guid.Empty)
                status = ENetError.kNetErrAuthenticationFailed;

            // Only CCR+ may create special avatars
            if (fPermissions < Permissions.CCR) {
                if (req.fShape.ToLower() != "male" &&
                    req.fShape.ToLower() != "female")
                    status = ENetError.kNetErrPlayerNameInvalid; // Close enough?
            }

            if (status == ENetError.kNetPending) {
                // Forward to the vault server
                // TODO: Reserved and Blacklisted player name list?
                pnCallback cb = new pnCallback(new pnVaultPlayerCreated(IOnPlayerCreated), req.fTransID);
                fVaultCli.CreatePlayer(fAcctGuid, req.fPlayerName, req.fShape, cb);
            } else {
                // I don't think so...
                pnAuth2Cli_PlayerCreateReply reply = new pnAuth2Cli_PlayerCreateReply();
                reply.fResult = status;
                reply.fTransID = req.fTransID;
                reply.Send(fStream);
            }
        }

        private void ILogin() {
            pnCli2Auth_AcctLoginRequest req = new pnCli2Auth_AcctLoginRequest();
            req.Read(fStream);

            // We absolutely must be connected to the vault server before this happens...
            fVaultCli.WaitForConnection();

            pnAuth2Cli_AcctLoginReply reply = new pnAuth2Cli_AcctLoginReply();
            reply.fResult = ENetError.kNetSuccess; // Change this if we fail somewhere...
            reply.fTransID = req.fTransID;

            List<pnAuth2Cli_AcctPlayerInfo> players = new List<pnAuth2Cli_AcctPlayerInfo>();
            try {
                // TODO: Move to vault server?
                IDbConnection db = pnDatabase.Connect();
                
                // Try to select this account...
                pnSqlSelectStatement acct = new pnSqlSelectStatement();
                acct.AddColumn("Idx");
                acct.AddColumn("Password");
                acct.AddColumn("Permissions");
                acct.AddColumn("Guid");
                acct.AddWhere("Username", req.fAccount);
                acct.Limit = 1;
                acct.Table = "Accounts";
                IDataReader r = acct.Execute(db);

                uint? acctID = new uint?();
                if (r.Read()) {
                    if (r["Password"].ToString() == pnHelpers.GetString(req.fHash)) {
                        acctID = (uint)r["Idx"];
                        reply.fAcctGuid = new Guid(r["Guid"].ToString());
                        reply.fBillingType = 1; // HACK -- Always create explorers
                        reply.fDroidKey = null; // TODO
                        fPermissions = (Permissions)((int)r["Permissions"]);
                        if (fPermissions == Permissions.Banned)
                            reply.fResult = ENetError.kNetErrAccountBanned;
                        else
                            // An empty AcctGuid signifies that we have NOT logged in
                            fAcctGuid = reply.fAcctGuid;
                    } else
                        reply.fResult = ENetError.kNetErrAuthenticationFailed;
                } else
                    // I realize there is an "Account Not Found" Error, but that's
                    // kind of a security hole.
                    reply.fResult = ENetError.kNetErrAuthenticationFailed;
                r.Close();

                // If we know the AccountID, then we have a valid player, so let's
                // prepare a list of avatars
                if (acctID.HasValue) {
                    pnSqlSelectStatement avatars = new pnSqlSelectStatement();
                    avatars.AddColumn("Model");
                    avatars.AddColumn("Name");
                    avatars.AddColumn("PlayerIdx");
                    avatars.AddWhere("AcctIdx", acctID.Value.ToString());
                    avatars.Limit = 5;
                    avatars.Table = "Players";
                    r = avatars.Execute(db);

                    while (r.Read()) {
                        pnAuth2Cli_AcctPlayerInfo info = new pnAuth2Cli_AcctPlayerInfo();
                        info.fExplorer = 1;
                        info.fModel = r["Model"].ToString();
                        info.fPlayerID = (uint)r["PlayerIdx"];
                        info.fPlayerName = r["Name"].ToString();
                        info.fTransID = req.fTransID;
                        players.Add(info);
                    }

                    r.Close();
                }

                db.Close();
            } catch (pnDbException e) {
                Error(e, "Database Error on Login");
                reply.fResult = ENetError.kNetErrInternalError;
            }

            foreach (pnAuth2Cli_AcctPlayerInfo player in players)
                player.Send(fStream);
            reply.Send(fStream);
        }

        private void IOnPlayerCreated(ENetError result, uint playerID, string playerName, string shape, object param) {
            pnAuth2Cli_PlayerCreateReply reply = new pnAuth2Cli_PlayerCreateReply();
            reply.fPlayerID = playerID;
            reply.fPlayerName = playerName;
            reply.fResult = result;
            reply.fShape = shape;
            reply.fTransID = (uint)param;
            lock (fStream) reply.Send(fStream);
        }

        private void IRegisterClient() {
            pnCli2Auth_ClientRegisterRequest req = new pnCli2Auth_ClientRegisterRequest();
            req.Read(fStream);

            // Double check the BuildID
            // This is mostly to ensure some dummy isn't trying to troll us
            // with some unproven netcode that really sucks.
            int? buildID = pngIni.Ini.GetInteger("Client.BuildID");
            if (buildID.HasValue)
                if (buildID != req.fBuildID) {
                    KickOff(ENetError.kNetErrOldBuildId);
                    return;
                }

            pnAuth2Cli_ClientRegisterReply reply = new pnAuth2Cli_ClientRegisterReply();
            reply.fChallenge = fChallenge;
            reply.Send(fStream);
        }

        private void ISetPlayer() {
            pnCli2Auth_AcctSetPlayerRequest req = new pnCli2Auth_AcctSetPlayerRequest();
            req.Read(fStream);

            // TODO
        }
    }
}
