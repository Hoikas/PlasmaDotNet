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

        public pnAuthSession(pnAuthServer parent, Socket s, pnCli2Srv_Connect hdr)
            : base(s, hdr) {
            fParent = parent;
            fLog = plDebugLog.GetLog("AuthSrv");
            fChallenge = BitConverter.ToUInt32(RNG.Random(4), 0);
        }

        public override void End() {
            // TODO: Clean up any Srv2Srv connections
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
            NetworkStream ns = new NetworkStream(fSocket, false);
            hsStream s = new hsStream(ns);

            try {
                int size = s.ReadInt() - 4;
                s.ReadBytes(size);
            } catch {
                s.Close();
                ns.Close();
                return false;
            }

            s.Close();
            ns.Close();

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
