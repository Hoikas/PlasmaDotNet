using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnAcctPerms {
        Banned = -1,
        Explorer = 0,
        Privledged, // Will allow player to login when logins are restricted
        CCR,        // Player is exempt from anti-cheat detection
        SuperAdmin, // Future...
    }

    public partial class pnAuthSession {

        Guid fAcctGuid;
        uint fPlayerIdx;
        uint fChallenge;
        pnAcctPerms fPermissions;

        private void ICreatePlayer() {
            pnCli2Auth_PlayerCreateRequest req = new pnCli2Auth_PlayerCreateRequest();
            req.Read(fStream);
            ENetError status = ENetError.kNetPending;

            // An empty Guid signifies we haven't logged in
            if (fAcctGuid == Guid.Empty)
                status = ENetError.kNetErrAuthenticationFailed;

            // Only CCR+ may create special avatars
            if (fPermissions < pnAcctPerms.CCR) {
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

            if (fAcctGuid == Guid.Empty) {
                // This is the first time we need the vault connection,
                // so let's establish it here.
                if (IConnectToVault()) {
                    fVaultCli.AcctLogin(req.fAccount, req.fHash, req.fChallenge, fChallenge,
                        new pnCallback(new pnVaultAcctLoggedIn(IOnAcctLoggedIn), req.fTransID));
                } else {
                    pnAuth2Cli_AcctLoginReply reply = new pnAuth2Cli_AcctLoginReply();
                    reply.fResult = ENetError.kNetErrInternalError;
                    reply.fTransID = req.fTransID;
                    reply.Send(fStream);
                }
            } else {
                // A special kind of stupid... time to get what you deserve.
                KickOff(ENetError.kNetErrDisconnected);
            }
        }

        private void IOnAcctLoggedIn(ENetError result, Guid guid, int perms, pnVaultAvatarInfo[] avatars, object param) {
            if (result == ENetError.kNetSuccess) {
                fAcctGuid = guid;
                fPermissions = (pnAcctPerms)perms;
            }

            List<plNetStruct> toSend = new List<plNetStruct>(6);
            if (avatars != null) {
                foreach (pnVaultAvatarInfo info in avatars) {
                    pnAuth2Cli_AcctPlayerInfo player = new pnAuth2Cli_AcctPlayerInfo();
                    player.fExplorer = 1; // HACK--always a "paying customer"
                    player.fModel = info.fModel;
                    player.fPlayerID = info.fPlayerID;
                    player.fPlayerName = info.fPlayerName;
                    player.fTransID = Convert.ToUInt32(param);
                    toSend.Add(player);
                }
            }

            pnAuth2Cli_AcctLoginReply reply = new pnAuth2Cli_AcctLoginReply();
            reply.fAcctGuid = guid;
            reply.fBillingType = 1; // HACK--always a "paying customer"
            reply.fDroidKey = null; // FIXME
            reply.fResult = result;
            reply.fTransID = Convert.ToUInt32(param);
            toSend.Add(reply);

            // Only acquire this lock once :)
            lock (fStream)
                foreach (plNetStruct ns in toSend)
                    ns.Send(fStream);
        }

        private void IOnPlayerCreated(ENetError result, uint playerID, string playerName, string shape, object param) {
            pnAuth2Cli_PlayerCreateReply reply = new pnAuth2Cli_PlayerCreateReply();
            reply.fPlayerID = playerID;
            reply.fPlayerName = playerName;
            reply.fResult = result;
            reply.fShape = shape;
            reply.fTransID = (uint)param;
            fPlayerIdx = playerID; // Cyan is hacking...
            lock (fStream) reply.Send(fStream);
        }

        private void IOnPlayerSet(ENetError result, object param) {
            pnAuth2Cli_AcctSetPlayerReply reply = new pnAuth2Cli_AcctSetPlayerReply();
            reply.fResult = result;
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

            if (fAcctGuid == Guid.Empty) {
                pnAuth2Cli_AcctSetPlayerReply reply = new pnAuth2Cli_AcctSetPlayerReply();
                reply.fResult = ENetError.kNetErrPlayerNotFound;
                reply.fTransID = req.fTransID;
                reply.Send(fStream);
            } else {
                fVaultCli.SetPlayer(req.fPlayerID, fAcctGuid,
                    new pnCallback(new pnVaultPlayerSet(IOnPlayerSet), req.fTransID));
            }
        }
    }
}
