using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnGate2Cli {
        kGateKeeper2Cli_PingReply,
        kGateKeeper2Cli_FileSrvIpAddressReply,
        kGateKeeper2Cli_AuthSrvIpAddressReply,
    }

    public class pnGate2Cli_AuthSrvIpAddressReply : plNetStruct {
        public uint fTransID;
        public string fHost;

        protected override object MsgID {
            get { return (ushort)pnGate2Cli.kGateKeeper2Cli_AuthSrvIpAddressReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fHost = pnHelpers.ReadString(s, 24);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            pnHelpers.WriteString(s, fHost, 24);
        }
    }

    public class pnGate2Cli_FileSrvIpAddressReply : plNetStruct {
        public uint fTransID;
        public string fHost;

        protected override object MsgID {
            get { return (ushort)pnGate2Cli.kGateKeeper2Cli_FileSrvIpAddressReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fHost = pnHelpers.ReadString(s, 24);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            pnHelpers.WriteString(s, fHost, 24);
        }
    }

    public class pnGate2Cli_PingReply : plNetStruct {
        public uint fTransID;
        public uint fPingTimeMs;
        public byte[] fPayload;

        protected override object MsgID {
            get { return (ushort)pnGate2Cli.kGateKeeper2Cli_PingReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fPingTimeMs = s.ReadUInt();
            fPayload = s.ReadBytes(s.ReadInt());
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fPingTimeMs);
            s.WriteInt(fPayload.Length);
            s.WriteBytes(fPayload);
        }
    }
}
