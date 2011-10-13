using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plNetMsgLoadClone : plNetMsgGameMessage {

        plKey fObjectHelper;
        bool fIsPlayer, fIsLoading, fIsInitialState;

        #region Properties
        public plKey Clone {
            get { return fObjectHelper; }
            set { fObjectHelper = value; }
        }

        public bool IsInitialState {
            get { return fIsInitialState; }
            set { fIsInitialState = value; }
        }

        public bool IsLoading {
            get { return fIsLoading; }
            set { fIsLoading = value; }
        }

        public bool IsPlayer {
            get { return fIsPlayer; }
            set { fIsPlayer = value; }
        }
        #endregion

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fObjectHelper = mgr.ReadUoid(s); // Yes, ReadUoid. Not ReadKey.
            fIsPlayer = s.ReadBool();
            fIsLoading = s.ReadBool();
            fIsInitialState = s.ReadBool();
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            base.Write(s, mgr);

            mgr.WriteUoid(s, fObjectHelper);
            s.WriteBool(fIsPlayer);
            s.WriteBool(fIsLoading);
            s.WriteBool(fIsInitialState);
        }
    }
}
