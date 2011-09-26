using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public static class plUnifiedTime {

        /// <summary>
        /// Gets the DateTime of the Unix Epoch
        /// </summary>
        public static DateTime Epoch {
            get { return new DateTime(1970, 1, 1, 0, 0, 0); }
        }

        public static DateTime Read(hsStream s) {
            // Read in the data from the stream
            double secs = (double)s.ReadUInt();
            double micros = (double)s.ReadUInt();

            // Now construct a .NET DateTime
            DateTime dt = Epoch.AddSeconds(secs);
            dt.AddMilliseconds(micros / 100);
            return dt;
        }

        public static void Write(hsStream s, DateTime dt) {
            // Figure out the Seconds and Microseconds from the DateTime
            TimeSpan ts = dt - Epoch;
            uint seconds = (uint)ts.TotalSeconds;
            uint micros = ((uint)(ts.TotalMilliseconds * 100.0d)) - seconds;

            // Now write them to the stream
            s.WriteUInt(seconds);
            s.WriteUInt(micros);
        }
    }
}
