using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnLobby {

        Socket fSocket;
        plDebugLog fLog = plDebugLog.GetLog("Lobby");

        pnAuthServer fAuth;
        pnVaultServer fVault;

        SocketAsyncEventArgs fAcceptArgs = new SocketAsyncEventArgs();
        const int kAcceptBufferMinSize = 288;

        public pnLobby(bool auth, bool file, bool game, bool gate, bool lookup, bool vault) {
            if (auth) {
                fAuth = new pnAuthServer();
                fAuth.CheckDb();
            }

            if (vault) {
                fVault = new pnVaultServer();
                fVault.CheckDb();
            }
        }

        public void Listen() {
            if (fSocket == null)
                ISetupSocket();

            if (!fSocket.AcceptAsync(fAcceptArgs))
                IAcceptConnection(fSocket.Accept());
        }

        private void IOnAsyncAccept(object sender, SocketAsyncEventArgs args) {
            if (args.SocketError == SocketError.Success) {
                IAcceptConnection(args.AcceptSocket);
            } else {
                args.AcceptSocket.Close();
            }

            fAcceptArgs.AcceptSocket = null;
            Listen();
        }

        private void IAcceptConnection(Socket cli) {
            hsStream s = new hsStream(new NetworkStream(cli, false));
            pnCli2Srv_Connect hdr = new pnCli2Srv_Connect();
            hdr.Read(s);
            s.Close();

            IAcceptConnection(cli, hdr);
        }

        private void IAcceptConnection(Socket cli, pnCli2Srv_Connect hdr) {
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
                case ENetProtocol.kConnTypeSrvToLookup:
                    throw new NotImplementedException();
                case ENetProtocol.kConnTypeSrvToVault:
                    if (!ITestServerGuid(hdr)) {
                        cli.Close();
                        break;
                    }

                    fVault.EatClient(cli, hdr);
                    break;
                default:
                    // Unknown protocol, boot the client on principle.
                    cli.Close();
                    break;
            }
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
            fSocket.NoDelay = true; // Match Cyan

            try {
                fSocket.Bind(new IPEndPoint(ip, 14617));
            } catch (Exception e) {
                // Rethrow as a more user accessible exception
                throw new pnBindException("Failed to bind to socket.", e);
            }

            fAcceptArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IOnAsyncAccept);
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
            Guid product = pngIni.Ini.GetGuid("Client.ProductID");
            if (product != Guid.Empty)
                return hdr.fProductUuid == product;
            return true;
        }

        private bool ITestServerGuid(pnCli2Srv_Connect hdr) {
            Guid product = pngIni.Ini.GetGuid("Server.ProductID");
            if (product != Guid.Empty)
                return hdr.fProductUuid == product;
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
