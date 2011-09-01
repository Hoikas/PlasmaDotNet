using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnCli2Vault {
        kCli2Vault_PingRequest,

        kCli2Vault_PlayerCreateRequest,
        kCli2Vault_InitAgeRequest,

        kCli2Vault_NodeCreate,
        kCli2Vault_NodeFetch,
        kCli2Vault_NodeSave,
        kCli2Vault_NodeDelete,
        kCli2Vault_NodeAdd,
        kCli2Vault_NodeRemove,
        kCli2Vault_FetchNodeRefs,
        kCli2Vault_NodeFind,
    }

    public class pnCli2Vault_PingRequest : plNetStruct {
        public uint fTransID;
        public uint fPingTimeMs;
        public byte[] fPayload;

        protected override ushort MsgID {
            get { return (ushort)pnCli2Vault.kCli2Vault_PingRequest; }
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

    public class pnCli2Vault_PlayerCreateRequest : plNetStruct {
        public uint fTransID;
        public Guid fAcctGuid;
        public string fPlayerName; // Len  40
        public string fShape;      // Len  64

        protected override ushort MsgID {
            get { return (ushort)pnCli2Vault.kCli2Vault_PlayerCreateRequest; }
        }

        public override void Read(hsStream s) {
            fTransID = s.ReadUInt();
            fAcctGuid = pnHelpers.ReadUuid(s);
            fPlayerName = pnHelpers.ReadString(s, 40);
            fShape = pnHelpers.ReadString(s, 64);
        }

        public override void Write(hsStream s) {
            s.WriteUInt(fTransID);
            pnHelpers.WriteUuid(s, fAcctGuid);
            pnHelpers.WriteString(s, fPlayerName, 40);
            pnHelpers.WriteString(s, fShape, 64);
        }
    }
}
