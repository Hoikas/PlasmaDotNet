using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plSceneNode : hsKeyedObject {

        List<plKey> fObjectPool = new List<plKey>();
        List<plKey> fGenericPool = new List<plKey>();

        public List<plKey> SceneObjects {
            get { return fObjectPool; }
        }

        public List<plKey> Pool {
            get { return fGenericPool; }
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            fObjectPool.Capacity = s.ReadInt();
            for (int i = 0; i < fObjectPool.Capacity; i++)
                fObjectPool.Add(mgr.ReadKey(s));

            fGenericPool.Capacity = s.ReadInt();
            for (int i = 0; i < fGenericPool.Capacity; i++)
                fGenericPool.Add(mgr.ReadKey(s));
        }
    }
}
