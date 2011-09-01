using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using OpenSSL;

namespace Plasma {
    public partial class pnAuthSession : pnSession {

        pnAuthServer fParent;
        pnVaultClient fVaultCli = new pnVaultClient();

        public pnAuthSession(pnAuthServer parent, Socket s, pnCli2Srv_Connect hdr)
            : base(s, hdr) {
            fParent = parent;
            fLog = plDebugLog.GetLog("AuthSrv");
            fChallenge = BitConverter.ToUInt32(RNG.Random(4), 0);
        }

        public override void End() {
            if (fVaultCli.Connected) fVaultCli.Close();
            fParent.RemoveClient(this);
        }

        public void Close() {
            fSocket.Close();
            fStream.Close();
            End();
        }

        public bool Initialize() {
            // Read in the server specific connection header
            // If contains nothing useful here, so we'll just throw it away
            fSocket.Receive(new byte[20], 20, SocketFlags.None);

            // Try to connect to the VaultSrv
            pnIniParser ini = pngIni.Ini;
            fVaultCli.Host = ini["Server.Vault"];
            fVaultCli.N = ini["Server.Vault.N"];
            fVaultCli.ProductID = ini.GetGuid("Server.ProductID");
            fVaultCli.X = ini["Server.Vault.X"];
            try {
                fVaultCli.Connect();
            } catch (Exception e) {
#if DEBUG
                throw e;
#else
                Error(e, "Failed to connect to VaultSrv");
                reply.fResult = ENetError.kNetErrInternalError;
                reply.Send(fStream);
                return;
#endif
            }

            // Note: We won't actually connect to the VaultSrv until the player
            //       tries to log in.
            return IInitialize("Auth");
        }

        public void KickOff(ENetError reason) {
            pnAuth2Cli_KickedOff kick = new pnAuth2Cli_KickedOff();
            kick.fReason = reason;

            lock (fStream) {
                kick.Send(fStream);
            }

            Close();
        }

        protected override void IReadMsg(IAsyncResult ar) {
            // Wrap this in so many try... catch blocks your head will spin.
            try {
                fSocket.EndReceive(ar);

                lock (fStream) {
                    pnCli2Auth msgID = (pnCli2Auth)fStream.ReadUShort();

                    switch (msgID) {
                        case pnCli2Auth.kCli2Auth_AcctLoginRequest:
                            ILogin();
                            break;
                        case pnCli2Auth.kCli2Auth_AcctSetPlayerRequest:
                            ISetPlayer();
                            break;
                        case pnCli2Auth.kCli2Auth_ClientRegisterRequest:
                            IRegisterClient();
                            break;
                        case pnCli2Auth.kCli2Auth_PingRequest:
                            IPingPong();
                            break;
                        case pnCli2Auth.kCli2Auth_PlayerCreateRequest:
                            ICreatePlayer();
                            break;
                        default:
                            // TODO: Kick Off properly
                            Close();
                            break;
                    }
                }

                fSocket.BeginReceive(new byte[0], 0, 0, SocketFlags.Peek, new AsyncCallback(IReadMsg), null);
            } catch (EndOfStreamException) {
                // Remote client disconnected in a strange way
                End();
                return;
            } catch (SocketException e) {
                // Connection Reset OR something weird happened
                if (e.SocketErrorCode != SocketError.ConnectionReset)
                    Error(e);
                End();
                return;
            } catch (ObjectDisposedException e) {
                // The client was kicked, but the socket is still alive, so just ignore.
                if (e.ObjectName != typeof(Socket).ToString())
                    throw e;
            }
        }

        private void IPingPong() {
            pnCli2Auth_PingRequest req = new pnCli2Auth_PingRequest();
            req.Read(fStream);

            pnAuth2Cli_PingReply reply = new pnAuth2Cli_PingReply();
            reply.fPayload = req.fPayload;
            reply.fPingTimeMs = req.fPingTimeMs;
            reply.fTransID = req.fTransID;
            reply.Send(fStream);
        }
    }
}
