using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public struct hsVector3 {

        float fX, fY, fZ;

        public hsVector3(float x, float y, float z) {
            fX = x;
            fY = y;
            fZ = z;
        }

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
