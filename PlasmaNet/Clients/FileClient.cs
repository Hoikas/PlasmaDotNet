using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnFileClient : pnSynchClient {
        public pnFileClient() : base() {
            fConnHdr.fType = ENetProtocol.kConnTypeCliToFile;
        }

        protected override void IOnConnect() {
            fStream = new plBufferedStream(new NetworkStream(fSocket, false));
            fConnHdr.Write(fStream);
            fStream.WriteUInt(12); // Complete BS below, but who cares?
            fStream.WriteUInt(0);
            fStream.WriteUInt(0);
            fStream.Flush();

            // File connections send an Int32 length, then the Int16 msgID
            fStream.PrependLength = true;
            base.IOnConnect();
        }

        public void Ping(uint pingTimeMs) {
            pnCli2File_PingRequest req = new pnCli2File_PingRequest();
            req.fPingTimeMs = pingTimeMs;
            lock (fStream)
                req.Send(fStream);
        }

        protected override void OnReceive() {
            try {
                lock (fStream) {
                    int size = fStream.ReadInt();
                    pnFile2Cli msgID = (pnFile2Cli)fStream.ReadUInt();

                    switch (msgID) {
                        case pnFile2Cli.kFile2Cli_PingReply:
                            IPingReply();
                            break;
                        default:
                            // Yay, we can just throw away messages we don't understand!
                            fStream.ReadBytes(size - 6);
                            break;
                    }

                    IReceive();
                }
            } catch (EndOfStreamException) {
                // Disconnected in a strange way
                return;
            } catch (SocketException) {
                // Connection Reset OR something weird happened
                return;
            } catch (ObjectDisposedException) {
                // The socket was closed.
            }
        }

        private void IPingReply() {
            pnFile2Cli_PingReply reply = new pnFile2Cli_PingReply();
            reply.Read(fStream);
            // We have no TransID, so we'll not fire off a callback for pings.
        }
    }
}
