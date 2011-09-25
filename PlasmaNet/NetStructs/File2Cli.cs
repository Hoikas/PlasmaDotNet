using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnFile2Cli {
        // Global
        kFile2Cli_PingReply = 0,

        // File server-related
        kFile2Cli_BuildIdReply = 10,
        kFile2Cli_BuildIdUpdate = 11,
        // 12 through 19 skipped

        // Cache-related
        kFile2Cli_ManifestReply = 20,
        kFile2Cli_FileDownloadReply = 21,
        // 22 through 29 skipped
    }

    public class pnFile2Cli_BuildIdReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public uint fBuildID;

        protected override object MsgID {
            get { return (uint)pnFile2Cli.kFile2Cli_BuildIdReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fBuildID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            s.WriteUInt(fBuildID);
        }
    }

    public class pnFile2Cli_PingReply : plNetStruct {
        public uint fPingTimeMs;

        protected override object MsgID {
            get { return (uint)pnFile2Cli.kFile2Cli_PingReply; }
        }

        public override void Read(hsStream s) {
            fPingTimeMs = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fPingTimeMs);
        }
    }
}
