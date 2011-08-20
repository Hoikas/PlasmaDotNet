using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plNetMsgMembersListReq : plNetMessage {

        public plNetMsgMembersListReq() {
            fFlags |= BitVectorFlags.kIsSystemMessage | BitVectorFlags.kNeedsReliableSend;
        }
    }

    public class plNetMsgMembersList : plNetMsgServerToClient {

        List<plNetMsgMemberInfoHelper> fMembers = new List<plNetMsgMemberInfoHelper>();

        public List<plNetMsgMemberInfoHelper> Members {
            get { return fMembers; }
        }

        public plNetMsgMembersList() : base() { }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            fMembers.Capacity = (int)s.ReadShort();
            for (int i = 0; i < fMembers.Capacity; i++) {
                plNetMsgMemberInfoHelper info = new plNetMsgMemberInfoHelper();
                info.Read(s, mgr);
                fMembers.Add(info);
            }
        }
    }

    public class plNetMsgMemberInfoHelper : plCreatable {

        uint fFlags;
        plClientGuid fClientGuid = new plClientGuid();
        plKey fAvatarUoid;

        public plKey Avatar {
            get { return fAvatarUoid; }
            set { fAvatarUoid = value; }
        }

        public plClientGuid Client {
            get { return fClientGuid; }
            set { fClientGuid = value; }
        }

        public override void Read(hsStream s, plResManager mgr) {
            fFlags = s.ReadUInt();
            fClientGuid.Read(s, mgr);
            fAvatarUoid = mgr.ReadUoid(s);
        }

        public override void Write(hsStream s, plResManager mgr) {
            s.WriteUInt(fFlags);
            fClientGuid.Write(s, mgr);
            mgr.WriteUoid(s, fAvatarUoid);
        }
    }
}
