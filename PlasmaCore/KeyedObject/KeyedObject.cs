using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class hsKeyedObject : plCreatable {

        plKey fpKey;

        public plKey Key {
            get { return fpKey; }
        }

        public override void Read(hsStream s, hsResMgr mgr) {
            plKey key = null;
            if (s.Version.IsMystOnline)
                key = mgr.ReadKey(s);
            else
                key = mgr.ReadUoid(s);
            fpKey = key;
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            if (s.Version.IsMystOnline)
                mgr.WriteKey(s, fpKey);
            else
                mgr.WriteUoid(s, fpKey);
        }
    }

    public class plHexBuffer : hsKeyedObject {

        byte[] fBuffer;

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);
            fBuffer = s.ReadBytes((int)(s.BaseStream.Length - s.BaseStream.Position));
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            base.Write(s, mgr);
            if (fBuffer != null) 
                s.WriteBytes(fBuffer);
        }
    }
}
