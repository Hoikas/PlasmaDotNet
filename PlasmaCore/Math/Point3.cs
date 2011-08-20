using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public struct hsPoint3 {

        float fX, fY, fZ;

        public void Read(hsStream s) {
            fX = s.ReadFloat();
            fY = s.ReadFloat();
            fZ = s.ReadFloat();
        }

        public void Write(hsStream s) {
            s.WriteFloat(fX);
            s.WriteFloat(fY);
            s.WriteFloat(fZ);
        }
    }
}
