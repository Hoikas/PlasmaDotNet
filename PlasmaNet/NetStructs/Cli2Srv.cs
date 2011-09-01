using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pnCli2Srv_Connect {
        public ENetProtocol fType;
        public uint fBuildID;
        public EBuildType fBuildType;
        public uint fBranchID;
        public Guid fProductUuid;

        private const ushort kHeaderSize = 31;

        public void Read(hsStream s) {
            fType = (ENetProtocol)s.ReadByte();
            if (s.ReadUShort() != kHeaderSize)
                throw new NotSupportedException();
            fBuildID = s.ReadUInt();
            fBuildType = (EBuildType)s.ReadUInt();
            fBranchID = s.ReadUInt();
            fProductUuid = pnHelpers.ReadUuid(s);
        }

        public void Write(hsStream s) {
            s.WriteByte((byte)fType);
            s.WriteUShort(kHeaderSize);
            s.WriteUInt(fBuildID);
            s.WriteUInt((uint)fBuildType);
            s.WriteUInt(fBranchID);
            pnHelpers.WriteUuid(s, fProductUuid);
        }
    }
}
