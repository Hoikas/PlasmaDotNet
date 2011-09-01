using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum ENodeType {
        kNodeInvalid, kNodeVNodeMgrLow, kNodePlayer, kNodeAge, kNodeGameServer,
        kNodeAdmin, kNodeVaultServer, kNodeCCR, kNodeVNodeMgrHigh = 21,
        kNodeFolder, kNodePlayerInfo, kNodeSystem, kNodeImage, kNodeTextNote,
        kNodeSDL, kNodeAgeLink, kNodeChronicle, kNodePlayerInfoList,
        kNodeUNUSED, kNodeMarker, kNodeAgeInfo, kNodeAgeInfoList,
        kNodeMarkerList
    }

    public enum EStandardNode {
        kUserDefinedNode, kInboxFolder, kBuddyListFolder, kIgnoreListFolder,
        kPeopleIKnowAboutFolder, kVaultMgrGlobalDataFolder, kChronicleFolder,
        kAvatarOutfitFolder, kAgeTypeJournalFolder, kSubAgesFolder,
        kDeviceInboxFolder, kHoodMembersFolder, kAllPlayersFolder,
        kAgeMembersFolder, kAgeJournalsFolder, kAgeDevicesFolder,
        kAgeInstanceSDLNode, kAgeGlobalSDLNode, kCanVisitFolder, kAgeOwnersFolder,
        kAllAgeGlobalSDLNodesFolder, kPlayerInfoNode, kPublicAgesFolder,
        kAgesIOwnFolder, kAgesICanVisitFolder, kAvatarClosetFolder, kAgeInfoNode,
        kSystemNode, kPlayerInviteFolder, kCCRPlayersFolder, kGlobalInboxFolder,
        kChildAgesFolder, kGameScoresFolder, kLastStandardNode
    }

    public enum ENoteType {
        kNoteGeneric, kNoteCCRPetition, kNoteDevice, kNoteInvite, kNoteVisit,
        kNoteUnVisit
    }

    public class pnVaultNode {
        [Flags]
        protected enum Fields : long {
            kNodeIdx = (1 << 0),
            kCreateTime = (1 << 1),
            kModifyTime = (1 << 2),
            kCreateAgeName = (1 << 3),
            kCreateAgeUuid = (1 << 4),
            kCreatorUuid = (1 << 5),
            kCreatorIdx = (1 << 6),
            kNodeType = (1 << 7),
            kInt32_1 = (1 << 8),
            kInt32_2 = (1 << 9),
            kInt32_3 = (1 << 10),
            kInt32_4 = (1 << 11),
            kUInt32_1 = (1 << 12),
            kUInt32_2 = (1 << 13),
            kUInt32_3 = (1 << 14),
            kUInt32_4 = (1 << 15),
            kUuid_1 = (1 << 16),
            kUuid_2 = (1 << 17),
            kUuid_3 = (1 << 18),
            kUuid_4 = (1 << 19),
            kString64_1 = (1 << 20),
            kString64_2 = (1 << 21),
            kString64_3 = (1 << 22),
            kString64_4 = (1 << 23),
            kString64_5 = (1 << 24),
            kString64_6 = (1 << 25),
            kIString64_1 = (1 << 26),
            kIString64_2 = (1 << 27),
            kText_1 = (1 << 28),
            kText_2 = (1 << 29),
            kBlob_1 = (1 << 30),
            kBlob_2 = (1 << 31)
        }

        protected Fields NodeFields {
            get {
                Fields f = (Fields)0;
                if (fIdx.HasValue) f |= Fields.kNodeIdx;
                if (fCreateAgeName != String.Empty) f |= Fields.kCreateAgeName;
                if (fCreateAgeUuid != Guid.Empty) f |= Fields.kCreateAgeUuid;
                if (fCreatorIdx.HasValue) f |= Fields.kCreatorIdx;
                if (fCreatorUuid != Guid.Empty) f |= Fields.kCreatorUuid;
                if (fNodeType != ENodeType.kNodeInvalid) f |= Fields.kNodeType;
                if (fInt32[0].HasValue) f |= Fields.kInt32_1;
                if (fInt32[1].HasValue) f |= Fields.kInt32_2;
                if (fInt32[2].HasValue) f |= Fields.kInt32_3;
                if (fInt32[3].HasValue) f |= Fields.kInt32_4;
                if (fUInt32[0].HasValue) f |= Fields.kUInt32_1;
                if (fUInt32[1].HasValue) f |= Fields.kUInt32_2;
                if (fUInt32[2].HasValue) f |= Fields.kUInt32_3;
                if (fUInt32[3].HasValue) f |= Fields.kUInt32_4;
                if (fUuid[0] != Guid.Empty) f |= Fields.kUuid_1;
                if (fUuid[1] != Guid.Empty) f |= Fields.kUuid_2;
                if (fUuid[2] != Guid.Empty) f |= Fields.kUuid_3;
                if (fUuid[3] != Guid.Empty) f |= Fields.kUuid_4;
                if (fString64[0] != String.Empty) f |= Fields.kString64_1;
                if (fString64[1] != String.Empty) f |= Fields.kString64_2;
                if (fString64[2] != String.Empty) f |= Fields.kString64_3;
                if (fString64[3] != String.Empty) f |= Fields.kString64_4;
                if (fString64[4] != String.Empty) f |= Fields.kString64_5;
                if (fString64[5] != String.Empty) f |= Fields.kString64_6;
                if (fIString64[0] != String.Empty) f |= Fields.kIString64_1;
                if (fIString64[1] != String.Empty) f |= Fields.kIString64_2;
                if (fText[0] != String.Empty) f |= Fields.kText_1;
                if (fText[1] != String.Empty) f |= Fields.kText_2;
                if (fBlob[0] != null)
                    if (fBlob[0].Length > 0) f |= Fields.kBlob_1;
                if (fBlob[1] != null)
                    if (fBlob[1].Length > 0) f |= Fields.kBlob_2;

                return f;
            }
        }

        public uint ID {
            get { return (uint)fIdx; }
            set {
                if (!fIdx.HasValue) fIdx = value;
                else throw new NotSupportedException("Cannot change a NodeID");
            }
        }

        public string CreateAgeName {
            get { return fCreateAgeName; }
            set {
                if (fCreateAgeName == String.Empty)
                    fCreateAgeName = value;
                else
                    throw new NotSupportedException("Cannot change the CreateAgeName");
            }
        }

        public Guid CreateAgeUuid {
            get { return fCreateAgeUuid; }
            set {
                if (fCreateAgeUuid == Guid.Empty)
                    fCreateAgeUuid = value;
                else
                    throw new NotSupportedException("Cannot change the CreateAgeUuid");
            }
        }

        public DateTime CreateTime {
            get { return fCreateTime; }
        }

        public Guid CreatorUuid {
            get { return fCreatorUuid; }
            set { fCreatorUuid = value; }
        }

        public DateTime ModifyTime {
            get { return fModifyTime; }
            set { fModifyTime = value; }
        }

        public ENodeType NodeType {
            get { return fNodeType; }
        }

        protected uint? fIdx;
        protected DateTime fCreateTime = DateTime.UtcNow;
        protected DateTime fModifyTime = DateTime.UtcNow;
        protected string fCreateAgeName = String.Empty;
        protected Guid fCreateAgeUuid = Guid.Empty;
        public uint? fCreatorIdx = new uint?();
        protected Guid fCreatorUuid = Guid.Empty;
        protected ENodeType fNodeType = ENodeType.kNodeInvalid;
        public Nullable<int>[] fInt32 = new Nullable<int>[4];
        public Nullable<uint>[] fUInt32 = new Nullable<uint>[4];
        public Guid[] fUuid = new Guid[] { Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty };
        public string[] fString64 = new string[] { String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty };
        public string[] fIString64 = new string[] { String.Empty, String.Empty };
        public string[] fText = new string[] { String.Empty, String.Empty };
        public byte[][] fBlob = new byte[][] { null, null };

        public pnVaultNode() { }
        public pnVaultNode(ENodeType type) { fNodeType = type; }
        public pnVaultNode(ENodeType type, DateTime createTime, DateTime modifyTime) {
            fNodeType = type;
            fCreateTime = createTime;
            fModifyTime = modifyTime;
        }

        public static pnVaultNode Parse(byte[] data) {
            MemoryStream ms = new MemoryStream(data);
            hsStream s = new hsStream(ms);

            pnVaultNode n = new pnVaultNode();
            n.Read(s);

            s.Close();
            ms.Close();

            return n;
        }

        public static pnVaultNode SafeParse(byte[] data, bool replaceTimes) {
            pnVaultNode node = Parse(data);
            node.fIdx = 0;

            if (replaceTimes) {
                node.fCreateTime = DateTime.UtcNow.Subtract(TimeSpan.FromHours(7));
                node.fModifyTime = DateTime.UtcNow.Subtract(TimeSpan.FromHours(7));
            }

            return node;
        }

        public void Read(hsStream s) {
            ulong bit = 1;
            Fields f = (Fields)s.ReadULong();
            while (bit != 0 && bit <= (ulong)f) {
                switch (f & (Fields)bit) {
                    case Fields.kBlob_1:
                        fBlob[0] = s.ReadBytes(s.ReadInt());
                        break;
                    case Fields.kBlob_2:
                        fBlob[1] = s.ReadBytes(s.ReadInt());
                        break;
                    case Fields.kCreateAgeName:
                        fCreateAgeName = pnHelpers.ReadString(s);
                        break;
                    case Fields.kCreateAgeUuid:
                        fCreateAgeUuid = pnHelpers.ReadUuid(s);
                        break;
                    case Fields.kCreateTime:
                        fCreateTime = ToDateTime(s.ReadUInt());
                        break;
                    case Fields.kCreatorIdx:
                        fCreatorIdx = s.ReadUInt();
                        break;
                    case Fields.kCreatorUuid:
                        fCreatorUuid = pnHelpers.ReadUuid(s);
                        break;
                    case Fields.kInt32_1:
                        fInt32[0] = s.ReadInt();
                        break;
                    case Fields.kInt32_2:
                        fInt32[1] = s.ReadInt();
                        break;
                    case Fields.kInt32_3:
                        fInt32[2] = s.ReadInt();
                        break;
                    case Fields.kInt32_4:
                        fInt32[3] = s.ReadInt();
                        break;
                    case Fields.kIString64_1:
                        fIString64[0] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kIString64_2:
                        fIString64[1] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kModifyTime:
                        fModifyTime = ToDateTime(s.ReadUInt());
                        break;
                    case Fields.kNodeIdx:
                        fIdx = s.ReadUInt();
                        break;
                    case Fields.kNodeType:
                        fNodeType = (ENodeType)s.ReadUInt();
                        break;
                    case Fields.kString64_1:
                        fString64[0] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kString64_2:
                        fString64[1] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kString64_3:
                        fString64[2] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kString64_4:
                        fString64[3] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kString64_5:
                        fString64[4] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kString64_6:
                        fString64[5] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kText_1:
                        fText[0] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kText_2:
                        fText[0] = pnHelpers.ReadString(s);
                        break;
                    case Fields.kUInt32_1:
                        fUInt32[0] = s.ReadUInt();
                        break;
                    case Fields.kUInt32_2:
                        fUInt32[1] = s.ReadUInt();
                        break;
                    case Fields.kUInt32_3:
                        fUInt32[2] = s.ReadUInt();
                        break;
                    case Fields.kUInt32_4:
                        fUInt32[3] = s.ReadUInt();
                        break;
                    case Fields.kUuid_1:
                        fUuid[0] = pnHelpers.ReadUuid(s);
                        break;
                    case Fields.kUuid_2:
                        fUuid[1] = pnHelpers.ReadUuid(s);
                        break;
                    case Fields.kUuid_3:
                        fUuid[2] = pnHelpers.ReadUuid(s);
                        break;
                    case Fields.kUuid_4:
                        fUuid[3] = pnHelpers.ReadUuid(s);
                        break;
                }

                bit <<= 1;
            }
        }

        public byte[] ToArray() {
            MemoryStream ms = new MemoryStream();
            hsStream s = new hsStream(ms);

            ulong bit = 1;
            Fields f = NodeFields;
            s.WriteULong((ulong)f);
            while (bit != 0 && bit <= (ulong)f) {
                switch ((f & (Fields)bit)) {
                    case Fields.kBlob_1:
                        s.WriteInt(fBlob[0].Length);
                        s.WriteBytes(fBlob[0]);
                        break;
                    case Fields.kBlob_2:
                        s.WriteInt(fBlob[1].Length);
                        s.WriteBytes(fBlob[1]);
                        break;
                    case Fields.kCreateAgeName:
                        pnHelpers.WriteString(s, fCreateAgeName);
                        break;
                    case Fields.kCreateAgeUuid:
                        s.WriteBytes(fCreateAgeUuid.ToByteArray());
                        break;
                    case Fields.kCreateTime:
                        s.WriteUInt(ToUnixTime(fCreateTime));
                        break;
                    case Fields.kCreatorIdx:
                        s.WriteUInt(fCreatorIdx.Value);
                        break;
                    case Fields.kCreatorUuid:
                        s.WriteBytes(fCreatorUuid.ToByteArray());
                        break;
                    case Fields.kInt32_1:
                        s.WriteInt(fInt32[0].Value);
                        break;
                    case Fields.kInt32_2:
                        s.WriteInt(fInt32[1].Value);
                        break;
                    case Fields.kInt32_3:
                        s.WriteInt(fInt32[2].Value);
                        break;
                    case Fields.kInt32_4:
                        s.WriteInt(fInt32[3].Value);
                        break;
                    case Fields.kIString64_1:
                        pnHelpers.WriteString(s, fIString64[0]);
                        break;
                    case Fields.kIString64_2:
                        pnHelpers.WriteString(s, fIString64[1]);
                        break;
                    case Fields.kModifyTime:
                        s.WriteUInt(ToUnixTime(fModifyTime));
                        break;
                    case Fields.kNodeIdx:
                        s.WriteUInt((uint)fIdx);
                        break;
                    case Fields.kNodeType:
                        s.WriteUInt((uint)fNodeType);
                        break;
                    case Fields.kString64_1:
                        pnHelpers.WriteString(s, fString64[0]);
                        break;
                    case Fields.kString64_2:
                        pnHelpers.WriteString(s, fString64[1]);
                        break;
                    case Fields.kString64_3:
                        pnHelpers.WriteString(s, fString64[2]);
                        break;
                    case Fields.kString64_4:
                        pnHelpers.WriteString(s, fString64[3]);
                        break;
                    case Fields.kString64_5:
                        pnHelpers.WriteString(s, fString64[4]);
                        break;
                    case Fields.kString64_6:
                        pnHelpers.WriteString(s, fString64[5]);
                        break;
                    case Fields.kText_1:
                        pnHelpers.WriteString(s, fText[0]);
                        break;
                    case Fields.kText_2:
                        pnHelpers.WriteString(s, fText[1]);
                        break;
                    case Fields.kUInt32_1:
                        s.WriteUInt(fUInt32[0].Value);
                        break;
                    case Fields.kUInt32_2:
                        s.WriteUInt(fUInt32[1].Value);
                        break;
                    case Fields.kUInt32_3:
                        s.WriteUInt(fUInt32[2].Value);
                        break;
                    case Fields.kUInt32_4:
                        s.WriteUInt(fUInt32[3].Value);
                        break;
                    case Fields.kUuid_1:
                        pnHelpers.WriteUuid(s, fUuid[0]);
                        break;
                    case Fields.kUuid_2:
                        pnHelpers.WriteUuid(s, fUuid[1]);
                        break;
                    case Fields.kUuid_3:
                        pnHelpers.WriteUuid(s, fUuid[2]);
                        break;
                    case Fields.kUuid_4:
                        pnHelpers.WriteUuid(s, fUuid[3]);
                        break;
                }

                bit <<= 1;
            }

            byte[] buf = ms.ToArray();
            s.Close();
            ms.Close();
            return buf;
        }

        public Dictionary<string, object> ToDictionary() {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("CreateTime", fCreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            dict.Add("ModifyTime", fModifyTime.ToString("yyyy-MM-dd HH:mm:ss"));

            if (fIdx.HasValue) 
                dict.Add("Idx", (uint)fIdx);
            if (fCreateAgeName != String.Empty) 
                dict.Add("CreateAgeName", fCreateAgeName);
            if (fCreateAgeUuid != Guid.Empty) 
                dict.Add("CreateAgeUuid", fCreateAgeUuid);
            if (fCreatorIdx.HasValue) 
                dict.Add("CreatorIdx", (uint)fCreatorIdx);
            if (fCreatorUuid != Guid.Empty)
                dict.Add("CreatorUuid", fCreatorUuid);
            if (fNodeType != ENodeType.kNodeInvalid)
                dict.Add("NodeType", (uint)fNodeType);
            if (fInt32[0].HasValue)
                dict.Add("Int32_1", (int)fInt32[0]);
            if (fInt32[1].HasValue)
                dict.Add("Int32_2", (int)fInt32[1]);
            if (fInt32[2].HasValue)
                dict.Add("Int32_3", (int)fInt32[2]);
            if (fInt32[3].HasValue)
                dict.Add("Int32_4", (int)fInt32[3]);
            if (fUInt32[0].HasValue)
                dict.Add("UInt32_1", (uint)fUInt32[0]);
            if (fUInt32[1].HasValue)
                dict.Add("UInt32_2", (uint)fUInt32[1]);
            if (fUInt32[2].HasValue)
                dict.Add("UInt32_3", (uint)fUInt32[2]);
            if (fUInt32[3].HasValue)
                dict.Add("UInt32_4", (uint)fUInt32[3]);
            if (fUuid[0] != Guid.Empty)
                dict.Add("Uuid_1", fUuid[0]);
            if (fUuid[1] != Guid.Empty)
                dict.Add("Uuid_1", fUuid[1]);
            if (fUuid[2] != Guid.Empty)
                dict.Add("Uuid_1", fUuid[2]);
            if (fUuid[3] != Guid.Empty)
                dict.Add("Uuid_1", fUuid[3]);
            if (fString64[0] != String.Empty)
                dict.Add("String64_1", fString64[0]);
            if (fString64[1] != String.Empty)
                dict.Add("String64_2", fString64[1]);
            if (fString64[2] != String.Empty)
                dict.Add("String64_3", fString64[2]);
            if (fString64[3] != String.Empty)
                dict.Add("String64_4", fString64[3]);
            if (fString64[4] != String.Empty)
                dict.Add("String64_5", fString64[4]);
            if (fString64[5] != String.Empty)
                dict.Add("String64_6", fString64[5]);
            if (fIString64[0] != String.Empty)
                dict.Add("IString64_1", fIString64[0]);
            if (fIString64[1] != String.Empty)
                dict.Add("IString64_2", fIString64[1]);
            if (fText[0] != String.Empty)
                dict.Add("Text_1", fText[0]);
            if (fText[1] != String.Empty)
                dict.Add("Text_2", fText[1]);
            if (fBlob[0] != null)
                if (fBlob[0].Length > 0)
                    dict.Add("Blob_1", Convert.ToBase64String(fBlob[0]));
            if (fBlob[1] != null)
                if (fBlob[1].Length > 0)
                    dict.Add("Blob_2", Convert.ToBase64String(fBlob[1]));

            return dict;
        }

        public static uint ToUnixTime(DateTime dt) {
            TimeSpan ts = (dt - new DateTime(1970, 1, 1, 0, 0, 0));
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        public static DateTime ToDateTime(uint unix) {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dt = dt.AddSeconds(Convert.ToDouble(unix));
            return dt;
        }
    }

    public class pnVaultNodeRef {

        public uint fParent;
        public uint fChild;
        public uint fSaver;

        public void Read(hsStream s) {
            fParent = s.ReadUInt();
            fChild = s.ReadUInt();
            fSaver = s.ReadUInt();
            s.ReadByte(); // "Seen" -- might as well be garbage
        }

        public void Write(hsStream s) {
            s.WriteUInt(fParent);
            s.WriteUInt(fChild);
            s.WriteUInt(fSaver);
            s.WriteByte(0);
        }
    }
}
