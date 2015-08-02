using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnCli2Auth {
        // Global
        kCli2Auth_PingRequest,

        // Client
        kCli2Auth_ClientRegisterRequest,
        kCli2Auth_ClientSetCCRLevel,

        // Account
        kCli2Auth_AcctLoginRequest,
        kCli2Auth_AcctSetEulaVersion,
        kCli2Auth_AcctSetDataRequest,
        kCli2Auth_AcctSetPlayerRequest,
        kCli2Auth_AcctCreateRequest,
        kCli2Auth_AcctChangePasswordRequest,
        kCli2Auth_AcctSetRolesRequest,
        kCli2Auth_AcctSetBillingTypeRequest,
        kCli2Auth_AcctActivateRequest,
        kCli2Auth_AcctCreateFromKeyRequest,

        // Player
        kCli2Auth_PlayerDeleteRequest,
        kCli2Auth_PlayerUndeleteRequest,
        kCli2Auth_PlayerSelectRequest,
        kCli2Auth_PlayerRenameRequest,
        kCli2Auth_PlayerCreateRequest,
        kCli2Auth_PlayerSetStatus,
        kCli2Auth_PlayerChat,
        kCli2Auth_UpgradeVisitorRequest,
        kCli2Auth_SetPlayerBanStatusRequest,
        kCli2Auth_KickPlayer,
        kCli2Auth_ChangePlayerNameRequest,
        kCli2Auth_SendFriendInviteRequest,

        // Vault
        kCli2Auth_VaultNodeCreate,
        kCli2Auth_VaultNodeFetch,
        kCli2Auth_VaultNodeSave,
        kCli2Auth_VaultNodeDelete,
        kCli2Auth_VaultNodeAdd,
        kCli2Auth_VaultNodeRemove,
        kCli2Auth_VaultFetchNodeRefs,
        kCli2Auth_VaultInitAgeRequest,
        kCli2Auth_VaultNodeFind,
        kCli2Auth_VaultSetSeen,
        kCli2Auth_VaultSendNode,

        // Ages
        kCli2Auth_AgeRequest,

        // File-related
        kCli2Auth_FileListRequest,
        kCli2Auth_FileDownloadRequest,
        kCli2Auth_FileDownloadChunkAck,

        // Game
        kCli2Auth_PropagateBuffer,

        // Public ages    
        kCli2Auth_GetPublicAgeList,
        kCli2Auth_SetAgePublic,

        // Log Messages
        kCli2Auth_LogPythonTraceback,
        kCli2Auth_LogStackDump,
        kCli2Auth_LogClientDebuggerConnect,

        // Score
        kCli2Auth_ScoreCreate,
        kCli2Auth_ScoreDelete,
        kCli2Auth_ScoreGetScores,
        kCli2Auth_ScoreAddPoints,
        kCli2Auth_ScoreTransferPoints,
        kCli2Auth_ScoreSetPoints,
        kCli2Auth_ScoreGetRanks,

        kCli2Auth_AccountExistsRequest,
    }

    public class pnCli2Auth_AcctLoginRequest : plNetStruct {
        public uint fTransID;
        public uint fChallenge;
        public string fAccount;   // Len 64
        public byte[] fHash = new byte[20];
        public string fAuthToken; // Len 64
        public string fOS;        // Len 08

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_AcctLoginRequest; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fChallenge = s.ReadUInt();
            fAccount = pnHelpers.ReadString(s, 64);

            // We receive a uint[5] for the Hash... This is not useful.
            Buffer.BlockCopy(IReadHashUInt(s), 0, fHash, 0, 4);
            Buffer.BlockCopy(IReadHashUInt(s), 0, fHash, 4, 4);
            Buffer.BlockCopy(IReadHashUInt(s), 0, fHash, 8, 4);
            Buffer.BlockCopy(IReadHashUInt(s), 0, fHash, 12, 4);
            Buffer.BlockCopy(IReadHashUInt(s), 0, fHash, 16, 4);

            fAuthToken = pnHelpers.ReadString(s, 64);
            fOS = pnHelpers.ReadString(s, 8);
        }

        private byte[] IReadHashUInt(hsStream s) {
            byte[] buf = s.ReadBytes(4);
            Array.Reverse(buf);
            return buf;
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fChallenge);
            pnHelpers.WriteString(s, fAccount, 64);

            // We need to send a uint[5] for the Hash
            for (int i = 0; i < 5; i++)
                IWriteHashUInt(s, i);

            pnHelpers.WriteString(s, fAuthToken, 64);
            pnHelpers.WriteString(s, fOS, 8);
        }

        private void IWriteHashUInt(hsStream s, int i) {
            byte[] buf = new byte[4];
            Buffer.BlockCopy(fHash, i * 4, buf, 0, 4);
            Array.Reverse(buf);
            s.WriteUInt(BitConverter.ToUInt32(buf, 0));
        }
    }

    public class pnCli2Auth_AcctSetPlayerRequest : plNetStruct {
        public uint fTransID;
        public uint fPlayerID;

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_AcctSetPlayerRequest; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fPlayerID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fPlayerID);
        }
    }

    public class pnCli2Auth_ClientRegisterRequest : plNetStruct {
        public uint fBuildID;

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_ClientRegisterRequest; }
        }

        public override void Read(hsStream s) {
            fBuildID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fBuildID);
        }
    }

    public class pnCli2Auth_PingRequest : plNetStruct {
        public uint   fTransID;
        public uint   fPingTimeMs;
        public byte[] fPayload;

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_PingRequest; }
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

    public class pnCli2Auth_PlayerCreateRequest : plNetStruct {
        public uint fTransID;
        public string fPlayerName; // Len  40
        public string fShape;      // Len 260
        public string fInvite;     // Len 260

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_PlayerCreateRequest; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fPlayerName = pnHelpers.ReadString(s, 40);
            fShape = pnHelpers.ReadString(s, 260);
            fInvite = pnHelpers.ReadString(s, 260);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            pnHelpers.WriteString(s, fPlayerName, 40);
            pnHelpers.WriteString(s, fShape, 260);
            pnHelpers.WriteString(s, fInvite, 260);
        }
    }

    public class pnCli2Auth_VaultFetchNodeRefs : plNetStruct {
        public uint fTransID;
        public uint fNodeID;

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_VaultFetchNodeRefs; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fNodeID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fNodeID);
        }
    }

    public class pnCli2Auth_VaultNodeFetch : plNetStruct {
        public uint fTransID;
        public uint fNodeID;

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_VaultNodeFetch; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fNodeID = s.ReadUInt();
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fNodeID);
        }
    }

    public class pnCli2Auth_VaultNodeFind : plNetStruct {
        public uint fTransID;
        public pnVaultNode fPattern;

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_VaultNodeFind; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            if (s.ReadInt() != 0) {
                fPattern = new pnVaultNode();
                fPattern.Read(s);
            }
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            if (fPattern == null)
                s.WriteInt(0);
            else {
                byte[] buf = fPattern.ToArray();
                s.WriteInt(buf.Length);
                s.WriteBytes(buf);
            }
        }
    }

    public class pnCli2Auth_VaultNodeSave : plNetStruct {
        public uint fTransID;
        public uint fNodeID;
        public Guid fRevision;
        public pnVaultNode fNode;

        protected override object MsgID {
            get { return (ushort)pnCli2Auth.kCli2Auth_VaultNodeSave; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fNodeID = s.ReadUInt();
            fRevision = pnHelpers.ReadUuid(s);
            if (s.ReadInt() != 0) {
                fNode = new pnVaultNode();
                fNode.Read(s);
            }
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            s.WriteUInt(fNodeID);
            pnHelpers.WriteUuid(s, fRevision);
            if (fNode == null)
                s.WriteInt(0);
            else {
                byte[] buf = fNode.ToArray();
                s.WriteInt(buf.Length);
                s.WriteBytes(buf);
            }
        }
    }
}
