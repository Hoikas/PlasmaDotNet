using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnVaultServer {

        List<pnVaultSession> fSessions = new List<pnVaultSession>();
        plDebugLog fLog = plDebugLog.GetLog("VaultSrv");

        public void CheckDb() {
            throw new NotImplementedException();
        }

        public void RemoveClient(pnVaultSession cli) {
            throw new NotImplementedException();
        }
    }
}
