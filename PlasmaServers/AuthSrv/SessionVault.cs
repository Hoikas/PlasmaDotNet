using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnAuthSession {
        private void IFetchNodeRefs() {
            pnCli2Auth_VaultFetchNodeRefs req = new pnCli2Auth_VaultFetchNodeRefs();
            req.Read(fStream);

            // Can't fetch NodeRefs if we're not logged in...
            if (fPlayerID == 0) {
                pnAuth2Cli_VaultNodeRefsFetched reply = new pnAuth2Cli_VaultNodeRefsFetched();
                reply.fResult = ENetError.kNetErrVaultNodeAccessViolation;
                reply.fTransID = req.fTransID;
                reply.Send(fStream);
            } else
                fVaultCli.FetchNodeRefs(req.fNodeID, new pnCallback(new pnVaultNodeRefsFetched(IOnNodeRefsFetched), req.fTransID));
        }

        private void IOnNodeRefsFetched(ENetError result, pnVaultNodeRef[] refs, object param) {
            pnAuth2Cli_VaultNodeRefsFetched reply = new pnAuth2Cli_VaultNodeRefsFetched();
            reply.fNodeRefs = refs;
            reply.fResult = result;
            reply.fTransID = Convert.ToUInt32(param);
            lock (fStream) reply.Send(fStream);
        }
    }
}
