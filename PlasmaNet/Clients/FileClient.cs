using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public class pnFileClient : plNetClient {
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

            // File connections send an Int32 length, then the Int32 msgID
            fStream.PrependLength = true;
            base.IOnConnect();
        }

        public void GetBuildId(pnCallback cb = null) {
            pnCli2File_BuildIdRequest req = new pnCli2File_BuildIdRequest();
            req.fTransID = GetTransID();

            lock (fStream) {
                if (cb != null)
                    fCallbacks.Add(req.fTransID, cb);
                req.Send(fStream);
            }
        }

        public void Ping(uint pingTimeMs) {
            pnCli2File_PingRequest req = new pnCli2File_PingRequest();
            req.fPingTimeMs = pingTimeMs;
            lock (fStream)
                req.Send(fStream);
        }

        protected override void OnReceive() {
            lock (fStream) {
                int size = fStream.ReadInt();
                pnFile2Cli msgID = (pnFile2Cli)fStream.ReadUInt();

                switch (msgID) {
                    case pnFile2Cli.kFile2Cli_BuildIdReply:
                        IBuildIdReply();
                        break;
                    case pnFile2Cli.kFile2Cli_PingReply:
                        IPingReply();
                        break;
                    default:
                        // Yay, we can just throw away messages we don't understand!
                        fStream.ReadBytes(size - 8);
                        break;
                }

                IReceive();
            }
        }

        private void IBuildIdReply() {
            pnFile2Cli_BuildIdReply reply = new pnFile2Cli_BuildIdReply();
            reply.Read(fStream);
            FireCallback(reply.fTransID, new object[] { reply.fResult, reply.fBuildID, null });
        }

        private void IPingReply() {
            pnFile2Cli_PingReply reply = new pnFile2Cli_PingReply();
            reply.Read(fStream);
            // We have no TransID, so we'll not fire off a callback for pings.
        }
    }

    public delegate void pnFileBuildId(ENetError result, uint buildID, object param);
}
