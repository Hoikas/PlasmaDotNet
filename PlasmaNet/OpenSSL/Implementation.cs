using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenSSL {
    internal static class OpenSSL {

        #region BigNum
        [DllImport("libcrypto")]
        public static extern IntPtr BN_new();

        [DllImport("libcrypto")]
        public static extern void BN_free(IntPtr a);

        [DllImport("libcrypto")]
        public static extern void BN_clear(IntPtr a);

        [DllImport("libcrypto")]
        public static extern void BN_clear_free(IntPtr a);

        //[DllImport("libcrypto")]
        //public static extern int BN_num_bytes(IntPtr a);

        [DllImport("libcrypto")]
        public static extern int BN_num_bits(IntPtr a);

        [DllImport("libcrypto")]
        public static extern int BN_add(IntPtr r, IntPtr a, IntPtr b);

        [DllImport("libcrypto")]
        public static extern int BN_sub(IntPtr r, IntPtr a, IntPtr b);

        [DllImport("libcrypto")]
        public static extern int BN_mul(IntPtr r, IntPtr a, IntPtr b, IntPtr ctx);

        [DllImport("libcrypto")]
        public static extern int BN_div(IntPtr dv, IntPtr rem, IntPtr a, IntPtr d, IntPtr ctx);

        [DllImport("libcrypto")]
        public static extern int BN_mod_exp(IntPtr r, IntPtr a, IntPtr p, IntPtr m, IntPtr ctx);

        [DllImport("libcrypto")]
        public static extern int BN_cmp(IntPtr a, IntPtr b);

        [DllImport("libcrypto")]
        public static extern int BN_rand(IntPtr rnd, int bits, int top, int bottom);

        [DllImport("libcrypto")]
        public static extern IntPtr BN_generate_prime(IntPtr ret, int bits, int safe, IntPtr add, IntPtr rem, PrimeGenerator cb, IntPtr cb_args);

        [DllImport("libcrypto")]
        public static extern int BN_bn2bin(IntPtr a, byte[] to);

        [DllImport("libcrypto")]
        public static extern IntPtr BN_bin2bn(byte[] s, int len, IntPtr ret);

        [DllImport("libcrypto")]
        public static extern IntPtr BN_bn2hex(IntPtr a);

        [DllImport("libcrypto")]
        public static extern IntPtr BN_bn2dec(IntPtr a);

        [DllImport("libcrypto")]
        public static extern int BN_hex2bn(ref IntPtr a, byte[] str);

        [DllImport("libcrypto")]
        public static extern int BN_dec2bn(ref IntPtr a, byte[] str);
        #endregion

        #region BigNum CTX
        [DllImport("libcrypto")]
        public static extern IntPtr BN_CTX_new();

        [DllImport("libcrypto")]
        public static extern IntPtr BN_CTX_free(IntPtr a);
        #endregion

        #region ERR
        [DllImport("libcrypto")]
        public static extern uint ERR_get_error();

        [DllImport("libcrypto")]
        public static extern byte[] ERR_get_error_string(uint e, byte[] buf);
        #endregion

        #region RAND
        [DllImport("libcrypto")]
        public static extern int RAND_bytes(byte[] buf, int num);

        [DllImport("libcrypto")]
        public static extern void RAND_cleanup();

        [DllImport("libcrypto")]
        public static extern int RAND_load_file(byte[] filename, int max_bytes);

        [DllImport("libcrypto")]
        public static extern void RAND_screen();

        [DllImport("libcrypto")]
        public static extern void RAND_seed(byte[] buf, int num);
        #endregion

        #region Misc
        [DllImport("libcrypto")]
        public extern static void CRYPTO_free(IntPtr p);

        [DllImport("libcrypto")]
        public extern static void CRYPTO_malloc_init();
        #endregion

        #region RC4 Encryption
        [DllImport("libcrypto")]
        public static extern void RC4_set_key(IntPtr key, int len, byte[] data);

        [DllImport("libcrypto")]
        public static extern void RC4(IntPtr key, uint len, byte[] indata, byte[] outdata);
        #endregion

        #region SHA Hashing
        [DllImport("libcrypto")]
        public static extern IntPtr SHA(byte[] d, uint n, byte[] md);

        [DllImport("libcrypto")]
        public static extern IntPtr SHA1(byte[] d, uint n, byte[] md);
        #endregion
    }

    public class OpenSSLException : Exception {

        internal OpenSSLException() : base("Generic OpenSSL Exception") { }
        internal OpenSSLException(string message) : base(message) { }
        internal OpenSSLException(string message, Exception innerException) : base(message, innerException) { }
    }
}
