using System;
using Plasma;

namespace VaultMangler {
    public class ServerIni {

        pnIniParser fIni;
        string fName;

        private ServerIni(pnIniParser ini) { fIni = ini; }
        public static ServerIni CreateFromFile(string inifile) {
            ServerIni ini = new ServerIni(new pnIniParser(inifile));

            // Make sure this ini parser has everything we need
            if (ini.AuthN == null)
                return null;
            if (ini.AuthX == null)
                return null;
            if (ini.GateN == null)
                return null;
            if (ini.GateX == null)
                return null;
            if (ini.GateHost == null)
                return null;

            // ToString'izing
            string shard = ini.fIni["Server.DispName"];
            if (shard == null)
                shard = inifile;
            ini.fName = shard;

            // Done!
            return ini;
        }

        public override string ToString() {
            return fName;
        }

        public string AuthHost {
            get { return fIni["Server.Auth.Host"]; }
        }

        public int AuthG {
            get {
                int? g = fIni.GetInteger("Server.Auth.G");
                if (g.HasValue)
                    return g.Value;
                else
                    return 41;
            }
        }

        public string AuthN {
            get { return fIni["Server.Auth.N"]; }
        }

        public string AuthX {
            get { return fIni["Server.Auth.X"]; }
        }

        public string GateHost {
            get { return fIni["Server.Gate.Host"]; }
        }

        public int GateG {
            get {
                int? g = fIni.GetInteger("Server.Gate.G");
                if (g.HasValue)
                    return g.Value;
                else
                    return 4;
            }
        }

        public string GateN {
            get { return fIni["Server.Gate.N"]; }
        }

        public string GateX {
            get { return fIni["Server.Gate.X"]; }
        }

        public Guid ProductID {
            get { return fIni.GetGuid("Client.ProductID", new Guid("ea489821-6c35-4bd0-9dae-bb17c585e680")); }
        }

        public int Port {
            get {
                int? port = fIni.GetInteger("Server.Port");
                if (port.HasValue)
                    return port.Value;
                else
                    return 14617;
            }
        }

        public int? BuildId {
            get { return fIni.GetInteger("Server.BuildId"); }
        }
    }
}
