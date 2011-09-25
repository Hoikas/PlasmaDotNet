using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnGateClient : plNetClient {

        public pnGateClient() : base() {
            fConnHdr.fType = ENetProtocol.kConnTypeCliToGate;
        }

        protected override void IOnConnect() {
            plBufferedStream bs = new plBufferedStream(new NetworkStream(fSocket, false));
            fConnHdr.Write(bs);
            bs.WriteInt(20);
            pnHelpers.WriteUuid(bs, Guid.Empty);
            bs.Flush();

            // Encryption
            if (!base.INetCliConnect(bs, 4))
                Close();
            bs.Close();

            // Listen
            base.IOnConnect();
        }

        public void GetAuthSrvIP(pnCallback cb = null) {
            pnCli2Gate_AuthSrvIpAddressRequest req = new pnCli2Gate_AuthSrvIpAddressRequest();
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        /// <summary>
        /// Requests the IP address of an active file server
        /// </summary>
        /// <param name="isPatcher">Specifies whether or not this client is a patcher</param>
        /// <param name="cb">Optional callback descriptor</param>
        /// <seealso cref="pnGateFileIP"/>
        public void GetFileSrvIP(bool isPatcher, pnCallback cb = null) {
            pnCli2Gate_FileSrvIpAddressRequest req = new pnCli2Gate_FileSrvIpAddressRequest();
            req.fIsPatcher = isPatcher;
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void Ping(uint ms, byte[] payload, pnCallback cb = null) {
            pnCli2Gate_PingRequest req = new pnCli2Gate_PingRequest();
            req.fPayload = payload;
            req.fPingTimeMs = ms;
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
                    pnGate2Cli msgID = (pnGate2Cli)fStream.ReadUShort();
                    switch (msgID) {
                        case pnGate2Cli.kGateKeeper2Cli_AuthSrvIpAddressReply:
                            IAuthSrvIpReply();
                            break;
                        case pnGate2Cli.kGateKeeper2Cli_FileSrvIpAddressReply:
                            IFileSrvIpReply();
                            break;
                        case pnGate2Cli.kGateKeeper2Cli_PingReply:
                            IPingReply();
                            break;
                    }
                }
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

        private void IAuthSrvIpReply() {
            pnGate2Cli_AuthSrvIpAddressReply reply = new pnGate2Cli_AuthSrvIpAddressReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fHost, null });
        }

        private void IFileSrvIpReply() {
            pnGate2Cli_FileSrvIpAddressReply reply = new pnGate2Cli_FileSrvIpAddressReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fHost, null });
        }

        private void IPingReply() {
            pnGate2Cli_PingReply reply = new pnGate2Cli_PingReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fPingTimeMs, reply.fPayload, null });
        }
    }

    public delegate void pnGateAuthIP(string host, object param);
    public delegate void pnGateFileIP(string host, object param);
}
