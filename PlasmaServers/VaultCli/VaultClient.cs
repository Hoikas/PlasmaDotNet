using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnVaultClient : plNetClient {
        public pnVaultClient() : base() {
            fConnHdr.fType = ENetProtocol.kConnTypeSrvToVault;
        }

        protected override void IOnConnect() {
            // Write out the lobby header & the server header
            plBufferedStream bs = new plBufferedStream(new NetworkStream(fSocket, false));
            fConnHdr.Write(bs);
            bs.Flush();

            // Encryption
            if (!base.INetCliConnect(bs, 2011))
                Close();
            bs.Close();

            // Listen
            base.IOnConnect();
        }

        public void AcctLogin(string user, byte[] hash, uint cliChg, uint srvChg, pnCallback cb = null) {
            pnCli2Vault_AcctLoginRequest req = new pnCli2Vault_AcctLoginRequest();
            req.fAccount = user;
            req.fCliChg = cliChg;
            req.fHash = hash;
            req.fSrvChg = srvChg;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void CreatePlayer(Guid acct, string name, string shape, pnCallback cb = null) {
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

        public void FetchNode(uint nodeID, pnCallback cb = null) {
            pnCli2Vault_NodeFetch req = new pnCli2Vault_NodeFetch();
            req.fNodeID = nodeID;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void FetchNodeRefs(uint nodeID, pnCallback cb = null) {
            pnCli2Vault_FetchNodeRefs req = new pnCli2Vault_FetchNodeRefs();
            req.fNodeID = nodeID;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void FindNode(pnVaultNode pattern, pnCallback cb = null) {
            pnCli2Vault_NodeFind req = new pnCli2Vault_NodeFind();
            req.fPattern = pattern;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void Ping(uint ms, byte[] payload = null, pnCallback cb = null) {
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

        public void SetPlayer(uint playerID, Guid acct, pnCallback cb = null) {
            pnCli2Vault_PlayerSetRequest req = new pnCli2Vault_PlayerSetRequest();
            req.fAcctGuid = acct;
            req.fPlayerID = playerID;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        protected override void OnReceive() {
            try {
                lock (fStream) {
                    pnVault2Cli msgID = (pnVault2Cli)fStream.ReadUShort();
                    switch (msgID) {
                        case pnVault2Cli.kVault2Cli_AcctLoginReply:
                            ILoggedIn();
                            break;
                        case pnVault2Cli.kVault2Cli_NodeFetched:
                            INodeFetched();
                            break;
                        case pnVault2Cli.kVault2Cli_NodeRefsFetched:
                            INodeRefsFetched();
                            break;
                        case pnVault2Cli.kVault2Cli_NodeFindReply:
                            INodeFound();
                            break;
                        case pnVault2Cli.kVault2Cli_PingReply:
                            IPingPong();
                            break;
                        case pnVault2Cli.kVault2Cli_PlayerCreateReply:
                            IPlayerCreated();
                            break;
                        case pnVault2Cli.kVault2Cli_PlayerSetReply:
                            IPlayerSet();
                            break;
                    }
                }

                IReceive();
            } catch (EndOfStreamException) {
                // Disconnected in a strange way
                IDisconnected();
                return;
            } catch (SocketException) {
                // Connection Reset OR something weird happened
                IDisconnected();
                return;
            } catch (ObjectDisposedException) {
                // The socket was closed.
                IDisconnected();
                return;
            }
        }

        private void ILoggedIn() {
            pnVault2Cli_AcctLoginReply reply = new pnVault2Cli_AcctLoginReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fAcctGuid, reply.fPermissions, reply.fAvatars, null });
        }

        private void INodeFetched() {
            pnVault2Cli_NodeFetched reply = new pnVault2Cli_NodeFetched();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fNode, null });
        }

        private void INodeFound() {
            pnVault2Cli_NodeFindReply reply = new pnVault2Cli_NodeFindReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fNodeIDs, null });
        }

        private void INodeRefsFetched() {
            pnVault2Cli_NodeRefsFetched reply = new pnVault2Cli_NodeRefsFetched();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fNodeRefs, null });
        }

        private void IPingPong() {
            pnVault2Cli_PingReply reply = new pnVault2Cli_PingReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fPingTimeMs, reply.fPayload, null });
        }

        private void IPlayerCreated() {
            pnVault2Cli_PlayerCreateReply reply = new pnVault2Cli_PlayerCreateReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fPlayerID, reply.fPlayerName, reply.fShape, null });
        }

        private void IPlayerSet() {
            pnVault2Cli_PlayerSetReply reply = new pnVault2Cli_PlayerSetReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, null });
        }
    }

    public delegate void pnVaultAcctLoggedIn(ENetError result, Guid guid, int perms, pnVaultAvatarInfo[] avatars, object param);
    public delegate void pnVaultNodeFetched(ENetError result, pnVaultNode node, object param);
    public delegate void pnVaultNodeRefsFetched(ENetError result, pnVaultNodeRef[] refs, object param);
    public delegate void pnVaultNodeFound(ENetError result, uint[] nodeIDs, object param);
    public delegate void pnVaultPlayerCreated(ENetError result, uint playerID, string playerName, string shape, object param);
    public delegate void pnVaultPlayerSet(ENetError result, object param);
    public delegate void pnVaultPong(uint pingTimeMs, byte[] payload, object param);
}
