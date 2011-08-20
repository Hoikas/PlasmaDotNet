using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plNetMsgVoice : plNetMessage {

        byte fFlags;
        byte fNumFrames;
        string fVoiceData;
        List<uint> fReceivers = new List<uint>();

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            fFlags = s.ReadByte();
            fNumFrames = s.ReadByte();
            fVoiceData = s.ReadStdString();

            fReceivers.Capacity = (int)s.ReadByte();
            for (int i = 0; i < fReceivers.Capacity; i++)
                fReceivers.Insert(i, s.ReadUInt());
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            s.WriteByte(fFlags);
            s.WriteByte(fNumFrames);
            s.WriteStdString(fVoiceData);

            s.WriteByte((byte)fReceivers.Count);
            foreach (uint r in fReceivers)
                s.WriteUInt(r);
        }
    }
}
