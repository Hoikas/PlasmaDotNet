using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnVault2Cli {
        kVault2Cli_PingReply,

        kVault2Cli_AcctLoginReply,
        kVault2Cli_PlayerCreateReply,
        kVault2Cli_PlayerSetReply,
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

    public class pnVaultAvatarInfo {
        public uint fPlayerID;
        public string fPlayerName; // Len 40
        public string fModel;      // Len 64

        public void Read(hsStream s) {
            fPlayerID = s.ReadUInt();
            fPlayerName = pnHelpers.ReadString(s, 40);
            fModel = pnHelpers.ReadString(s, 64);
        }

        public void Write(hsStream s) {
            s.WriteUInt(fPlayerID);
            pnHelpers.WriteString(s, fPlayerName, 40);
            pnHelpers.WriteString(s, fModel, 64);
        }
    }

    public class pnVault2Cli_AcctLoginReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public Guid fAcctGuid;
        public int fPermissions; // Your responsibility to interpret this level.
        public pnVaultAvatarInfo[] fAvatars;

        protected override object MsgID {
            get { return (ushort)pnVault2Cli.kVault2Cli_AcctLoginReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fAcctGuid = pnHelpers.ReadUuid(s);
            fPermissions = s.ReadInt();
            fAvatars = new pnVaultAvatarInfo[s.ReadInt()];
            for (int i = 0; i < fAvatars.Length; i++) {
                fAvatars[i] = new pnVaultAvatarInfo();
                fAvatars[i].Read(s);
            }
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            pnHelpers.WriteUuid(s, fAcctGuid);
            s.WriteInt(fPermissions);
            if (fAvatars == null)
                s.WriteInt(0);
            else {
                s.WriteInt(fAvatars.Length);
                foreach (pnVaultAvatarInfo a in fAvatars)
                    a.Write(s);
            }
        }
    }

    public class pnVault2Cli_NodeFetched : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public pnVaultNode fNode;

        protected override object MsgID {
            get { return (ushort)pnVault2Cli.kVault2Cli_NodeFetched; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();

            // I'm not eap, so just embed the vault node into the stream
            // Forget crazy buffer hacks.
            if (s.ReadBool()) {
                fNode = new pnVaultNode();
                fNode.Read(s);
            }
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            s.WriteBool(fNode != null);
            if (fNode != null)
                fNode.Write(s);
        }
    }

    public class pnVault2Cli_NodeFindReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public uint[] fNodeIDs;

        protected override object MsgID {
            get { return (ushort)pnVault2Cli.kVault2Cli_NodeFindReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fNodeIDs = new uint[s.ReadInt()];
            for (int i = 0; i < fNodeIDs.Length; i++)
                fNodeIDs[i] = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            if (fNodeIDs == null)
                s.WriteInt(0);
            else {
                s.WriteInt(fNodeIDs.Length);
                for (int i = 0; i < fNodeIDs.Length; i++)
                    s.WriteUInt(fNodeIDs[i]);
            }
        }
    }

    public class pnVault2Cli_NodeRefsFetched : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public pnVaultNodeRef[] fNodeRefs;

        protected override object MsgID {
            get { return (ushort)pnVault2Cli.kVault2Cli_NodeRefsFetched; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fNodeRefs = new pnVaultNodeRef[s.ReadInt()];
            for (int i = 0; i < fNodeRefs.Length; i++) {
                fNodeRefs[i] = new pnVaultNodeRef();
                fNodeRefs[i].Read(s);
            }
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            if (fNodeRefs == null)
                s.WriteInt(0);
            else {
                s.WriteInt(fNodeRefs.Length);
                foreach (pnVaultNodeRef r in fNodeRefs)
                    r.Write(s);
            }
        }
    }

    public class pnVault2Cli_PingReply : plNetStruct {
        public uint fTransID;
        public uint fPingTimeMs;
        public byte[] fPayload;

        protected override object MsgID {
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
        
        protected override object MsgID {
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

    public class pnVault2Cli_PlayerSetReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;

        protected override object MsgID {
            get { return (ushort)pnVault2Cli.kVault2Cli_PlayerSetReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
        }
    }
}
