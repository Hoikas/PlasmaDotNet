using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnVault2Cli {
        kVault2Cli_PingReply,

        kVault2Cli_PlayerCreateReply,
        kVault2Cli_InitAgeReply,

        kVault2Cli_NodeCreateReply,
        kVault2Cli_NodeFetched,
        kVault2Cli_NodeSaveReply,
        kVault2Cli_NodeDeleteReply,
        kVault2Cli_NodeAddReply,
        kVault2Cli_NodeRemoveReply,
        kVault2Cli_NodeRefsFetched,
        kVault2Cli_NodeFindReply,

        kVault2Cli_NodeChanged,
        kVault2Cli_NodeAdded,
        kVault2Cli_NodeRemoved,
    }

    public class pnVault2Cli_PingReply : plNetStruct {
        public uint fTransID;
        public uint fPingTimeMs;
        public byte[] fPayload;

        protected override ushort MsgID {
            get { return (ushort)pnVault2Cli.kVault2Cli_PingReply; }
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

    public class pnVault2Cli_PlayerCreateReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public uint fPlayerID;
        public string fPlayerName; // Len 40
        public string fShape;      // Len 64
        
        protected override ushort MsgID {
            get { return (ushort)pnVault2Cli.kVault2Cli_PlayerCreateReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fPlayerID = s.ReadUInt();
            fPlayerName = pnHelpers.ReadString(s, 40);
            fShape = pnHelpers.ReadString(s, 64);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            s.WriteUInt(fPlayerID);
            pnHelpers.WriteString(s, fPlayerName, 40);
            pnHelpers.WriteString(s, fShape, 64);
        }
    }
}
