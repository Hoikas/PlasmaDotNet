using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plLinkEffectsTriggerPrepMsg : plMessage {
    }

    public class plLinkEffectsTriggerMsg : plMessage {

        int fInvisLevel;
        bool fLeavingAge;
        plKey fLinkKey;
        int fEffects; //Should always be zero
        plKey fLinkAnimKey;

        #region Properties
        public int Invisibility {
            get { return fInvisLevel; }
            set { fInvisLevel = value; }
        }

        public bool LinkOut {
            get { return fLeavingAge; }
            set { fLeavingAge = value; }
        }

        public plKey LinkKey {
            get { return fLinkKey; }
            set { fLinkKey = value; }
        }

        public plKey Animation {
            get { return fLinkAnimKey; }
            set { fLinkAnimKey = value; }
        }
        #endregion

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fInvisLevel = s.ReadInt();
            fLeavingAge = s.ReadBool();
            fLinkKey = mgr.ReadKey(s);
            fEffects = s.ReadInt();
            fLinkAnimKey = mgr.ReadKey(s);
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            base.Write(s, mgr);

            s.WriteInt(fInvisLevel);
            s.WriteBool(fLeavingAge);
            mgr.WriteKey(s, fLinkKey);
            s.WriteInt(fEffects);
            mgr.WriteKey(s, fLinkAnimKey);
        }
    }
}
