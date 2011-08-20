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

        protected bool IInitialize(string srv) {
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

            fSocket.BeginReceive(new byte[0], 0, 0, SocketFlags.Peek, new AsyncCallback(IReadMsg), null);
            return true;
        }

        protected abstract void IReadMsg(IAsyncResult ar);

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
}
