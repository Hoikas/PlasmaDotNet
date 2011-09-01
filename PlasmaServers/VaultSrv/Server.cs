using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession {
        public void PopulateVault() {
            // See if we actually need to do anything...
            pnVaultSystemNode system = new pnVaultSystemNode();
            if (IFindNode(system.BaseNode).Length != 0)
                return;

            Debug("Populating the VaultDB...");

            // Core Nodes
            uint? sys = ICreateStdNode(EStandardNode.kSystemNode);
            uint? gbox = ICreateStdNode(EStandardNode.kGlobalInboxFolder);
            uint? allp = ICreateStdNode(EStandardNode.kAllPlayersFolder);
            uint? gsdl = ICreateStdNode(EStandardNode.kAllAgeGlobalSDLNodesFolder); // The only time I won't kill you.

            if (sys.HasValue && gbox.HasValue)
                ICreateRelationship(sys.Value, gbox.Value, 0);

            // TODO: Core Ages...
            // Ae'gura, Guild Pubs, Kirel, K'veer, Phil's Relto
        }
    }

    public class pnVaultServer {

        List<pnVaultSession> fSessions = new List<pnVaultSession>();
        plDebugLog fLog = plDebugLog.GetLog("VaultSrv");
        bool fPopulateTaskDone = false;

        public void CheckDb() {
            IDbConnection db = pnDatabase.Connect();

            // Try to insert the Nodes table...
            pnSqlCreateTable nodes = new pnSqlCreateTable();
            nodes.AddColumn("Idx", typeof(uint), pnColumnOption.AutoIncrement);
            nodes.AddColumn("CreateTime", typeof(DateTime));
            nodes.AddColumn("ModifyTime", typeof(DateTime));
            nodes.AddColumn("CreateAgeName", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("CreateAgeUuid", typeof(Guid));
            nodes.AddColumn("CreatorIdx", typeof(uint));
            nodes.AddColumn("CreatorUuid", typeof(Guid));
            nodes.AddColumn("NodeType", typeof(uint));
            nodes.AddColumn("Int32_1", typeof(int), pnColumnOption.Nullable);
            nodes.AddColumn("Int32_2", typeof(int), pnColumnOption.Nullable);
            nodes.AddColumn("Int32_3", typeof(int), pnColumnOption.Nullable);
            nodes.AddColumn("Int32_4", typeof(int), pnColumnOption.Nullable);
            nodes.AddColumn("UInt32_1", typeof(uint), pnColumnOption.Nullable);
            nodes.AddColumn("UInt32_2", typeof(uint), pnColumnOption.Nullable);
            nodes.AddColumn("UInt32_3", typeof(uint), pnColumnOption.Nullable);
            nodes.AddColumn("UInt32_4", typeof(uint), pnColumnOption.Nullable);
            nodes.AddColumn("Uuid_1", typeof(Guid));
            nodes.AddColumn("Uuid_2", typeof(Guid));
            nodes.AddColumn("Uuid_3", typeof(Guid));
            nodes.AddColumn("Uuid_4", typeof(Guid));
            nodes.AddColumn("String64_1", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("String64_2", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("String64_3", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("String64_4", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("String64_5", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("String64_6", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("IString64_1", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("IString64_2", typeof(string), pnColumnOption.VariableSize);
            nodes.AddColumn("Text_1", typeof(string));
            nodes.AddColumn("Text_2", typeof(string));
            nodes.AddColumn("Blob_1", typeof(string));
            nodes.AddColumn("Blob_2", typeof(string));
            nodes.AddKey("NodeType"); // For VaultNodeFinds
            nodes.Name = "Nodes";
            nodes.PrimaryKey = "Idx";
            nodes.Execute(db);

            // Try to insert the NodeRefs table...
            pnSqlCreateTable refs = new pnSqlCreateTable();
            refs.AddColumn("Idx", typeof(uint), pnColumnOption.AutoIncrement);
            refs.AddColumn("ParentIdx", typeof(uint));
            refs.AddColumn("ChildIdx", typeof(uint));
            refs.AddColumn("SaverIdx", typeof(uint));
            refs.AddKey("ParentIdx"); // We will be selecting often by this value...
            refs.Name = "NodeRefs";
            refs.PrimaryKey = "Idx";
            refs.Execute(db);

            // We're done for now...
            // NOTE: Initial node population will happen when the first client connects!
            db.Close();
        }

        public void EatClient(Socket cli, pnCli2Srv_Connect hdr) {
            pnVaultSession vc = new pnVaultSession(this, cli, hdr);
            if (!vc.Initialize()) {
                cli.Close();
                return;
            }

            lock (fSessions) {
                fSessions.Add(vc);
                if (!fPopulateTaskDone) {
                    fPopulateTaskDone = true;
                    vc.PopulateVault();
                }
            }
        }

        public void RemoveClient(pnVaultSession cli) {
            lock (fSessions)
                fSessions.Remove(cli);
        }
    }
}
