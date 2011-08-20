using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class plNetMsgServerToClient : plNetMessage {

        public plNetMsgServerToClient() {
            fFlags |= BitVectorFlags.kIsSystemMessage | BitVectorFlags.kNeedsReliableSend;
        }
    }

    public class plNetMsgGroupOwner : plNetMsgServerToClient {

        List<plNetGroupInfo> fGroups = new List<plNetGroupInfo>();

        public plNetMsgGroupOwner() : base() { }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            int count = s.ReadInt();
            for (int i = 0; i < count; i++) {
                plNetGroupInfo g = new plNetGroupInfo();
                g.Read(s);
                fGroups.Add(g);
            }
        }
    }
}
