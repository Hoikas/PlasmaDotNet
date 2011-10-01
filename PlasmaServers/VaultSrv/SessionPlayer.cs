using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession {

        private void IAcctLogin() {
            pnCli2Vault_AcctLoginRequest req = new pnCli2Vault_AcctLoginRequest();
            req.Read(fStream);

            pnVault2Cli_AcctLoginReply reply = new pnVault2Cli_AcctLoginReply();
            reply.fTransID = req.fTransID;
            reply.fResult = ENetError.kNetSuccess;

            try {
                pnSqlSelectStatement acct = new pnSqlSelectStatement();
                acct.AddColumn("Idx");
                acct.AddColumn("Password");
                acct.AddColumn("Permissions");
                acct.AddColumn("Guid");
                acct.AddWhere("Username", req.fAccount);
                acct.Limit = 1;
                acct.Table = "Accounts";
                IDataReader r = acct.Execute(fDb);

                uint? acctID = new uint?();
                if (r.Read()) {
                    // eap has made this password thing difficult for us...
                    // Usernames that are email addresses do some strange SHA-0 stuff,
                    // but normal usernames are just a SHA-1 hash. Lawd help us.
                    byte[] gPass = pnHelpers.GetBytes(r["Password"].ToString());
                    if (req.fAccount.Contains('@'))
                        gPass = pnHelpers.HashLogin(gPass, req.fCliChg, req.fSrvChg);

                    // ... Nice, Microsoft. Neither the == operator nor the Equals method
                    // actually tests the values >.<
                    if (gPass.SequenceEqual(req.fHash)) {
                        acctID = (uint)r["Idx"];
                        reply.fAcctGuid = new Guid(r["Guid"].ToString());
                        reply.fPermissions = (int)r["Permissions"];
                        if (reply.fPermissions == (int)pnAcctPerms.Banned)
                            reply.fResult = ENetError.kNetErrAccountBanned;
                    } else
                        reply.fResult = ENetError.kNetErrAuthenticationFailed;
                } else
                    // I realize there is an "Account Not Found" Error, but that's
                    // kind of a security hole.
                    reply.fResult = ENetError.kNetErrAuthenticationFailed;
                r.Close();

                // Now grab the avatars
                if (acctID.HasValue) {
                    pnSqlSelectStatement avatars = new pnSqlSelectStatement();
                    avatars.AddColumn("Model");
                    avatars.AddColumn("Name");
                    avatars.AddColumn("PlayerIdx");
                    avatars.AddWhere("AcctIdx", acctID.Value.ToString());
                    avatars.Limit = 5;
                    avatars.Table = "Players";
                    r = avatars.Execute(fDb);

                    List<pnVaultAvatarInfo> players = new List<pnVaultAvatarInfo>();
                    while (r.Read()) {
                        pnVaultAvatarInfo info = new pnVaultAvatarInfo();
                        info.fModel = r["Model"].ToString();
                        info.fPlayerID = (uint)r["PlayerIdx"];
                        info.fPlayerName = r["Name"].ToString();
                        players.Add(info);
                    }

                    reply.fAvatars = players.ToArray();
                    r.Close();
                }
            } catch (pnDbException e) {
                Error(e, "Database Error on Login");
                reply.fResult = ENetError.kNetErrInternalError;
            }

            reply.Send(fStream);
        }

        private void ICreatePlayer() {
            pnCli2Vault_PlayerCreateRequest req = new pnCli2Vault_PlayerCreateRequest();
            req.Read(fStream);
            Debug(String.Format("Creating a Player: [N: {0}] [S: {1}]", req.fPlayerName, req.fShape));

            // Go ahead and init the response
            pnVault2Cli_PlayerCreateReply reply = new pnVault2Cli_PlayerCreateReply();
            reply.fTransID = req.fTransID;
            reply.fResult = ENetError.kNetSuccess;
            reply.fPlayerName = req.fPlayerName;
            reply.fShape = req.fShape;

            // Search for a matching PlayerInfo
            // We check the entire vault because we want to include deleted players as well.
            pnVaultPlayerInfoNode pi = new pnVaultPlayerInfoNode();
            pi.PlayerName = req.fPlayerName;
            try {
                if (IFindNode(pi.BaseNode).Length != 0) {
                    reply.fResult = ENetError.kNetErrPlayerAlreadyExists;
                    reply.Send(fStream);
                    return;
                }
            } catch (pnDbException e) {
                Error(e, "Create Player: FindNode Failed");
                reply.fResult = ENetError.kNetErrInternalError;
                reply.Send(fStream);
                return;
            }

            // Prepare the core node
            pnVaultPlayerNode player = new pnVaultPlayerNode();
            player.AccountUuid = req.fAcctGuid;
            player.AvatarShape = req.fShape;
            player.CreatorUuid = req.fAcctGuid;
            player.PlayerName = req.fPlayerName;

            // Go ahead and insert this node.
            try {
                ICreateNode(player.BaseNode);
                reply.fPlayerID = player.NodeID;
            } catch (pnDbException e) {
                Error(e, "Create Player: Couldn't create the PlayerNode");
                reply.fResult = ENetError.kNetErrInternalError;
                reply.Send(fStream);
                return;
            }

            // Player Info
            pnVaultPlayerInfoNode info = new pnVaultPlayerInfoNode();
            info.CreatorID = player.NodeID;
            info.CreatorUuid = req.fAcctGuid;
            info.PlayerID = player.NodeID;
            info.PlayerName = req.fPlayerName;

            try {
                ICreateNode(info.BaseNode);
            } catch (pnDbException e) {
                Error(e, "Create Player: Couldn't create the PlayerInfoNode");
                reply.fResult = ENetError.kNetErrInternalError;
                reply.Send(fStream);
                return;
            }

            // SimpleStdNodes
            uint? agejourn = ICreateStdNode(EStandardNode.kAgeJournalsFolder, player.NodeID, req.fAcctGuid);
            uint? agesicanvisit = ICreateStdNode(EStandardNode.kAgesICanVisitFolder, player.NodeID, req.fAcctGuid);
            uint? agesiown = ICreateStdNode(EStandardNode.kAgesIOwnFolder, player.NodeID, req.fAcctGuid);
            uint? avcloset = ICreateStdNode(EStandardNode.kAvatarClosetFolder, player.NodeID, req.fAcctGuid);
            uint? avoutfit = ICreateStdNode(EStandardNode.kAvatarOutfitFolder, player.NodeID, req.fAcctGuid);
            uint? buddies = ICreateStdNode(EStandardNode.kBuddyListFolder, player.NodeID, req.fAcctGuid);
            uint? chrons = ICreateStdNode(EStandardNode.kChronicleFolder, player.NodeID, req.fAcctGuid);
            uint? ignore = ICreateStdNode(EStandardNode.kIgnoreListFolder, player.NodeID, req.fAcctGuid);
            uint? inbox = ICreateStdNode(EStandardNode.kInboxFolder, player.NodeID, req.fAcctGuid);
            uint? recent = ICreateStdNode(EStandardNode.kPeopleIKnowAboutFolder, player.NodeID, req.fAcctGuid);

            // Create the tree
            ICreateRelationship(player.NodeID, info.NodeID, player.NodeID);
            if (agejourn.HasValue)
                ICreateRelationship(player.NodeID, agejourn.Value, player.NodeID);
            if (agesicanvisit.HasValue)
                ICreateRelationship(player.NodeID, agesicanvisit.Value, player.NodeID);
            if (agesiown.HasValue)
                ICreateRelationship(player.NodeID, agesiown.Value, player.NodeID);
            if (avcloset.HasValue)
                ICreateRelationship(player.NodeID, avcloset.Value, player.NodeID);
            if (avoutfit.HasValue)
                ICreateRelationship(player.NodeID, avoutfit.Value, player.NodeID);
            if (buddies.HasValue)
                ICreateRelationship(player.NodeID, buddies.Value, player.NodeID);
            if (chrons.HasValue)
                ICreateRelationship(player.NodeID, chrons.Value, player.NodeID);
            if (ignore.HasValue)
                ICreateRelationship(player.NodeID, ignore.Value, player.NodeID);
            if (recent.HasValue)
                ICreateRelationship(player.NodeID, recent.Value, player.NodeID);

            // Link to the System Node
            pnVaultSystemNode sys = new pnVaultSystemNode();
            uint[] systems = IFindNode(sys.BaseNode);
            if (systems.Length == 0)
                Warn("Hmmm... Where's the system node?");
            else {
                if (systems.Length > 1)
                    Warn("Hmmm... We have multiple system nodes. How did that happen?");
                ICreateRelationship(player.NodeID, systems[0], player.NodeID);
            }

            // Add to the players table...
            // First, we need to figure out what our account ID is
            pnSqlSelectStatement selAcctId = new pnSqlSelectStatement();
            selAcctId.AddColumn("Idx");
            selAcctId.AddWhere("Guid", req.fAcctGuid.ToString());
            selAcctId.Limit = 1;
            selAcctId.Table = "Accounts";
            try {
                IDataReader rsaid = selAcctId.Execute(fDb);
                if (rsaid.Read()) {
                    // Now, actually insert
                    uint acctIdx = Convert.ToUInt32(rsaid[0]);
                    rsaid.Close();

                    pnSqlInsertStatement insPlayer = new pnSqlInsertStatement();
                    insPlayer.AddValue("AcctIdx", acctIdx);
                    insPlayer.AddValue("PlayerIdx", player.NodeID);
                    insPlayer.AddValue("Name", req.fPlayerName);
                    insPlayer.AddValue("Model", req.fShape);
                    insPlayer.Table = "Players";
                    insPlayer.Execute(fDb);
                } else {
                    Warn(String.Format("HACK??? New player [#{0}] created for AcctGuid [{1}] not in our Accounts",
                        player.NodeID, req.fAcctGuid));
                    rsaid.Close();
                }
            } catch (pnDbException e) {
                Error(e, "Failed to add to Players table");
            } catch (Exception e) {
                Error(e, "Hmmm...");
            }

            // TODO: Add to AllPlayers Folder
            //       Implement me when we can notify clients about node adds.

            // TODO: Initialize core ages
            // Neighborhood, Ae'gura

            // Send the response :)
            reply.Send(fStream);
        }

        private void ISetPlayer() {
            pnCli2Vault_PlayerSetRequest req = new pnCli2Vault_PlayerSetRequest();
            req.Read(fStream);

            pnVault2Cli_PlayerSetReply reply = new pnVault2Cli_PlayerSetReply();
            reply.fTransID = req.fTransID;

            // Make sure that player is on this account
            try {
                pnSqlSelectStatement selAcctIdx = new pnSqlSelectStatement();
                selAcctIdx.AddColumn("Idx");
                selAcctIdx.AddColumn("Permissions");
                selAcctIdx.AddWhere("Guid", req.fAcctGuid);
                selAcctIdx.Table = "Accounts";

                IDataReader rAcctIdx = selAcctIdx.Execute(fDb);
                if (rAcctIdx.Read()) {
                    uint acctIdx = Convert.ToUInt32(rAcctIdx["Idx"]);
                    int perms = Convert.ToInt32(rAcctIdx["Permissions"]);
                    rAcctIdx.Close();

                    pnSqlSelectStatement selPlayer = new pnSqlSelectStatement();
                    selPlayer.AddColumn("COUNT(*)");
                    selPlayer.AddWhere("AcctIdx", acctIdx);
                    selPlayer.Table = "Players"; ;

                    IDataReader rPlayer = selPlayer.Execute(fDb);
                    if (rPlayer.Read())
                        reply.fResult = ENetError.kNetSuccess;
                    else 
                        reply.fResult = ENetError.kNetErrPlayerNotFound;
                    rPlayer.Close();
                } else {
                    reply.fResult = ENetError.kNetErrPlayerNotFound;
                    rAcctIdx.Close();
                }
            } catch (pnDbException e) {
                reply.fResult = ENetError.kNetErrInternalError;
                Error(e, "SetActivePlayer Failed");
            }

            reply.Send(fStream);
        }
    }
}
