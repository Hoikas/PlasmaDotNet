using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnLobby {

        Socket fSocket;
        plDebugLog fLog = plDebugLog.GetLog("Lobby");

        pnAuthServer fAuth;

        public pnLobby(bool auth, bool file, bool game, bool gate, bool lookup, bool vault) {
            if (auth) {
                fAuth = new pnAuthServer();
                fAuth.CheckDb();
            }
        }

        public void InteractiveConsole() {
            while (true) {
                Console.Write("$ ");
                string line = Console.ReadLine();
                string lower = line.ToLower();
                
                if (lower == "exit" || lower == "quit")
                    break;
                else if (lower == "show copyright") {
                    Console.WriteLine("This program is free software: you can redistribute it and/or modify");
                    Console.WriteLine("it under the terms of the GNU Affero General Public License as published by");
                    Console.WriteLine("the Free Software Foundation, either version 3 of the License, or");
                    Console.WriteLine("(at your option) any later version.");
                } else if (lower == "show warranty") {
                    Console.WriteLine("This program is distributed in the hope that it will be useful,");
                    Console.WriteLine("but WITHOUT ANY WARRANTY; without even the implied warranty of");
                    Console.WriteLine("MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the");
                    Console.WriteLine("GNU Affero General Public License for more details.");
                } 

                Console.WriteLine();
            }
        }


        public void Listen() {
            if (fSocket == null)
                ISetupSocket();
            fSocket.BeginAccept(new AsyncCallback(IAcceptConnection), null);
        }

        private void IAcceptConnection(IAsyncResult ar) {
            Socket cli = null;
            pnCli2Srv_Connect hdr = null;

            // Sanely handle any stupid. We do NOT want to kill this thread!
            try {
                cli = fSocket.EndAccept(ar);
                cli.ReceiveTimeout = 100; // We don't want to hold this thread up long. 
                NetworkStream ns = new NetworkStream(cli, false);
                plBufferedStream bs = new plBufferedStream(ns);

                hdr = new pnCli2Srv_Connect();
                hdr.Read(bs);

                bs.Close();
                ns.Close();
            } catch (Exception e) {
#if DEBUG
                throw e;
#else
                fLog.Error("Failed to accept incoming connection: " + e.GetType().ToString());
                fLog.Error(e.ToString());
#endif
                Listen();
                return;
            }
            
            // Now, let's send this client to where it needs to go...
            pnIniParser ini = pngIni.Ini;
            switch (hdr.fType) {
                case ENetProtocol.kConnTypeCliToAuth:
                    if (!ITestClientBranchID(hdr)) {
                        cli.Close();
                        break;
                    }

                    if (!ITestClientBuildID(hdr)) {
                        cli.Close();
                        break;
                    }

                    if (!ITestClientGuid(hdr)) {
                        cli.Close();
                        break;
                    }

                    fAuth.EatClient(cli, hdr);
                    break;
                case ENetProtocol.kConnTypeCliToFile:
                    throw new NotImplementedException();
                case ENetProtocol.kConnTypeCliToGame:
                    throw new NotImplementedException();
                case ENetProtocol.kConnTypeCliToGate:
                    throw new NotImplementedException();
                default:
                    // Unknown protocol, boot the client on principle.
                    cli.Close();
                    break;
            }

            Listen();
        }

        private void ISetupSocket() {
            IPAddress ip = null;
            try {
                ip = IPAddress.Parse(pngIni.Ini["Server.Listen"]);
            } catch (Exception e) {
                // Rethrow this as an exception that the console will consume.
                throw new pnBindException("Server Bind address is malformed.", e);
            }

            fSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try {
                fSocket.Bind(new IPEndPoint(ip, 14617));
            } catch (Exception e) {
                // Rethrow as a more user accessible exception
                throw new pnBindException("Failed to bind to socket.", e);
            }

            fSocket.Listen(10);
        }

        #region Client-Test Methods
        private bool ITestClientBranchID(pnCli2Srv_Connect hdr) {
            int? branchID = pngIni.Ini.GetInteger("Client.BranchID");
            if (branchID.HasValue)
                return hdr.fBranchID == branchID.Value;
            return true;
        }

        private bool ITestClientBuildID(pnCli2Srv_Connect hdr) {
            int? buildID = pngIni.Ini.GetInteger("Client.BuildID");
            if (buildID.HasValue)
                return hdr.fBuildID == buildID.Value;
            return true;
        }

        private bool ITestClientGuid(pnCli2Srv_Connect hdr) {
            Guid? product = pngIni.Ini.GetGuid("Client.ProductID");
            if (product.HasValue)
                return hdr.fProductUuid == product.Value;
            return true;
        }
        #endregion
    }

    public class pnBindException : plNetException {
        public pnBindException() { }
        public pnBindException(string message) : base(message) { }
        public pnBindException(string message, Exception inner) : base(message, inner) { }
    }
}
