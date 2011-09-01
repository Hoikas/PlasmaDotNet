using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using OpenSSL;

namespace Plasma {
    public abstract class plNetClient {

        protected plBufferedStream fStream;
        protected Socket fSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        protected pnCli2Srv_Connect fConnHdr = new pnCli2Srv_Connect();
        ManualResetEvent fWaitOnConnect = new ManualResetEvent(false);

        protected IPAddress fHost;
        protected int fPort = 14617;

        private byte[] fN;
        private byte[] fX;

        protected Dictionary<uint, pnCallback> fCallbacks = new Dictionary<uint, pnCallback>();
        private uint fTransID = 0;

        public uint BranchID {
            get { return fConnHdr.fBranchID; }
            set { fConnHdr.fBranchID = value; }
        }

        public uint BuildID {
            get { return fConnHdr.fBuildID; }
            set { fConnHdr.fBuildID = value; }
        }

        public bool Connected {
            get { return fSocket.Connected; }
        }

        public string Host {
            get { return fHost.ToString(); }
            set {
                if (value == null) return;

                try {
                    fHost = IPAddress.Parse(value);
                } catch {
                    IPAddress[] resolve = Dns.GetHostAddresses(value);
                    if (resolve.Length == 0)
                        throw new plNetException("Couldn't resolve host: " + value);
                    fHost = resolve[0];
                }
            }
        }

        /// <summary>
        /// Gets or sets a base64 representation of the NKey in OpenSSL byte order
        /// </summary>
        public string N {
            get { return Convert.ToBase64String(fN); }
            set { fN = Convert.FromBase64String(value); }
        }

        public int Port {
            get { return fPort; }
            set { fPort = value; }
        }

        public Guid ProductID {
            get { return fConnHdr.fProductUuid; }
            set { fConnHdr.fProductUuid = value; }
        }

        /// <summary>
        /// Gets or sets a base64 representation of the XKey in OpenSSL byte order
        /// </summary>
        public string X {
            get { return Convert.ToBase64String(fX); }
            set { fX = Convert.FromBase64String(value); }
        }

        public uint Version {
            get { return fConnHdr.fProtocolVer; }
            set { fConnHdr.fProtocolVer = value; }
        }

        public void Connect() {
            // We're probably not going to get any performance increase from ConnectAsync
            // because (in most cases) we will only connect a client once, then throw it away.
            fSocket.BeginConnect(fHost, fPort, new AsyncCallback(IOnConnect), null);
        }

        private void IOnConnect(IAsyncResult ar) {
            fSocket.EndConnect(ar);
            IOnConnect();
        }

        protected virtual void IOnConnect() {
            fWaitOnConnect.Set();
        }

        /// <summary>
        /// Blocks the current thread until a connection has been established.
        /// </summary>
        public void WaitForConnection() {
            fWaitOnConnect.Reset();
        }

        public void Close() {
            if (fStream != null) fStream.Close();
            fSocket.Shutdown(SocketShutdown.Both); // Be nice
            fSocket.Close();
            fCallbacks.Clear();
            fWaitOnConnect.Reset();
        }

        protected void FireCallback(uint transID, object[] param) {
            if (fCallbacks.ContainsKey(transID)) {
                pnCallback cb = fCallbacks[transID];
                fCallbacks.Remove(transID);

                object[] args;
                if (cb.Parameter == null) args = param;
                else {
                    args = new object[param.Length + 1];
                    param.CopyTo(args, 0);
                    args[args.Length - 1] = cb.Parameter;
                }

                cb.Callback.DynamicInvoke(args);
            }
        }

        protected uint GetTransID() {
            uint trans = fTransID;
            if (fTransID < UInt32.MaxValue)
                fTransID++;
            else
                fTransID = 0;
            return trans;
        }

        protected bool INetCliConnect(plBufferedStream bs, int gval) {
            byte[] cli = ISetupKeys(bs, gval);
            byte[] srv = IReadNetClientEncrypt(bs);
            if (srv == null) return false;
            ISetupEncryption(srv, cli);

            return true;
        }

        private void ISetupEncryption(byte[] srv, byte[] cli) {
            byte[] key = new byte[7];
            for (int i = 0; i < 7; i++) {
                if (i >= cli.Length) key[i] = srv[i];
                else key[i] = (byte)(cli[i] ^ srv[i]);
            }

            fStream = new plBufferedStream(new pnRC4SocketStream(fSocket, key));
        }

        private byte[] ISetupKeys(plBufferedStream s, int gval) {
            BigNum b = BigNum.Random(512);
            BigNum n = new BigNum(fN);
            BigNum x = new BigNum(fX);

            // Calculate seeds
            BigNum client_seed = x.PowMod(b, n);
            BigNum server_seed = new BigNum(gval).PowMod(b, n);
            byte[] cliSeed = client_seed.ToLittleArray();
            IWriteNetClientConnect(s, server_seed.ToLittleArray());

            // Dispose of this crap...
            b.Dispose();
            n.Dispose();
            x.Dispose();
            client_seed.Dispose();
            server_seed.Dispose();

            return cliSeed;
        }

        private byte[] IReadNetClientEncrypt(plBufferedStream s) {
            try {
                if (s.ReadByte() != plNetCore.kNetCliEncrypt) return null;
                if (s.ReadByte() != 9) return null;
                return s.ReadBytes(7);
            } catch (EndOfStreamException) {
                return null;
            }
        }

        private void IWriteNetClientConnect(plBufferedStream s, byte[] seed) {
            s.WriteByte(plNetCore.kNetCliConnect);
            s.WriteByte(66);
            s.WriteBytes(seed);
            s.Flush();
        }
    }

    public class pnCallback {

        Delegate fFunction;
        object fParam;

        /// <summary>
        /// Delegate to be executed when the network transaction completes
        /// </summary>
        public Delegate Callback {
            get { return fFunction; }
            set { fFunction = value; }
        }

        /// <summary>
        /// Parameter to pass via the Delegate callback
        /// </summary>
        public object Parameter {
            get { return fParam; }
            set { fParam = value; }
        }

        public pnCallback() { }
        public pnCallback(Delegate cb) { fFunction = cb; }
        public pnCallback(Delegate cb, object param) { fFunction = cb; fParam = param; }
    }
}
