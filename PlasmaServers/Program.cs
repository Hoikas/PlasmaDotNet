using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using OpenSSL;

namespace Plasma {
    sealed class Program {

        #region Reflection Helpers
        static string ARevision {
            get {
                Version v = Assembly.GetExecutingAssembly().GetName().Version;
                return v.Revision.ToString();
            }
        }

        static string AVersion {
            get {
                Version v = Assembly.GetExecutingAssembly().GetName().Version;
                return String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);
            }
        }
        #endregion

        static void Main(string[] args) {
            Console.WriteLine("Plasma .NET Servers v" + AVersion);
            Console.WriteLine("Revision " + ARevision);
            Console.WriteLine();
            Console.WriteLine();

            // Check the arguments to see what we need to do...
            // If the first arg is /KeyGen, then generate some keys for the INI
            // Else, we may have /Daemon (default) with a list of servers to spawn
            if (args.Length == 0)
                IRunDaemon(new string[] { "auth", "file", "game", "gate", "lookup", "vault" });
            else if (args[0].ToLower() == "/keygen")
                IGenerateKeys();
            else
                IRunDaemon(args);
        }

        static void IGenerateKeys() {
            // Hopefully people don't decide to change G like Cyan did...
            // Because that's really not how security works...
            IGenerateTheKey("Auth", 41);
            IGenerateTheKey("Game", 73);
            IGenerateTheKey("Gate", 4);
            IGenerateTheKey("Lookup", 991);
            IGenerateTheKey("Vault", 2011);

            Console.WriteLine("You will need to copy the N and X values for the Auth, Game, and Gate servers into the server.ini file for your client.");
            Console.WriteLine("Do not share the the Lookup or Vault keys.");
            Console.WriteLine("Happy Hacking!");
        }

        static void IGenerateTheKey(string name, int g) {
            BigNum K = BigNum.GeneratePrime(512);
            BigNum N = BigNum.GeneratePrime(512);
            BigNum X = new BigNum(g).PowMod(K, N);

            // We store the keys as base64 strings in OpenSSL byte order (BE)
            string k = Convert.ToBase64String(K.ToBigArray());
            string n = Convert.ToBase64String(N.ToBigArray());
            string x = Convert.ToBase64String(X.ToBigArray());

            Console.WriteLine(String.Format("Server.{0}.K \"{1}\"", name, k));
            Console.WriteLine(String.Format("Server.{0}.N \"{1}\"", name, n));
            Console.WriteLine(String.Format("Server.{0}.X \"{1}\"", name, x));
            Console.WriteLine();
        }

        static bool ILoadConfig() {
            // Load the configuration
            if (!pngIni.Init()) {
                Console.WriteLine("ERROR: PlasmaServers.ini missing or mangled.");
                Console.WriteLine("Please verify the configuration and try again.");
                return false;
            }

            // Certain values should ALWAYS be set.
            // Let's check them now.
            pnIniParser ini = pngIni.Ini;
            if (ini["Server.Listen"] == null) {
                Console.WriteLine("Error: Listen IP is not set.");
                return false;
            }

            if (ini["Server.Lookup"] == null ||
                ini["Server.Vault"] == null) {
                Console.WriteLine("Error: Lookup/Vault IPs are missing.");
                return false;
            }

            if (ini["Server.Auth.K"] == null ||
                ini["Server.Auth.X"] == null ||
                ini["Server.Game.K"] == null ||
                ini["Server.Game.X"] == null ||
                ini["Server.Lookup.K"] == null ||
                ini["Server.Lookup.N"] == null ||
                ini["Server.Lookup.X"] == null ||
                ini["Server.Gate.K"] == null ||
                ini["Server.Gate.X"] == null ||
                ini["Server.Vault.K"] == null ||
                ini["Server.Vault.N"] == null ||
                ini["Server.Vault.X"] == null) {
                Console.WriteLine("Error: Encryption Keys not set/missing.");
                Console.WriteLine("Use the /KeyGen argument to create a set of Encryption Keys");
                return false;
            }

            if (ini["Database.Name"] == null ||
                ini["Database.Type"] == null ||
                ini["Database.User"] == null) {
                Console.WriteLine("Error: Database configuration is incomplete.");
                return false;
            }

            return true;
        }

        static void IRunDaemon(string[] args) {
            if (!ILoadConfig()) return;
            
            // Decide which servers we need to spawn...
            bool auth = false;
            bool file = false;
            bool game = false;
            bool gate = false;
            bool lookup = false;
            bool vault  = false;

            foreach (string arg in args) {
                if (arg.ToLower() == "auth")
                    auth = true;
                else if (arg.ToLower() == "file")
                    file = true;
                else if (arg.ToLower() == "game")
                    game = true;
                else if (arg.ToLower() == "gate")
                    gate = true;
                else if (arg.ToLower() == "lookup")
                    lookup = true;
                else if (arg.ToLower() == "vault")
                    vault = true;
            }

            // Test the database connection
            if (auth || vault)
                if (!ITestDb())
                    return;

            // Start the lobby server with the requested services...
            pnLobby lobby = new pnLobby(auth, file, game, gate, lookup, vault);
            try {
                lobby.Listen();
                lobby.InteractiveConsole();
            } catch (pnBindException e) {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Reason: " + e.InnerException.Message);
#if DEBUG
                throw e.InnerException;
#else
                return;
#endif
            }
            
            // TODO: Exit
            throw new NotImplementedException();
        }

        private static bool ITestDb() {
            try {
                // Use var because I don't want to write out the whole type or add a using
                var conn = pnDatabase.Connect();
                conn.Close();
                return true;
            } catch (pnDbException e) {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Reason: " + e.InnerException.Message);
                return false;
            }
        }
    }

    sealed class pngIni {

        static pnIniParser fIniParser;
        public static pnIniParser Ini {
            get { return fIniParser; }
        }

        public static bool Init() {
            if (!File.Exists("PlasmaServers.ini"))
                return false;

            try {
                fIniParser = new pnIniParser("PlasmaServers.ini");
            } catch (Exception e) {
#if DEBUG
                throw e;
#else
                return false;
#endif
            }

            return true;
        }
    }
}
