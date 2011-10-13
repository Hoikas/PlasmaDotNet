using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {

    public enum SessionTypes {
        kAgent = 1, kLobby, kGame, kVault,
        kAuth, kAdmin, kLookup, kClient,
        kUnknown
    }

    public class plNetMsgPing : plNetMessage {

        double fSecsRunning;
        SessionTypes fSession;

        public plNetMsgPing() {
            fFlags |= BitVectorFlags.kIsSystemMessage;
        }

        public override void Read(hsStream s, hsResMgr mgr) {
            base.Read(s, mgr);

            fSecsRunning = s.ReadDouble();
            fSession = (SessionTypes)s.ReadByte();
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            base.Write(s, mgr);

            s.WriteDouble(fSecsRunning);
            s.WriteByte((byte)fSession);
        }
    }
}
