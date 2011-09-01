using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession {

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
            player.AccountUUID = req.fAcctGuid;
            player.AvatarShape = req.fShape;
            player.CreatorUUID = req.fAcctGuid;
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
            info.CreatorUUID = req.fAcctGuid;
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
    }
}
