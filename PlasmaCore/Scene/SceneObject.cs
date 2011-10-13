using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plSceneObject : plSynchedObject {

        plKey fDrawInterface;
        plKey fSimulationInterface;
        plKey fCoordInterface;
        plKey fAudioInterface;
        List<plKey> fInterfaces = new List<plKey>();
        List<plKey> fModifiers = new List<plKey>();
        plKey fSceneNode;

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fDrawInterface = mgr.ReadKey(s);
            fSimulationInterface = mgr.ReadKey(s);
            fCoordInterface = mgr.ReadKey(s);
            fAudioInterface = mgr.ReadKey(s);

            fInterfaces.Capacity = s.ReadInt();
            for (int i = 0; i < fInterfaces.Capacity; i++)
                fInterfaces.Add(mgr.ReadKey(s));

            fModifiers.Capacity = s.ReadInt();
            for (int i = 0; i < fModifiers.Capacity; i++)
                fModifiers.Add(mgr.ReadKey(s));

            fSceneNode = mgr.ReadKey(s);
        }
    }
}
