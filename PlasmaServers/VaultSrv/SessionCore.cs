using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession : pnSession {

        pnVaultServer fParent;
        IDbConnection fDb;
        
        public pnVaultSession(pnVaultServer parent, Socket s, pnCli2Srv_Connect hdr)
            : base(s, hdr) {
            fParent = parent;
            fLog = plDebugLog.GetLog("VaultSrv");
        }

        public void Close() {
            fSocket.Close();
            End();
        }

        public override void End() {
            fParent.RemoveClient(this);
            if (fDb.State == ConnectionState.Open)
                fDb.Close();
        }

        public bool Initialize() {
            try {
                fDb = pnDatabase.Connect();
            } catch (pnDbException e) {
                Error(e);
                return false;
            }

            return IInitialize("Vault");
        }

        protected override void IReadMsg(IAsyncResult ar) {
            // Wrap this in so many try... catch blocks your head will spin.
            try {
                fSocket.EndReceive(ar);

                lock (fStream) {
                    pnCli2Vault msgID = (pnCli2Vault)fStream.ReadUShort();

                    switch (msgID) {
                        case pnCli2Vault.kCli2Vault_PingRequest:
                            IPingPong();
                            break;
                        case pnCli2Vault.kCli2Vault_PlayerCreateRequest:
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
            pnCli2Vault_PingRequest req = new pnCli2Vault_PingRequest();
            req.Read(fStream);

            pnVault2Cli_PingReply reply = new pnVault2Cli_PingReply();
            reply.fPayload = req.fPayload;
            reply.fPingTimeMs = req.fPingTimeMs;
            reply.fTransID = req.fTransID;
            reply.Send(fStream);
        }
    }
}
