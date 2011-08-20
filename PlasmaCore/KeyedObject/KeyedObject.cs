using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class hsKeyedObject : plCreatable {

        /* 
         * In Cyan's Plasma, fpKey is a plKey. However, if we 
         * use a plKey here, that will introduce a circular 
         * reference... Very difficult to deal with in C#. 
         * We shall store a plUoid (weak reference)... :)
         */
        plUoid fpKey;

        public plKey GetKey(plResManager mgr) {
            return mgr.FindKey(fpKey.fLocation, fpKey.fClassType, fpKey.fObjectName);
        }

        public override void Read(hsStream s, plResManager mgr) {
            plKey key = null;
            if (s.Version.IsMystOnline)
                key = mgr.ReadKey(s);
            else
                key = mgr.ReadUoid(s);
            fpKey = key.Uoid;
        }

        public override void Write(hsStream s, plResManager mgr) {
            plKey key = GetKey(mgr);
            if (s.Version.IsMystOnline)
                mgr.WriteKey(s, key);
            else
                mgr.WriteUoid(s, key);
        }
    }

    public class plHexBuffer : hsKeyedObject {

        byte[] fBuffer;

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);
            fBuffer = s.ReadBytes((int)(s.BaseStream.Length - s.BaseStream.Position));
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);
            if (fBuffer != null) 
                s.WriteBytes(fBuffer);
        }
    }
}
