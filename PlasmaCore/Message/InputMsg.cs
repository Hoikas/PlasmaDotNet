using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plAvatarInputStateMsg : plMessage {

        ushort fState;

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);
            fState = s.ReadUShort();
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);
            s.WriteUShort(fState);
        }
    }
}
