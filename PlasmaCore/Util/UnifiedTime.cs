using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plUnifiedTime {

        private uint fSeconds;
        private uint fMicroSecs;

        public bool AtEpoch {
            get { return (DateTime == Epoch); }
        }

        public DateTime DateTime {
            get {
                DateTime dt = Epoch.AddSeconds(Convert.ToDouble(fSeconds));
                dt.AddTicks(Convert.ToInt64(fMicroSecs / 100));
                return dt;
            }

            set {
                TimeSpan ts = (value - Epoch);
                fSeconds = Convert.ToUInt32(ts.TotalSeconds);
                //Too lazy to use microseconds. Maybe later.
            }
        }

        private DateTime Epoch {
            get { return new DateTime(1970, 1, 1, 0, 0, 0); }
        }

        public plUnifiedTime() { this.DateTime = Epoch; }
        public plUnifiedTime(DateTime dt) { this.DateTime = dt; }
        public plUnifiedTime(hsStream s) { Read(s); }

        public void Read(hsStream bs) {
            fSeconds = bs.ReadUInt();
            fMicroSecs = bs.ReadUInt();
        }

        public void Write(hsStream bs) {
            bs.WriteUInt(fSeconds);
            bs.WriteUInt(fMicroSecs);
        }
    }
}
