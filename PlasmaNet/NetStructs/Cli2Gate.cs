using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnCli2Gate {
        kCli2GateKeeper_PingRequest,
        kCli2GateKeeper_FileSrvIpAddressRequest,
        kCli2GateKeeper_AuthSrvIpAddressRequest,
    }

    public class pnCli2Gate_AuthSrvIpAddressRequest : plNetStruct {
        public uint fTransID;

        protected override object MsgID {
            get { return (ushort)pnCli2Gate.kCli2GateKeeper_AuthSrvIpAddressRequest; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
        }
    }

    public class pnCli2Gate_FileSrvIpAddressRequest : plNetStruct {
        public uint fTransID;
        public bool fIsPatcher; // Really, who cares?

        protected override object MsgID {
            get { return (ushort)pnCli2Gate.kCli2GateKeeper_FileSrvIpAddressRequest; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fIsPatcher = s.ReadBool();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteBool(fIsPatcher);
        }
    }

    public class pnCli2Gate_PingRequest : plNetStruct {
        public uint fTransID;
        public uint fPingTimeMs;
        public byte[] fPayload;

        protected override object MsgID {
            get { return (ushort)pnCli2Gate.kCli2GateKeeper_PingRequest; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fPingTimeMs = s.ReadUInt();
            fPayload = s.ReadBytes(s.ReadInt());
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fPingTimeMs);
            if (fPayload == null)
                s.WriteInt(0);
            else {
                s.WriteInt(fPayload.Length);
                s.WriteBytes(fPayload);
            }
        }
    }
}
