using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plNetMsgGameMessage : plNetMsgStream {

        plUnifiedTime fDeliveryTime;

        public plUnifiedTime DeliveryTime {
            get { return fDeliveryTime; }
            set { fDeliveryTime = value; }
        }

        public plNetMsgGameMessage() {
            fFlags |= BitVectorFlags.kNeedsReliableSend;
        }
        
        public plMessage GetMessage(plResManager mgr) {
            hsStream s = fHelper.Stream;
            if (s == null || s.BaseStream.Length == 0)
                return null;

            plCreatable pCre = mgr.ReadCreatable(s);
            s.Close();

            if (pCre is plMessage)
                return (plMessage)pCre;
            else
                return null;
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            if (s.ReadBool()) {
                fDeliveryTime = new plUnifiedTime();
                fDeliveryTime.Read(s);
            }
        }

        public void SetMessage(plMessage msg, plResManager mgr) {
            hsStream s = new hsStream(new MemoryStream());
            s.Version = mgr.Version;
            if (msg != null)
                mgr.WriteCreatable(s, msg);
            fHelper.Stream = s;
            s.Close();

            //Fill in the flags
            //TODO: CCR -> AllPlayers
            InterAgeRouting = msg.InterAgeRouting;
            UseRelRegions = msg.UseRelRegions;
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            s.WriteBool(fDeliveryTime != null);
            if (fDeliveryTime != null)
                fDeliveryTime.Write(s);
        }
    }

    public class plNetMsgGameMessageDirected : plNetMsgGameMessage {

        List<uint> fPlayerIDs = new List<uint>();

        public List<uint> PlayerIDs {
            get { return fPlayerIDs; }
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            byte count = s.ReadByte();
            for (byte i = 0; i < count; i++)
                fPlayerIDs.Add(s.ReadUInt());
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            s.WriteByte((byte)fPlayerIDs.Count);
            foreach (uint playerID in fPlayerIDs)
                s.WriteUInt(playerID);
        }
    }
}
