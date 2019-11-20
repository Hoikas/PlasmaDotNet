using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Plasma {
    public enum pnAuth2Cli {
        // Global
        kAuth2Cli_PingReply,
        kAuth2Cli_ServerAddr,
        kAuth2Cli_NotifyNewBuild,

        // Client
        kAuth2Cli_ClientRegisterReply,

        // Account
        kAuth2Cli_AcctLoginReply,
        kAuth2Cli_AcctData,
        kAuth2Cli_AcctPlayerInfo,
        kAuth2Cli_AcctSetPlayerReply,
        kAuth2Cli_AcctCreateReply,
        kAuth2Cli_AcctChangePasswordReply,
        kAuth2Cli_AcctSetRolesReply,
        kAuth2Cli_AcctSetBillingTypeReply,
        kAuth2Cli_AcctActivateReply,
        kAuth2Cli_AcctCreateFromKeyReply,

        // Player
        kAuth2Cli_PlayerList,
        kAuth2Cli_PlayerChat,
        kAuth2Cli_PlayerCreateReply,
        kAuth2Cli_PlayerDeleteReply,
        kAuth2Cli_UpgradeVisitorReply,
        kAuth2Cli_SetPlayerBanStatusReply,
        kAuth2Cli_ChangePlayerNameReply,
        kAuth2Cli_SendFriendInviteReply,

        // Friends
        kAuth2Cli_FriendNotify,

        // Vault
        kAuth2Cli_VaultNodeCreated,
        kAuth2Cli_VaultNodeFetched,
        kAuth2Cli_VaultNodeChanged,
        kAuth2Cli_VaultNodeDeleted,
        kAuth2Cli_VaultNodeAdded,
        kAuth2Cli_VaultNodeRemoved,
        kAuth2Cli_VaultNodeRefsFetched,
        kAuth2Cli_VaultInitAgeReply,
        kAuth2Cli_VaultNodeFindReply,
        kAuth2Cli_VaultSaveNodeReply,
        kAuth2Cli_VaultAddNodeReply,
        kAuth2Cli_VaultRemoveNodeReply,

        // Ages
        kAuth2Cli_AgeReply,

        // File-related
        kAuth2Cli_FileListReply,
        kAuth2Cli_FileDownloadChunk,

        // Game
        kAuth2Cli_PropagateBuffer,

        // Admin
        kAuth2Cli_KickedOff,

        // Public ages    
        kAuth2Cli_PublicAgeList,

        // Score
        kAuth2Cli_ScoreCreateReply,
        kAuth2Cli_ScoreDeleteReply,
        kAuth2Cli_ScoreGetScoresReply,
        kAuth2Cli_ScoreAddPointsReply,
        kAuth2Cli_ScoreTransferPointsReply,
        kAuth2Cli_ScoreSetPointsReply,
        kAuth2Cli_ScoreGetRanksReply,

        kAuth2Cli_AccountExistsReply,
    }

    public class pnAuth2Cli_AcctLoginReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public Guid fAcctGuid;
        public uint fFlags;
        public uint fBillingType;
        public uint[] fDroidKey;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_AcctLoginReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fAcctGuid = pnHelpers.ReadUuid(s);
            fFlags = s.ReadUInt();
            fBillingType = s.ReadUInt();
            fDroidKey = new uint[4];
            for (int i = 0; i < fDroidKey.Length; i++)
                fDroidKey[i] = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            pnHelpers.WriteUuid(s, fAcctGuid);
            s.WriteUInt(fFlags);
            s.WriteUInt(fBillingType);
            if (fDroidKey == null) fDroidKey = new uint[4];
            for (int i = 0; i < fDroidKey.Length; i++)
                s.WriteUInt(fDroidKey[i]);
        }
    }

    public class pnAuth2Cli_AcctPlayerInfo : plNetStruct {
        public uint fTransID;
        public uint fPlayerID;
        public string fPlayerName; // Len 40
        public string fModel;      // Len 64
        public uint fExplorer;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_AcctPlayerInfo; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fPlayerID = s.ReadUInt();
            fPlayerName = pnHelpers.ReadString(s, 40);
            fModel = pnHelpers.ReadString(s, 64);
            fExplorer = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fPlayerID);
            pnHelpers.WriteString(s, fPlayerName, 40);
            pnHelpers.WriteString(s, fModel, 64);
            s.WriteUInt(fExplorer);
        }
    }

    public class pnAuth2Cli_AcctSetPlayerReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_AcctSetPlayerReply; }
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

    public class pnAuth2Cli_ClientRegisterReply : plNetStruct {
        public uint fChallenge;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_ClientRegisterReply; }
        }

        public override void Read(hsStream s) {
            fChallenge = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fChallenge);
        }
    }

    public class pnAuth2Cli_FileListReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public byte[] fData;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_FileListReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            int size = s.ReadInt() * 2;
            if (size != 0)
                fData = s.ReadBytes(size);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            if (fData == null || fData.Length == 0) {
                s.WriteInt(0);
            } else {
                s.WriteInt(fData.Length / 2);
                s.WriteBytes(fData);
            }
        }
    }

    public class pnAuth2Cli_KickedOff : plNetStruct {
        public ENetError fReason;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_KickedOff; }
        }

        public override void Read(hsStream s) {
            fReason = (ENetError)s.ReadInt();
        }

        public override void Write(hsStream s) {
            s.WriteInt((int)fReason);
        }
    }

    public class pnAuth2Cli_PingReply : plNetStruct {
        public uint fTransID;
        public uint fPingTimeMs;
        public byte[] fPayload;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_PingReply; }
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

    public class pnAuth2Cli_PlayerCreateReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public uint fPlayerID;
        public uint fFlags;
        public string fPlayerName; // Len 40
        public string fShape;      // Len 64

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_PlayerCreateReply; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fPlayerID = s.ReadUInt();
            fFlags = s.ReadUInt();
            fPlayerName = pnHelpers.ReadString(s, 40);
            fShape = pnHelpers.ReadString(s, 64);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            s.WriteUInt(fPlayerID);
            s.WriteUInt(fFlags);
            pnHelpers.WriteString(s, fPlayerName, 40);
            pnHelpers.WriteString(s, fShape, 64);
        }
    }

    public class pnAuth2Cli_ServerAddr : plNetStruct {
        public IPAddress fAddress;
        public Guid fToken;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_ServerAddr; }
        }

        public override void Read(hsStream s) {
            int addr = s.ReadInt();
            //fAddress = new IPAddress(addr);
            fToken = pnHelpers.ReadUuid(s);
        }

        public override void Write(hsStream s) {
            s.WriteInt((int)fAddress.Address); // Ugh, Cyan
            pnHelpers.WriteUuid(s, fToken);
        }
    }

    public class pnAuth2Cli_VaultAddNodeReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultAddNodeReply; }
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

    public class pnAuth2Cli_VaultNodeAdded : plNetStruct {
        public uint fParentID;
        public uint fChildID;
        public uint fSaverID;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultNodeAdded; }
        }

        public override void Read(hsStream s) {
            fParentID = s.ReadUInt();
            fChildID = s.ReadUInt();
            fSaverID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fParentID);
            s.WriteUInt(fChildID);
            s.WriteUInt(fSaverID);
        }
    }

    public class pnAuth2Cli_VaultNodeChanged : plNetStruct {
        public uint fNodeID;
        public Guid fRevision;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultNodeChanged; }
        }

        public override void Read(hsStream s) {
            fNodeID = s.ReadUInt();
            fRevision = pnHelpers.ReadUuid(s);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fNodeID);
            pnHelpers.WriteUuid(s, fRevision);
        }
    }

    public class pnAuth2Cli_VaultNodeCreated : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public uint fNodeID;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultNodeCreated; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            fNodeID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            s.WriteUInt(fNodeID);
        }
    }

    public class pnAuth2Cli_VaultNodeFetched : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public pnVaultNode fNode;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultNodeFetched; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fResult = (ENetError)s.ReadInt();
            if (s.ReadInt() != 0) { // Vault Node buffer size
                fNode = new pnVaultNode();
                fNode.Read(s);
            }
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteInt((int)fResult);
            if (fNode == null)
                s.WriteInt(0);
            else {
                byte[] buf = fNode.ToArray();
                s.WriteInt(buf.Length);
                s.WriteBytes(buf);
            }
        }
    }

    public class pnAuth2Cli_VaultNodeRefsFetched : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public pnVaultNodeRef[] fNodeRefs;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultNodeRefsFetched; }
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

    public class pnAuth2Cli_VaultNodeFindReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;
        public uint[] fNodeIDs;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultNodeFindReply; }
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
                foreach (uint nodeID in fNodeIDs)
                    s.WriteUInt(nodeID);
            }
        }
    }

    public class pnAuth2Cli_VaultSaveNodeReply : plNetStruct {
        public uint fTransID;
        public ENetError fResult;

        protected override object MsgID {
            get { return (ushort)pnAuth2Cli.kAuth2Cli_VaultSaveNodeReply; }
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
