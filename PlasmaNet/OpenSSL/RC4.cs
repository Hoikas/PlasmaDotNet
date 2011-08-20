using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace OpenSSL {
    public class RC4 : IDisposable{

        [StructLayout(LayoutKind.Sequential)]
        struct RC4_KEY {
            public byte x, y;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] data;
        }

        private IntPtr fKey;
        private bool fDisposed;

        public bool Disposed {
            get { return fDisposed; }
        }

        public RC4(byte[] key) {
            RC4_KEY mkey = new RC4_KEY();
            fKey = Marshal.AllocHGlobal(258);
            Marshal.StructureToPtr(mkey, fKey, false);

            OpenSSL.RC4_set_key(fKey, key.Length, key);
        }

        ~RC4() { if (!fDisposed) Dispose(); }

        public void Dispose() {
            if (fDisposed) throw new ObjectDisposedException("fKey");

            GC.SuppressFinalize(this);
            try {
                OpenSSL.CRYPTO_free(fKey);
            } catch (AccessViolationException) { }
            fDisposed = true;
        }

        public byte[] Transform(byte[] inbuf) {
            if (fDisposed) throw new ObjectDisposedException("fKey");

            byte[] outbuf = new byte[inbuf.Length];
            OpenSSL.RC4(fKey, (uint)inbuf.Length, inbuf, outbuf);
            return outbuf;
        }

        public void Transform(byte[] buffer, int offset, int size) {
            if (fDisposed) throw new ObjectDisposedException("fKey");

            byte[] inbuf = new byte[size];
            byte[] outbuf = new byte[size];

            Buffer.BlockCopy(buffer, offset, inbuf, 0, size);
            OpenSSL.RC4(fKey, (uint)size, inbuf, outbuf);
            Buffer.BlockCopy(outbuf, 0, buffer, offset, size);
        }
    }
}
