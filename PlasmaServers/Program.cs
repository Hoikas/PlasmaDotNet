using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using OpenSSL;

namespace Plasma {
    sealed class Program : ServiceBase {

        public Program() : base() {
            ServiceName = "PlasmaServers";
        }

        protected override void OnStart(string[] args) {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            string[] arguments;
            if(args.Length == 0)
                arguments = new string[] { "auth", "file", "game", "gate", "lookup", "vault" };
            else
                arguments = args;

            if (!ILoadConfig()) return;
            
            // Decide which servers we need to spawn...
            bool auth = false;
            bool file = false;
            bool game = false;
            bool gate = false;
            bool lookup = false;
            bool vault  = false;

            foreach (string arg in arguments) {
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
            lobby.Listen();
        }

        protected override void OnStop() {
            // TODO: Exit
            throw new NotImplementedException();
        }

        static void Main(string[] args) {
            Console.WriteLine("Plasma .NET Servers Copyright (C) 2011  Adam Johnson");
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details type `show w'.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it");
            Console.WriteLine("under certain conditions; type `show c' for details.");
            Console.WriteLine();
            Console.WriteLine();

            // Check the arguments to see what we need to do...
            // If the first arg is /KeyGen, then generate some keys for the INI
            // Else, we may have /Daemon (default) with a list of servers to spawn
            if (args.Length == 0) {
                ServiceBase.Run(new Program());
            } else if (args[0].ToLower() == "/keygen") {
                IGenerateKeys();
            } else if (args[0].ToLower() == "/debug"){
                Program prog = new Program();
                if (args.Length == 1)
                    prog.IRunDaemon(new string[] { "auth", "file", "game", "gate", "lookup", "vault" });
                else
                    prog.IRunDaemon(args);
            }
        }

        #region Interactive Console
        void IInteractiveMode() {
            // Try not to crash it... Otherwise you're screwed.
            bool keepGoing = true;
            while (keepGoing) {
                Console.Write("$ ");
                keepGoing = IExecuteLine(Console.ReadLine());
            }
        }

        bool IExecuteLine(string line) {
            string lower = line.ToLower();
            
            if (lower.StartsWith("acctadd")) {
                IAddAccount(line.Substring(7).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            } else if (lower == "exit" || lower == "quit")
                return false;
            else if (lower.StartsWith("show c")) {
                Console.WriteLine("This program is free software: you can redistribute it and/or modify");
                Console.WriteLine("it under the terms of the GNU Affero General Public License as published by");
                Console.WriteLine("the Free Software Foundation, either version 3 of the License, or");
                Console.WriteLine("(at your option) any later version.");
            } else if (lower.StartsWith("show w")) {
                Console.WriteLine("This program is distributed in the hope that it will be useful,");
                Console.WriteLine("but WITHOUT ANY WARRANTY; without even the implied warranty of");
                Console.WriteLine("MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the");
                Console.WriteLine("GNU Affero General Public License for more details.");
            }

            return true;
        }

        void IAddAccount(string[] args) {
            if (args.Length < 2) {
                Console.WriteLine("Usage: addacct <username> <password> [permissions]");
                Console.Write("Valid Permissions:");

                string[] perms = Enum.GetNames(typeof(pnAcctPerms));
                foreach (string perm in perms)
                    Console.Write(" " + perm);
                Console.WriteLine();
                return;
            }

            // Get our permissions
            int oPerm = (int)pnAcctPerms.Explorer;
            Guid oGuid = Guid.NewGuid();
            if (args.Length > 2) {
                try {
                    oPerm = (int)Enum.Parse(typeof(pnAcctPerms), args[2], true);
                } catch (ArgumentException) {
                    Console.WriteLine(String.Format("Error: Undefined permission level '{0}'", args[2]));
                    return;
                }
            }

            try {
                IDbConnection db = pnDatabase.Connect();

                // Check to see if this account already exists
                pnSqlSelectStatement check = new pnSqlSelectStatement();
                check.AddColumn("Guid");
                check.AddWhere("Username", args[0]);
                check.Limit = 1;
                check.Table = "Accounts";
                IDataReader r = check.Execute(db);
                if (r.Read()) {
                    Console.WriteLine(String.Format("Account already exists! [GUID: {0}]", r[0]));
                    r.Close();
                    return;
                } else r.Close();

                // Alright, we can add us an account
                pnSqlInsertStatement create = new pnSqlInsertStatement();
                create.AddValue("Username", args[0]);
                create.AddValue("Password", pnHelpers.GetString(pnHelpers.HashLogin(args[0], args[1])));
                create.AddValue("Permissions", oPerm);
                create.AddValue("Guid", oGuid);
                create.Table = "Accounts";
                create.Execute(db);
            } catch (pnDbException e) {
                Console.WriteLine("---Database Error---");
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

        #region KeyGen
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
        #endregion

        bool ILoadConfig() {
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

        void IRunDaemon(string[] args) {
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
                IInteractiveMode();
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

        private bool ITestDb() {
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
