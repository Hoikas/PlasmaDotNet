using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession {

        private void IFetchNode() {
            pnCli2Vault_NodeFetch req = new pnCli2Vault_NodeFetch();
            req.Read(fStream);

            pnVault2Cli_NodeFetched reply = new pnVault2Cli_NodeFetched();
            reply.fResult = ENetError.kNetSuccess;
            reply.fTransID = req.fTransID;

            pnSqlSelectStatement select = new pnSqlSelectStatement();
            select.AddWhere("NodeIdx", req.fNodeID);
            select.Limit = 1;
            select.Table = "Nodes";
            try {
                IDataReader r = select.Execute(fDb);
                if (r.Read())
                    reply.fNode = IMakeNode(r);
                else
                    reply.fResult = ENetError.kNetErrVaultNodeNotFound;
                r.Close();
            } catch (pnDbException e) {
                Error(e, "Failed to fetch node #" + req.fNodeID.ToString());
                reply.fResult = ENetError.kNetErrInternalError;
            }

            reply.Send(fStream);
        }

        private void IFetchNodeRefs() {
            pnCli2Vault_FetchNodeRefs req = new pnCli2Vault_FetchNodeRefs();
            req.Read(fStream);

            // Go ahead and prepare the response in case anything terrible happens
            pnVault2Cli_NodeRefsFetched reply = new pnVault2Cli_NodeRefsFetched();
            reply.fResult = ENetError.kNetSuccess;
            reply.fTransID = req.fTransID;

            // Let's spider the tree now :)
            List<uint> toFetch = new List<uint>();
            List<pnVaultNodeRef> refs = new List<pnVaultNodeRef>();
            toFetch.Add(req.fNodeID);
            for (int i = 0; i < toFetch.Count; i++) {
                pnSqlSelectStatement select = new pnSqlSelectStatement();
                select.AddColumn("ChildIdx");
                select.AddColumn("SaverIdx");
                select.AddWhere("ParentIdx", toFetch[i]);
                select.Table = "NodeRefs";

                try {
                    IDataReader r = select.Execute(fDb);
                    while (r.Read()) {
                        pnVaultNodeRef vnr = new pnVaultNodeRef(toFetch[i], (uint)r["ChildIdx"], (uint)r["SaverIdx"]);
                        if (!toFetch.Contains(vnr.fChild))
                            toFetch.Add(vnr.fChild);
                        refs.Add(vnr);
                    }

                    r.Close();
                } catch (pnDbException e) {
                    Error(e, "Failed to fetch NodeRefs");
                    reply.fResult = ENetError.kNetErrInternalError;
                }
            }

            if (refs.Count == 0)
                // The vault node may actually exist, but it certainly has no children.
                // Maybe FIXME at some point?
                reply.fResult = ENetError.kNetErrVaultNodeNotFound;
            else if (reply.fResult == ENetError.kNetSuccess)
                reply.fNodeRefs = refs.ToArray();
            reply.Send(fStream);
        }

        private void IFindNode() {
            pnCli2Vault_NodeFind req = new pnCli2Vault_NodeFind();
            req.Read(fStream);

            pnVault2Cli_NodeFindReply reply = new pnVault2Cli_NodeFindReply();
            reply.fTransID = req.fTransID;
            try {
                reply.fNodeIDs = IFindNode(req.fPattern);
                reply.fResult = ENetError.kNetSuccess;
            } catch (pnDbException e) {
                reply.fResult = ENetError.kNetErrInternalError;
                Error(e, "VaultNodeFind failed");
            }

            reply.Send(fStream);
        }
    }
}
