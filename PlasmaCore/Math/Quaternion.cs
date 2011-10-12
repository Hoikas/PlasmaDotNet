using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public struct hsQuat {

        float fX, fY, fZ, fW;

        public hsQuat(float x, float y, float z, float w) {
            fX = x;
            fY = y;
            fZ = z;
            fW = w;
        }

        public void Read(hsStream s) {
            fX = s.ReadFloat();
            fY = s.ReadFloat();
            fZ = s.ReadFloat();
            fW = s.ReadFloat();
        }

        public void Write(hsStream s) {
            s.WriteFloat(fX);
            s.WriteFloat(fY);
            s.WriteFloat(fZ);
            s.WriteFloat(fW);
        }
    }
}
