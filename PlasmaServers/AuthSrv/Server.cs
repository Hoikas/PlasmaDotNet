using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma{
    public class pnAuthServer {

        Dictionary<uint, pnAuthSession> fLookup = new Dictionary<uint, pnAuthSession>();
        List<pnAuthSession> fSessions = new List<pnAuthSession>();
        plDebugLog fLog = plDebugLog.GetLog("AuthSrv");

        public void CheckDb() {
            IDbConnection db = pnDatabase.Connect();

            // Try to insert the Accounts table
            pnSqlCreateTable acct = new pnSqlCreateTable();
            acct.AddColumn("Idx", typeof(uint), pnColumnOption.AutoIncrement);
            acct.AddColumn("Username", typeof(string), pnColumnOption.VariableSize);
            acct.AddColumn("Password", typeof(OpenSSL.Hash));
            acct.AddColumn("Permissions", typeof(int)); // I wanted to use SByte, but that causes an InvalidCastException...
            acct.AddColumn("Guid", typeof(Guid));
            acct.AddKey("Guid");
            acct.Name = "Accounts";
            acct.PrimaryKey = "Idx";
            acct.Execute(db);

            // Now verify the Players table
            pnSqlCreateTable player = new pnSqlCreateTable();
            player.AddColumn("Idx", typeof(uint), pnColumnOption.AutoIncrement);
            player.AddColumn("AcctIdx", typeof(uint));
            player.AddColumn("PlayerIdx", typeof(uint));
            player.AddColumn("Name", typeof(string), pnColumnOption.VariableSize);
            player.AddColumn("Model", typeof(string), pnColumnOption.VariableSize);
            player.AddKey("AcctIdx");
            player.AddKey("PlayerIdx");
            player.Name = "Players";
            player.PrimaryKey = "Idx";
            player.Execute(db);

            // Clean up :)
            db.Close();
        }

        public void EatClient(Socket cli, pnCli2Srv_Connect hdr) {
            pnAuthSession ac = new pnAuthSession(this, cli, hdr);
            if (!ac.Initialize()) {
                cli.Close();
                return;
            }

            lock (fSessions)
                fSessions.Add(ac);
        }

        public void RemoveClient(pnAuthSession session) {
            lock (fSessions)
                fSessions.Remove(session);
        }
    }
}
