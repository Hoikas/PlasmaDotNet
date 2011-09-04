using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnCli2File {
        // Global
        kCli2File_PingRequest = 0,

        // File server-related
        kCli2File_BuildIdRequest = 10,
        // 11 through 19 skipped

        // Cache-related
        kCli2File_ManifestRequest = 20,
        kCli2File_FileDownloadRequest = 21,
        kCli2File_ManifestEntryAck = 22,
        kCli2File_FileDownloadChunkAck = 23,
        // 24 through 29 skipped

        kCli2File_UNUSED_1 = 30,
    }

    public class pnCli2File_PingRequest : plNetStruct {
        public uint fPingTimeMs;

        protected override object MsgID {
            get { return (uint)pnCli2File.kCli2File_PingRequest; }
        }

        public override void Read(hsStream s) {
            fPingTimeMs = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fPingTimeMs);
        }
    }
}
