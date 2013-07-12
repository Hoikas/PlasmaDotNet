using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnAuthClient : plNetClient {
        class LoginData {
            public string fUser, fPass;
            public pnCallback fCallback;

            public LoginData(string user, string pass, pnCallback cb) {
                fUser = user;
                fPass = pass;
                fCallback = cb;
            }
        }

        public event pnAuthPlayerInfo PlayerInfo;
        public event pnAuthServerAddr ServerAddress;
        public event pnAuthVaultNodeChanged VaultNodeChanged;

        private uint? fSrvChg;
        private LoginData fEvilTemporaryHack;

        public pnAuthClient() : base() {
            fConnHdr.fType = ENetProtocol.kConnTypeCliToAuth;
            fPingTimer.Elapsed += (object s, System.Timers.ElapsedEventArgs args) => Ping((uint)args.SignalTime.Ticks);
        }

        protected override void IOnConnect() {
            plBufferedStream bs = new plBufferedStream(new NetworkStream(fSocket, false));
            fConnHdr.Write(bs);
            bs.WriteInt(20);
            pnHelpers.WriteUuid(bs, Guid.Empty);
            bs.Flush();

            // Encryption
            if (!base.INetCliConnect(bs, 41))
                Close();
            bs.Close();

            // Listen
            base.IOnConnect();
        }

        public void FetchVaultNode(uint nodeID, pnCallback cb = null) {
            pnCli2Auth_VaultNodeFetch req = new pnCli2Auth_VaultNodeFetch();
            req.fNodeID = nodeID;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void FetchVaultNodeRefs(uint nodeID, pnCallback cb = null) {
            pnCli2Auth_VaultFetchNodeRefs req = new pnCli2Auth_VaultFetchNodeRefs();
            req.fNodeID = nodeID;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void Login(string user, string pass, pnCallback cb = null) {
            // Logging in is a two step process:
            // First, we must register with the server and get a server challenge
            // Then, we actually login.
            // We will make it easy on the programmer and silently register the client :)
            if (fSrvChg.HasValue || !user.Contains('@'))
                ILogin(user, pass, cb);
            else {
                // Unfortunately, RegisterRequests have no TransID, so we can't save our
                // params using the normal way... So, we must use this ugly HACK.
                fEvilTemporaryHack = new LoginData(user, pass, cb);

                // Back to the regularly scheduled programming
                pnCli2Auth_ClientRegisterRequest req = new pnCli2Auth_ClientRegisterRequest();
                req.fBuildID = fConnHdr.fBuildID;
                lock (fStream) req.Send(fStream);
            }
        }

        private void ILogin(string user, string pass, pnCallback cb) {
            pnCli2Auth_AcctLoginRequest req = new pnCli2Auth_AcctLoginRequest();
            req.fAccount = user;
            req.fChallenge = BitConverter.ToUInt32(OpenSSL.RNG.Random(4), 0);
            req.fHash = pnHelpers.HashLogin(user, pass, req.fChallenge, fSrvChg.HasValue ? fSrvChg.Value : 0);
            switch (Environment.OSVersion.Platform) {
                case PlatformID.MacOSX:
                    req.fOS = "mac";
                    break;
                case PlatformID.Unix:
                    req.fOS = "nix";
                    break;
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    req.fOS = "win";
                    break;
                case PlatformID.Xbox:
                    req.fOS = "xbox";
                    break;
                default:
                    req.fOS = "unknown";
                    break;
            }
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void Ping(uint ms, byte[] payload = null, pnCallback cb = null) {
            pnCli2Auth_PingRequest req = new pnCli2Auth_PingRequest();
            req.fPayload = payload;
            req.fPingTimeMs = ms;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void SetPlayer(uint playerID, pnCallback cb = null) {
            pnCli2Auth_AcctSetPlayerRequest req = new pnCli2Auth_AcctSetPlayerRequest();
            req.fPlayerID = playerID;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        protected override void OnReceive() {
            lock (fStream) {
                pnAuth2Cli msgID = (pnAuth2Cli)fStream.ReadUShort();
                switch (msgID) {
                    case pnAuth2Cli.kAuth2Cli_AcctLoginReply:
                        ILoginReply();
                        break;
                    case pnAuth2Cli.kAuth2Cli_AcctPlayerInfo:
                        IPlayerInfo();
                        break;
                    case pnAuth2Cli.kAuth2Cli_AcctSetPlayerReply:
                        IPlayerSet();
                        break;
                    case pnAuth2Cli.kAuth2Cli_ClientRegisterReply:
                        IClientRegistered();
                        break;
                    case pnAuth2Cli.kAuth2Cli_PingReply:
                        IPingReply();
                        break;
                    case pnAuth2Cli.kAuth2Cli_ServerAddr:
                        IServerAddr();
                        break;
                    case pnAuth2Cli.kAuth2Cli_VaultNodeChanged:
                        IVaultNodeChanged();
                        break;
                    case pnAuth2Cli.kAuth2Cli_VaultNodeFetched:
                        IVaultNodeFetched();
                        break;
                    case pnAuth2Cli.kAuth2Cli_VaultNodeRefsFetched:
                        IVaultNodeRefsFetched();
                        break;
                    default:
#if DEBUG
                        throw new NotSupportedException();
#endif
                        Close();
                        break;
                }
            }

            IReceive();
        }

        private void IClientRegistered() {
            pnAuth2Cli_ClientRegisterReply reply = new pnAuth2Cli_ClientRegisterReply();
            reply.Read(fStream);

            // This always happens as a result of a login request
            // This is such an implementation detail that there's really no need to expose it
            fSrvChg = reply.fChallenge;
            ILogin(fEvilTemporaryHack.fUser, fEvilTemporaryHack.fPass, fEvilTemporaryHack.fCallback);
            fEvilTemporaryHack = null; // Take out the trash...
        }

        private void ILoginReply() {
            pnAuth2Cli_AcctLoginReply reply = new pnAuth2Cli_AcctLoginReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fAcctGuid, reply.fDroidKey, null });
        }

        private void IPingReply() {
            pnAuth2Cli_PingReply reply = new pnAuth2Cli_PingReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fPingTimeMs, reply.fPayload, null });
        }

        private void IPlayerInfo() {
            pnAuth2Cli_AcctPlayerInfo notify = new pnAuth2Cli_AcctPlayerInfo();
            notify.Read(fStream);

            // I realize that this message has a TransID
            // However, our current design doesn't play well with sending out
            // Multiple callbacks for one TransID. Therefore, player infos will
            // be a separate event [for now?]
            if (PlayerInfo != null)
                PlayerInfo(notify.fPlayerID, notify.fPlayerName, notify.fModel);
        }

        private void IPlayerSet() {
            pnAuth2Cli_AcctSetPlayerReply reply = new pnAuth2Cli_AcctSetPlayerReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, null });
        }

        private void IServerAddr() {
            pnAuth2Cli_ServerAddr notify = new pnAuth2Cli_ServerAddr();
            notify.Read(fStream);
            if (ServerAddress != null)
                ServerAddress(notify.fAddress, notify.fToken);
        }

        private void IVaultNodeChanged() {
            pnAuth2Cli_VaultNodeChanged notify = new pnAuth2Cli_VaultNodeChanged();
            notify.Read(fStream);
            if (VaultNodeChanged != null)
                VaultNodeChanged(notify.fNodeID);
        }

        private void IVaultNodeFetched() {
            pnAuth2Cli_VaultNodeFetched reply = new pnAuth2Cli_VaultNodeFetched();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fNode, null });
        }

        private void IVaultNodeRefsFetched() {
            pnAuth2Cli_VaultNodeRefsFetched reply = new pnAuth2Cli_VaultNodeRefsFetched();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fNodeRefs, null });
        }
    }

    public delegate void pnAuthClientRegistered(uint challenge);
    public delegate void pnAuthLoggedIn(ENetError result, Guid acctGuid, uint[] droidKey, object param);
    public delegate void pnAuthPlayerInfo(uint playerID, string name, string model);
    public delegate void pnAuthPlayerSet(ENetError result, object param);
    public delegate void pnAuthServerAddr(IPAddress ip, Guid token);
    public delegate void pnAuthVaultNodeChanged(uint nodeID);
    public delegate void pnAuthVaultNodeFetched(ENetError result, pnVaultNode node, object param);
    public delegate void pnAuthVaultNodeRefsFetched(ENetError result, pnVaultNodeRef[] refs, object param);
}
