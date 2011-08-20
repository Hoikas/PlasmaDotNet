using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plNetMsgSDLState : plNetMsgStreamedObject {

        bool fIsInitialState = false;
        bool fPersistOnServer = true;
        bool fIsAvatarState = false;

        public plNetMsgSDLState() {
            fFlags |= BitVectorFlags.kNeedsReliableSend;
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            fIsInitialState = s.ReadBool();
            fPersistOnServer = s.ReadBool();
            fIsAvatarState = s.ReadBool();
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            s.WriteBool(fIsInitialState);
            s.WriteBool(fPersistOnServer);
            s.WriteBool(fIsAvatarState);
        }
    }

    public class plNetMsgSDLStateBCast : plNetMsgSDLState { }
}
