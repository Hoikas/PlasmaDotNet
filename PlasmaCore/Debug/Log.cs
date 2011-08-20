using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {

    /// <summary>
    /// Implements a common debug logger
    /// </summary>
    /// <remarks>
    /// This class is thread safe.
    /// </remarks>
    public class plDebugLog {

        const int kLogDebug = 0;
        const int kLogWarn = 1;
        const int kLogError = 2;

        static Dictionary<string, plDebugLog> fWriters = new Dictionary<string, plDebugLog>();
        static int fDebugLevel;

        TextWriter fWriter;
        string fLogPath;

        /// <summary>
        /// Gets (or creates) a debug log
        /// </summary>
        /// <param name="index">Name of the log file</param>
        /// <returns>Instance of plDebugLog</returns>
        public static plDebugLog GetLog(string index) {
            lock (fWriters) {
                if (fWriters.ContainsKey(index)) return fWriters[index];

                plDebugLog log = new plDebugLog(index);
                fWriters.Add(index, log);
                return log;
            }
        }

        /// <summary>
        /// Gets or sets the verbosity of the logs
        /// </summary>
        public static int LogLevel {
            get { return fDebugLevel; }
            set { fDebugLevel = value; }
        }

        private string LogNow {
            get { return DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss"); }
        }

        private plDebugLog(string name) {
            if (!Directory.Exists("log")) Directory.CreateDirectory("log");
            fLogPath = Path.Combine("log", name + ".log");
        }

        private void IInit() {
            if (fWriter != null) return;

            lock (this) {
                if (fLogPath != null) {
                    fWriter = TextWriter.Synchronized(new StreamWriter(fLogPath));
                    fLogPath = null;
                }
            }
        }

        public void Debug(string line) {
            if (fDebugLevel > kLogDebug) return;
            IInit();
            IWriteLine(line);
        }

        public void Error(string line) {
            if (fDebugLevel > kLogError) return;
            IInit();
            IWriteLine(line);
        }

        public void Error(Exception e) {
            if (fDebugLevel > kLogError) return;
            IInit();

            IWriteLine("----UNHANDLED EXCEPTION----" + fWriter.NewLine + e.ToString());
            fWriter.Flush();
        }

        public void Error(Exception e, string msg) {
            if (fDebugLevel > kLogError) return;
            IInit();

            IWriteLine(msg + fWriter.NewLine + e.ToString());
            fWriter.Flush();
        }

        public void Warn(string line) {
            if (fDebugLevel > kLogWarn) return;
            IInit();
            IWriteLine(line);
        }

        private void IWriteLine(string line) {
            string msg = String.Format("[{0}] {1}", LogNow, line);
            fWriter.WriteLine(msg);
            fWriter.Flush();
        }
    }
}
