using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSSL {
    public static class Hash {

        public static byte[] SHA0(byte[] buffer) {
            byte[] hash = new byte[20];
            OpenSSL.SHA(buffer, (uint)buffer.Length, hash);
            return hash;
        }

        public static byte[] SHA1(byte[] buffer) {
            byte[] hash = new byte[20];
            OpenSSL.SHA1(buffer, (uint)buffer.Length, hash);
            return hash;
        }
    }
}
