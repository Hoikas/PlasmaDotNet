using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace OpenSSL {
    public class RNG {

        private static bool fSeeded = false;

        public static bool Seeded {
            get { return fSeeded; }
        }

        public static void Cleanup() {
            if (fSeeded) OpenSSL.RAND_cleanup();
            fSeeded = false;
        }

        /// <summary>
        /// Seeds the Pseudo RNG with content from a file
        /// </summary>
        /// <param name="file">File to read</param>
        /// <param name="max_bytes">Number of bytes to read (-1 for the full contents)</param>
        /// <returns>Number of bytes actually read</returns>
        public static int LoadFile(string file, int max_bytes) {
            if (File.Exists(file)) {
                int read = OpenSSL.RAND_load_file(Encoding.ASCII.GetBytes(file), max_bytes);
                fSeeded = true;
                return read;
            } else {
                throw new IOException("The file does not exist.");
            }
        }

        public static byte[] Random(int bytes) {
            if(!fSeeded) Seed();
            byte[] buf = new byte[bytes];
            if (OpenSSL.RAND_bytes(buf, bytes) == 1)
                return buf;
            else {
                uint err = OpenSSL.ERR_get_error();
                throw new OpenSSLException(Encoding.ASCII.GetString(OpenSSL.ERR_get_error_string(err, null)));
            }
        }

        /// <summary>
        /// Adds the content of the screen to the Pseudo RNG
        /// </summary>
        public static void Screen() {
            OpenSSL.RAND_screen();
            fSeeded = true;
        }

        /// <summary>
        /// Seeds the RNG with user data
        /// </summary>
        /// <param name="data">Data to seed the RNG with</param>
        public static void Seed(byte[] data) {
            OpenSSL.RAND_seed(data, data.Length);
            fSeeded = true;
        }

        /// <summary>
        /// Seeds the RNG in a platform independent manner
        /// </summary>
        public static void Seed() {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT || 
                Environment.OSVersion.Platform == PlatformID.Win32S || 
                Environment.OSVersion.Platform == PlatformID.Win32Windows) Screen();
            else if (Environment.OSVersion.Platform == PlatformID.Unix) {
                try {
                    LoadFile("/dev/urandom", 2048);
                } catch (IOException) { } //Silently handle an exception
            }

            // Fall back to the .NET RNG
            // I wrote all this mess before I realized .NET has its own (real) RNG
            if (!fSeeded) {
                byte[] seed = new byte[2048];
                RandomNumberGenerator rng = RandomNumberGenerator.Create();
                rng.GetBytes(seed);
                Seed(seed);
            }
        }
    }
}
