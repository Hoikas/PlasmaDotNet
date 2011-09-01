using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pnCli2Srv_Connect {
        public ENetProtocol fType;
        public uint fBuildID;
        public uint fProtocolVer = plNetCore.Version;
        public uint fBranchID;
        public Guid fProductUuid;

        public const ushort kHeaderSize = 31;

        public void Read(hsStream s) {
            fType = (ENetProtocol)s.ReadByte();
            if (s.ReadUShort() != kHeaderSize)
                throw new NotSupportedException();
            fBuildID = s.ReadUInt();
            fProtocolVer = s.ReadUInt();
            if (fProtocolVer < 50)
                fProtocolVer = 50; // <= 50 are the old "Build Type" values...
            fBranchID = s.ReadUInt();
            fProductUuid = pnHelpers.ReadUuid(s);
        }

        public void Write(hsStream s) {
            s.WriteByte((byte)fType);
            s.WriteUShort(kHeaderSize);
            s.WriteUInt(fBuildID);
            s.WriteUInt(fProtocolVer);
            s.WriteUInt(fBranchID);
            pnHelpers.WriteUuid(s, fProductUuid);
        }
    }

    public abstract class plNetStruct {

        private uint fProtocolVer = plNetCore.Version;

        /// <summary>
        /// Gets or sets the Protocol Version for this NetStruct.
        /// </summary>
        /// <remarks>
        /// Defaults to plNetCore.Version
        /// </remarks>
        /// <seealso cref="plNetCore.Version"/>
        public uint Version {
            get { return fProtocolVer; }
            set { fProtocolVer = value; }
        }

        // Implementation details...
        protected abstract ushort MsgID { get; }
        public abstract void Read(hsStream s);
        public abstract void Write(hsStream s);

        /// <summary>
        /// Sends a NetStruct over some (network backed) stream
        /// </summary>
        /// <param name="s">Stream to write to</param>
        public void Send(plBufferedStream s) {
            s.WriteUShort(MsgID);
            Write(s);
            s.Flush();
        }
    }
}
