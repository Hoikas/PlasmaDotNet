using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnAuthSession {
        private void IFetchNode() {
            pnCli2Auth_VaultNodeFetch req = new pnCli2Auth_VaultNodeFetch();
            req.Read(fStream);

            // We'll inspect the node for perms once it is fetched
            fVaultCli.FetchNode(req.fNodeID, new pnCallback(new pnVaultNodeFetched(IOnNodeFetched), req.fTransID));
        }

        private void IFetchNodeRefs() {
            pnCli2Auth_VaultFetchNodeRefs req = new pnCli2Auth_VaultFetchNodeRefs();
            req.Read(fStream);

            if (fPlayerID == 0) {
                // Can't fetch NodeRefs if we're not logged in...
                pnAuth2Cli_VaultNodeRefsFetched reply = new pnAuth2Cli_VaultNodeRefsFetched();
                reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                reply.fTransID = req.fTransID;
                reply.Send(fStream);
            } else if (fPermissions >= pnAcctPerms.CCR) {
                // If we're a CCR+, then we can grab any tree we want.
                fVaultCli.FetchNodeRefs(req.fNodeID, new pnCallback(new pnVaultNodeRefsFetched(IOnNodeRefsFetched), req.fTransID));
            } else {
                // For everyone else, we need to inspect the parent node for security
                fVaultCli.FetchNode(req.fNodeID, new pnCallback(new pnVaultNodeFetched(ITreeFetchRequest), req.fTransID));
            }
        }

        private void IFindNode() {
            pnCli2Auth_VaultNodeFind req = new pnCli2Auth_VaultNodeFind();
            req.Read(fStream);

            // Why find a node if you know the NodeID?
            bool allowed = (req.fPattern[pnVaultNodeFields.NodeIdx] == null);

            // Don't allow regular users to search for important nodes...
            if (fPermissions < pnAcctPerms.CCR && allowed) {
                switch (req.fPattern.NodeType) {
                    case ENodeType.kNodeAdmin:
                    case ENodeType.kNodeCCR:
                    case ENodeType.kNodeGameServer:
                    case ENodeType.kNodeInvalid:
                    case ENodeType.kNodePlayer:
                    case ENodeType.kNodeSDL: // Easy way to find AgeGlobalSDLs
                    case ENodeType.kNodeUNUSED:
                    case ENodeType.kNodeVaultServer:
                    case ENodeType.kNodeVNodeMgrHigh:
                    case ENodeType.kNodeVNodeMgrLow:
                        allowed = false;
                        break;
                    case ENodeType.kNodeAgeInfoList:
                        pnVaultAgeInfoListNode ages = new pnVaultAgeInfoListNode(req.fPattern);
                        if (ages.FolderType == EStandardNode.kPublicAgesFolder) // Currently unused
                            allowed = false;
                        break;
                    case ENodeType.kNodeFolder:
                        // Yay, moar switching
                        pnVaultFolderNode folder = new pnVaultFolderNode(req.fPattern);
                        switch (folder.FolderType) {
                            case EStandardNode.kAllAgeGlobalSDLNodesFolder:
                            case EStandardNode.kGameScoresFolder: // Currently unused
                            case EStandardNode.kVaultMgrGlobalDataFolder:
                                allowed = false;
                                break;
                        }
                        break;
                    case ENodeType.kNodePlayerInfoList:
                        pnVaultPlayerInfoListNode players = new pnVaultPlayerInfoListNode(req.fPattern);
                        switch (players.FolderType) {
                            case EStandardNode.kAllPlayersFolder:
                            case EStandardNode.kCCRPlayersFolder: // Currently unused
                                allowed = false;
                                break;
                        }
                        break;
                }
            }

            if (allowed)
                // FIXME: VaultCli hangs after the callack is fired.
                fVaultCli.FindNode(req.fPattern, new pnCallback(new pnVaultNodeFound(IOnNodeFound), req.fTransID));
            else {
                pnAuth2Cli_VaultNodeFindReply reply = new pnAuth2Cli_VaultNodeFindReply();
                reply.fResult = ENetError.kNetErrInvalidParameter;
                reply.fTransID = req.fTransID;
                reply.Send(fStream);
            }
        }

        private void IOnNodeFetched(ENetError result, pnVaultNode node, object param) {
            pnAuth2Cli_VaultNodeFetched reply = new pnAuth2Cli_VaultNodeFetched();
            reply.fTransID = (uint)param;

            if (result == ENetError.kNetSuccess && 
                node != null && fPermissions < pnAcctPerms.CCR) {
                switch (node.NodeType) {
                    case ENodeType.kNodeAdmin:
                    case ENodeType.kNodeCCR:
                    case ENodeType.kNodeGameServer:
                    case ENodeType.kNodeInvalid:
                    case ENodeType.kNodeUNUSED:
                    case ENodeType.kNodeVaultServer:
                    case ENodeType.kNodeVNodeMgrHigh:
                    case ENodeType.kNodeVNodeMgrLow:
                        reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                        break;
                    case ENodeType.kNodeAgeInfoList:
                        pnVaultAgeInfoListNode ages = new pnVaultAgeInfoListNode(node);
                        if (ages.FolderType == EStandardNode.kPublicAgesFolder)
                            reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                        break;
                    case ENodeType.kNodeFolder:
                        // Yay, moar switching
                        pnVaultFolderNode folder = new pnVaultFolderNode(node);
                        switch (folder.FolderType) {
                            case EStandardNode.kAllAgeGlobalSDLNodesFolder:
                            case EStandardNode.kGameScoresFolder: // Currently unused
                            case EStandardNode.kVaultMgrGlobalDataFolder:
                                reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                                break;
                        }
                        break;
                    case ENodeType.kNodePlayer:
                        if (fPlayerID != node.CreatorID)
                            reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                        break;
                    case ENodeType.kNodePlayerInfoList:
                        pnVaultPlayerInfoListNode players = new pnVaultPlayerInfoListNode(node);
                        switch (players.FolderType) {
                            case EStandardNode.kAllPlayersFolder:
                            case EStandardNode.kCCRPlayersFolder: // Currently unused
                                reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                                break;
                        }
                        break;
                    case ENodeType.kNodeSDL:
                        pnVaultSDLNode sdl = new pnVaultSDLNode(node);
                        if (sdl.StateIdent != EStandardNode.kAgeInstanceSDLNode)
                            reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                        break;
                    default:
                        reply.fResult = result;
                        break;
                }
            } else
                reply.fResult = result;

            if (result == ENetError.kNetSuccess)
                reply.fNode = node;
            reply.Send(fStream);
        }

        private void IOnNodeFound(ENetError result, uint[] nodeIDs, object param) {
            pnAuth2Cli_VaultNodeFindReply reply = new pnAuth2Cli_VaultNodeFindReply();
            reply.fNodeIDs = nodeIDs;
            reply.fResult = result;
            reply.fTransID = (uint)param;
            lock (fStream) reply.Send(fStream);
        }

        private void IOnNodeRefsFetched(ENetError result, pnVaultNodeRef[] refs, object param) {
            pnAuth2Cli_VaultNodeRefsFetched reply = new pnAuth2Cli_VaultNodeRefsFetched();
            reply.fNodeRefs = refs;
            reply.fResult = result;
            reply.fTransID = (uint)param;
            lock (fStream) reply.Send(fStream);
        }

        private void ITreeFetchRequest(ENetError result, pnVaultNode node, object param) {
            ENetError decision = ENetError.kNetErrVaultNodeAccessViolation;
            if (result != ENetError.kNetSuccess)
                // If something bad happens vault side, we cannot proceed :(
                decision = result;
            else if (node == null)
                // Successful node fetch but node is null? O.o
                result = ENetError.kNetErrInternalError;
            else if (node.NodeType == ENodeType.kNodeAge || node.NodeType == ENodeType.kNodeAgeInfo)
                // We will fetch the tree of ages when we link to them
                decision = ENetError.kNetSuccess;
            else if (node.NodeType == ENodeType.kNodePlayer)
                // We can only fetch our player
                if (fPlayerID == node.NodeID)
                    decision = ENetError.kNetSuccess;

            if (decision == ENetError.kNetSuccess)
                fVaultCli.FetchNodeRefs(node.NodeID, new pnCallback(new pnVaultNodeRefsFetched(IOnNodeRefsFetched), param));
            else {
                pnAuth2Cli_VaultNodeRefsFetched reply = new pnAuth2Cli_VaultNodeRefsFetched();
                reply.fResult = decision;
                reply.fTransID = (uint)param;
                lock (fStream) reply.Send(fStream);
            }
        }
    }
}
