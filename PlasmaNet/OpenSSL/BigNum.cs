using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace OpenSSL {

    public enum PrimeStatus : int {
        Generated = 0,
        Testing = 1,
        Found = 2,
    }

    public delegate void PrimeGenerator(PrimeStatus status, int i, IntPtr cb_arg);

    public class BigNum : IDisposable {

        private IntPtr fBigNum;
        private bool fDisposed = false;

        /// <summary>
        /// Gets a pointer to the internal OpenSSL BIGNUM object
        /// </summary>
        public IntPtr Pointer {
            get { return fBigNum; }
        }

        public BigNum() {
            fBigNum = OpenSSL.BN_new();
        }

        /// <summary>
        /// Creates a new BigNum instance from an array of bytes
        /// </summary>
        /// <param name="data">An array of bytes in OpenSSL byte order (big endian)</param>
        public BigNum(byte[] data) {
            fBigNum = OpenSSL.BN_bin2bn(data, data.Length, IntPtr.Zero);
        }

        public BigNum(int data) {
            fBigNum = OpenSSL.BN_new();
            OpenSSL.BN_dec2bn(ref fBigNum, Encoding.ASCII.GetBytes(Convert.ToString(data)));
        }

        /// <summary>
        /// Creates a new BigNum instance from a decimal string
        /// </summary>
        /// <param name="data">Number in decimal</param>
        public BigNum(string data) {
            fBigNum = OpenSSL.BN_new();
            OpenSSL.BN_dec2bn(ref fBigNum, Encoding.ASCII.GetBytes(data));
        }

        public BigNum(string data, string format) {
            fBigNum = OpenSSL.BN_new();
            if (format.ToLower() == "d")
                OpenSSL.BN_dec2bn(ref fBigNum, Encoding.ASCII.GetBytes(data));
            else if (format.ToLower() == "x")
                OpenSSL.BN_hex2bn(ref fBigNum, Encoding.ASCII.GetBytes(data));
            else
                throw new FormatException();
        }

        private BigNum(IntPtr native) {
            fBigNum = native;
            fDisposed = (native == IntPtr.Zero);
        }

        ~BigNum() {
            if (!fDisposed) Dispose();
        }

        /// <summary>
        /// Clears the BigNum from memory
        /// </summary>
        public void Clear() {
            if (fDisposed)
                throw new ObjectDisposedException("this");

            OpenSSL.BN_clear(fBigNum);
        }

        /// <summary>
        /// Explicitly frees resources consumed by this BigNum instance
        /// </summary>
        public void Dispose() {
            if (!fDisposed) {
                GC.SuppressFinalize(this);
                OpenSSL.BN_free(fBigNum);
                fDisposed = true;
            }
        }

        /// <summary>
        /// Compares the values of two BigNums
        /// </summary>
        /// <param name="obj">BigNum to compare with</param>
        /// <returns>If the two values are equal</returns>
        public override bool Equals(object obj) {
            if (!(obj is BigNum))
                throw new ArgumentException("obj must be a BigNum to compare");
            BigNum cmp = obj as BigNum;

            //Ensure that the BigNums have not been disposed
            if (fDisposed)
                throw new ObjectDisposedException("this");
            if (cmp.fDisposed)
                throw new ObjectDisposedException("cmp");

            return (OpenSSL.BN_cmp(fBigNum, cmp.fBigNum) == 0);
        }

        /// <summary>
        /// Generates a prime number
        /// </summary>
        /// <param name="bits">Number of bits the resulting prime should have</param>
        /// <returns>Generated prime</returns>
        public static BigNum GeneratePrime(int bits) {
            IntPtr res = OpenSSL.BN_new();
            OpenSSL.BN_generate_prime(res, bits, Convert.ToInt32(true), IntPtr.Zero, IntPtr.Zero, null, IntPtr.Zero);
            return new BigNum(res);
        }

        /// <summary>
        /// Raises this BigNum to a power and takes the modulus
        /// </summary>
        /// <param name="exp">The exponent</param>
        /// <param name="mod">Value to use when taking the modulus</param>
        /// <returns>this**exp % mod</returns>
        public BigNum PowMod(BigNum exp, BigNum mod) {
            if (fDisposed)
                throw new ObjectDisposedException("this");
            if (exp.fDisposed)
                throw new ObjectDisposedException("exp");
            if (mod.fDisposed)
                throw new ObjectDisposedException("mod");

            IntPtr r = OpenSSL.BN_new();
            IntPtr ctx = OpenSSL.BN_CTX_new();
            OpenSSL.BN_mod_exp(r, fBigNum, exp.fBigNum, mod.fBigNum, ctx);
            OpenSSL.BN_CTX_free(ctx);
            return new BigNum(r);
        }

        /// <summary>
        /// Replaces the content of the BigNum with a random BigNum
        /// </summary>
        /// <param name="bn">BigNum to replace</param>
        /// <param name="bits">How many bits the generated random number shoud have</param>
        public static void Random(BigNum bn, int bits) {
            if (bn.fDisposed)
                throw new ObjectDisposedException("BigNum");

            RNG.Seed();
            OpenSSL.BN_rand(bn.fBigNum, bits, 1, 1);
        }


        /// <summary>
        /// Generates a random BigNum
        /// </summary>
        /// <param name="bits">How many bits the generated random number shoud have</param>
        /// <returns>Generated random number</returns>
        public static BigNum Random(int bits) {
            RNG.Seed();
            BigNum rand = new BigNum();
            OpenSSL.BN_rand(rand.fBigNum, bits, 1, 1);
            return rand;
        }

        /// <summary>
        /// Returns the BigNum as a buffer
        /// </summary>
        /// <returns>Content of BigNum as byte array in OpenSSL byte order</returns>
        public byte[] ToBigArray() {
            if (fDisposed)
                throw new ObjectDisposedException("this");

            byte[] buf = new byte[(OpenSSL.BN_num_bits(fBigNum) + 7) / 8];
            OpenSSL.BN_bn2bin(fBigNum, buf);
            return buf;
        }

        /// <summary>
        /// Returns the BigNum as a buffer
        /// </summary>
        /// <returns>Content of BigNum as byte array in little endian</returns>
        public byte[] ToLittleArray() {
            if (fDisposed)
                throw new ObjectDisposedException("this");

            byte[] buf = new byte[(OpenSSL.BN_num_bits(fBigNum) + 7) / 8];
            OpenSSL.BN_bn2bin(fBigNum, buf);
            Array.Reverse(buf);
            return buf;
        }

        public override string ToString() {
            return ToString("D");
        }

        public string ToString(string format) {
            if (fDisposed)
                throw new ObjectDisposedException("this");

            IntPtr str = IntPtr.Zero;
            if (format.ToLower() == "d") {
                str = OpenSSL.BN_bn2dec(fBigNum);
            } else if (format.ToLower() == "x") {
                str = OpenSSL.BN_bn2hex(fBigNum);
            } else {
                throw new FormatException();
            }

            string res = Marshal.PtrToStringAnsi(str);
            OpenSSL.CRYPTO_free(str);
            return res;
        }

        #region Operators
        public static BigNum operator +(BigNum left, BigNum right) {
            //Ensure that the BigNums have not been disposed
            if (left.fDisposed)
                throw new ObjectDisposedException("left");
            if (right.fDisposed)
                throw new ObjectDisposedException("right");

            //Actually do the operation
            IntPtr r = OpenSSL.BN_new();
            OpenSSL.BN_add(r, left.fBigNum, right.fBigNum);
            return new BigNum(r);
        }

        public static BigNum operator -(BigNum left, BigNum right) {
            //Ensure that the BigNums have not been disposed
            if (left.fDisposed)
                throw new ObjectDisposedException("left");
            if (right.fDisposed)
                throw new ObjectDisposedException("right");

            //Actually do the operation
            IntPtr r = OpenSSL.BN_new();
            OpenSSL.BN_sub(r, left.fBigNum, right.fBigNum);
            return new BigNum(r);
        }

        public static BigNum operator *(BigNum left, BigNum right) {
            //Ensure that the BigNums have not been disposed
            if (left.fDisposed)
                throw new ObjectDisposedException("left");
            if (right.fDisposed)
                throw new ObjectDisposedException("right");

            //Actually do the operation
            IntPtr r = OpenSSL.BN_new();
            IntPtr ctx = OpenSSL.BN_CTX_new();
            OpenSSL.BN_mul(r, left.fBigNum, right.fBigNum, ctx);
            OpenSSL.BN_CTX_free(ctx);
            return new BigNum(r);
        }

        public static BigNum operator /(BigNum left, BigNum right) {
            //Ensure that the BigNums have not been disposed
            if (left.fDisposed)
                throw new ObjectDisposedException("left");
            if (right.fDisposed)
                throw new ObjectDisposedException("right");

            //Actually do the operation
            IntPtr res = OpenSSL.BN_new();
            IntPtr ctx = OpenSSL.BN_CTX_new();
            OpenSSL.BN_div(res, IntPtr.Zero, left.fBigNum, right.fBigNum, ctx);
            OpenSSL.BN_CTX_free(ctx);
            return new BigNum(res);
        }

        public static bool operator ==(BigNum left, BigNum right) {
            return left.Equals(right);
        }

        public static bool operator !=(BigNum left, BigNum right) {
            return !left.Equals(right);
        }

        public static bool operator >(BigNum left, BigNum right) {
            //Ensure that the BigNums have not been disposed
            if (left.fDisposed)
                throw new ObjectDisposedException("left");
            if (right.fDisposed)
                throw new ObjectDisposedException("right");

            return (OpenSSL.BN_cmp(left.fBigNum, right.fBigNum) == 1);
        }

        public static bool operator <(BigNum left, BigNum right) {
            //Ensure that the BigNums have not been disposed
            if (left.fDisposed)
                throw new ObjectDisposedException("left");
            if (right.fDisposed)
                throw new ObjectDisposedException("right");

            return (OpenSSL.BN_cmp(left.fBigNum, right.fBigNum) == -1);
        }
        #endregion
    }
}
