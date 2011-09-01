using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnVaultClient : plNetClient {

        public pnVaultClient() {
            fConnHdr.fType = ENetProtocol.kConnTypeSrvToVault;
        }

        protected override void IOnConnect() {
            // Write out the lobby header & the server header
            plBufferedStream bs = new plBufferedStream(new NetworkStream(fSocket, false));
            fConnHdr.Write(bs);
            bs.Flush();

            // Encryption
            if (!base.INetCliConnect(bs, 2011))
                throw new plNetException("Modified DH exchange failed");
            bs.Close();

            // Listen
            base.IOnConnect();
            fSocket.BeginReceive(new byte[2], 0, 2, SocketFlags.Peek, new AsyncCallback(IReceive), null);
        }

        public void CreatePlayer(Guid acct, string name, string shape, pnCallback cb) {
            pnCli2Vault_PlayerCreateRequest req = new pnCli2Vault_PlayerCreateRequest();
            req.fAcctGuid = acct;
            req.fPlayerName = name;
            req.fShape = shape;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void Ping(uint ms, byte[] payload, pnCallback cb) {
            pnCli2Vault_PingRequest req = new pnCli2Vault_PingRequest();
            req.fPayload = payload;
            req.fPingTimeMs = ms;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        private void IReceive(IAsyncResult ar) {
            try {
                lock (fStream) {
                    fSocket.EndReceive(ar);

                    pnVault2Cli msgID = (pnVault2Cli)fStream.ReadUShort();
                    switch (msgID) {
                        case pnVault2Cli.kVault2Cli_PingReply:
                            IPingPong();
                            break;
                        case pnVault2Cli.kVault2Cli_PlayerCreateReply:
                            IPlayerCreated();
                            break;
                    }

                    // Listen
                    fSocket.BeginReceive(new byte[2], 0, 2, SocketFlags.Peek, new AsyncCallback(IReceive), null);
                }
            } catch (EndOfStreamException) {
                // Disconnected in a strange way
                return;
            } catch (SocketException) {
                // Connection Reset OR something weird happened
                return;
            } catch (ObjectDisposedException e) {
                // The client was kicked, but the socket is still alive, so just ignore.
                if (e.ObjectName != typeof(Socket).ToString())
                    throw e;
            }
        }

        private void IPingPong() {
            pnVault2Cli_PingReply reply = new pnVault2Cli_PingReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fPingTimeMs, reply.fPayload });
        }

        private void IPlayerCreated() {
            pnVault2Cli_PlayerCreateReply reply = new pnVault2Cli_PlayerCreateReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fPlayerID, reply.fPlayerName, reply.fShape });
        }
    }

    public delegate void pnVaultPlayerCreated(ENetError result, uint playerID, string playerName, string shape, object param);
    public delegate void pnVaultPong(uint pingTimeMs, byte[] payload, object param);
}
