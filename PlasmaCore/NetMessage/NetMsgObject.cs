using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class plNetMsgObject : plNetMessage {

        plKey fObjectHelper;

        public plKey Object {
            get { return fObjectHelper; }
            set { fObjectHelper = value; }
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);
            fObjectHelper = mgr.ReadUoid(s);
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);
            mgr.WriteUoid(s, fObjectHelper);
        }
    }

    public abstract class plNetMsgStreamedObject : plNetMsgObject {

        protected plNetMsgStreamHelper fHelper = new plNetMsgStreamHelper();

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);
            fHelper.Read(s, mgr);
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);
            fHelper.Write(s, mgr);
        }
    }
}
