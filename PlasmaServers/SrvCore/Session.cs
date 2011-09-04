using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Plasma {
    public abstract class pnSession : plNetServer {

        protected plDebugLog fLog;

        public pnSession(Socket s, pnCli2Srv_Connect hdr)
            : base(s, hdr) { }

        public abstract void End();

        protected virtual bool IInitialize(string srv) {
            byte[] priv, pub;
            try {
                priv = Convert.FromBase64String(pngIni.Ini[String.Format("Server.{0}.K", srv)]);
                pub = Convert.FromBase64String(pngIni.Ini[String.Format("Server.{0}.N", srv)]);
            } catch {
                Error("Invalid encryption keys.");
                return false;
            }

            if (!SetupEncryption(pub, priv)) {
                Error("Bad things happened when setting up the encryption. I blame the remote client.");
                return false;
            }

            return true;
        }

        protected void Debug(string msg) {
            string log = String.Format("[{0}] {1}", fSocket.RemoteEndPoint.ToString(), msg);
            fLog.Debug(log);
        }

        protected void Error(Exception e) {
            string log = String.Format("[{0}] ----UNHANDLED EXCEPTION----", fSocket.RemoteEndPoint.ToString());
            fLog.Error(e, log);
        }

        protected void Error(Exception e, string msg) {
            string log = String.Format("[{0}] {1}", fSocket.RemoteEndPoint.ToString(), msg);
            fLog.Error(e, log);
        }

        protected void Error(string msg) {
            string log = String.Format("[{0}] {1}", fSocket.RemoteEndPoint.ToString(), msg);
            fLog.Error(log);
        }

        protected void Warn(string msg) {
            string log = String.Format("[{0}] {1}", fSocket.RemoteEndPoint.ToString(), msg);
            fLog.Warn(log);
        }
    }

    public abstract class pnSynchSession : pnSession {

        SocketAsyncEventArgs fReceiveArgs = new SocketAsyncEventArgs();

        public pnSynchSession(Socket s, pnCli2Srv_Connect hdr)
            : base(s, hdr) {
            fReceiveArgs.Completed +=new EventHandler<SocketAsyncEventArgs>(IReceive);
            fReceiveArgs.SetBuffer(new byte[0], 0, 0);
        }

        protected override bool IInitialize(string srv) {
            if (base.IInitialize(srv)) {
                IReceive();
                return true;
            } else return false;
        }

        protected void IReceive() {
            if (!fSocket.ReceiveAsync(fReceiveArgs))
                ReadMsg();
        }

        private void IReceive(object sender, SocketAsyncEventArgs args) {
            ReadMsg();
        }

        protected abstract void ReadMsg();
    }

    // TODO: pnBufferedSession? [For FileSrv]
    // Would use ReceiveAsync rather than synchronous reads.
}
