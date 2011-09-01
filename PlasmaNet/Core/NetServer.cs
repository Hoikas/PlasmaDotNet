using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using OpenSSL;

namespace Plasma {
    public abstract class plNetServer {

        protected Socket fSocket;
        protected plBufferedStream fStream;
        protected pnCli2Srv_Connect fConnHdr;

        public plNetServer(Socket client, pnCli2Srv_Connect hdr) {
            fSocket = client;
            fConnHdr = hdr;
        }

        protected bool SetupEncryption(byte[] pubKey, byte[] privKey) {
            NetworkStream ns = new NetworkStream(fSocket, false);
            plBufferedStream temp = new plBufferedStream(ns);
            byte[] yData = IReadNetCliConnect(temp);

            if (yData != null) {
                BigNum Y = new BigNum(yData);
                BigNum N = new BigNum(pubKey);
                BigNum K = new BigNum(privKey);

                // Generate some randomness and do Y**K%N to get the key components
                byte[] server_seed = RNG.Random(7);
                BigNum client_seed = Y.PowMod(K, N);
                byte[] seed_data = client_seed.ToLittleArray();

                // Grab the first 7 bytes and xor it with some randomness to make our key
                byte[] key = new byte[7];
                for (int i = 0; i < key.Length; i++)
                    if (i >= seed_data.Length) key[i] = server_seed[i];
                    else key[i] = (byte)(seed_data[i] ^ server_seed[i]);

                // Final setup
                fStream = new plBufferedStream(new pnRC4SocketStream(fSocket, key));
                IWriteNetCliEncrypt(temp, server_seed);

                // Explicitly clean up some resources so that we don't pressure the GC too much.
                // Those explicit finalizers really suck.
                Y.Dispose();
                N.Dispose();
                K.Dispose();
                client_seed.Dispose();
            } else
                // Write the error code to the unencrypted buffer
                temp.WriteByte(plNetCore.kNetCliError);

            temp.Flush();
            temp.Close();
            ns.Close();

            return (yData != null);
        }

        /// <summary>
        /// Reads in a NetCliConnect message from the stream
        /// </summary>
        /// <param name="s">Stream to read the message from</param>
        /// <returns>Connection YData (an array of 64 bytes) or NULL if the message is malformed</returns>
        protected byte[] IReadNetCliConnect(hsStream s) {
            byte[] y_data = null;
            try {
                if (s.ReadByte() != plNetCore.kNetCliConnect) return null;
                int size = (int)s.ReadByte() - 2;
                y_data = s.ReadBytes(size);
            } catch (Exception e) {
#if DEBUG
                throw e;
#else
                return null;
#endif
            }

            // Truncate the YData if it's too large
            if (y_data.Length > 64) {
                byte[] old = y_data;
                y_data = new byte[64];
                Buffer.BlockCopy(old, 0, y_data, 0, 64);
            }

            // The client sends us the YData in Little Endian, but
            // BigNum wants Big Endian (because OpenSSL). Let's fix that.
            Array.Reverse(y_data);

            return y_data;
        }

        protected void IWriteNetCliEncrypt(hsStream s, byte[] seed) {
            if (seed.Length != 7)
                throw new ArgumentException("seed should be an array of exactly 7 bytes");

            s.WriteByte(plNetCore.kNetCliEncrypt);
            s.WriteByte(9);
            s.WriteBytes(seed);
        }
    }
}
