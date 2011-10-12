using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pnVaultNodeAccess {

        protected pnVaultNode fBase;

        /// <summary>
        /// Gets the raw Vault Node
        /// </summary>
        public pnVaultNode BaseNode {
            get { return fBase; }
        }

        public DateTime CreateTime {
            get { return fBase.CreateTime; }
        }

        public uint CreatorID {
            get { return fBase.CreatorID; }
            set { fBase.CreatorID = value; }
        }

        public Guid CreatorUuid {
            get { return fBase.CreatorUuid; }
            set { fBase.CreatorUuid = value; }
        }

        /// <summary>
        /// Gets the node ID
        /// </summary>
        public uint NodeID {
            get { return fBase.NodeID; }
        }

        public pnVaultNodeAccess() { }
        public pnVaultNodeAccess(pnVaultNode n) { fBase = n; }
        public pnVaultNodeAccess(ENodeType type) { fBase = new pnVaultNode(type); }
    }

    public sealed class pnVaultAgeNode : pnVaultNodeAccess {

        public Guid Instance {
            get {
                if (fBase.Uuid_1.HasValue)
                    return fBase.Uuid_1.Value;
                else
                    return Guid.Empty;
            }

            set { fBase.Uuid_1 = value; }
        }

        public Guid ParentInstance {
            get {
                if (fBase.Uuid_2.HasValue)
                    return fBase.Uuid_2.Value;
                else
                    return Guid.Empty;
            }

            set { fBase.Uuid_2 = value; }
        }

        public string AgeName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public pnVaultAgeNode() : base(ENodeType.kNodeAge) { }
        public pnVaultAgeNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultAgeInfoNode : pnVaultNodeAccess {

        public int SequenceNumber {
            get {
                if (fBase.Int32_1.HasValue)
                    return fBase.Int32_1.Value;
                else
                    return 0;
            }

            set { fBase.Int32_1 = value; }
        }

        public bool Public {
            get {
                if (fBase.Int32_2.HasValue)
                    return Convert.ToBoolean(fBase.Int32_2.Value);
                else
                    return false;
            }

            set { fBase.Int32_2 = Convert.ToInt32(value); }
        }

        public int Language {
            get {
                if (fBase.Int32_3.HasValue)
                    return fBase.Int32_3.Value;
                else
                    return -1; // Invalid language
            }

            set { fBase.Int32_3 = value; }
        }

        public uint AgeNodeID {
            get {
                if (fBase.UInt32_1.HasValue)
                    return fBase.UInt32_1.Value;
                else
                    return 0;
            }

            set { fBase.UInt32_1 = value; }
        }

        public uint TsarID {
            get {
                if (fBase.UInt32_2.HasValue)
                    return fBase.UInt32_2.Value;
                else
                    return 0;
            }

            set { fBase.UInt32_2 = value; }
        }

        public uint Flags {
            get {
                if (fBase.UInt32_3.HasValue)
                    return fBase.UInt32_3.Value;
                else
                    return 0;
            }

            set { fBase.UInt32_3 = value; }
        }

        public Guid InstanceUuid {
            get {
                if (fBase.Uuid_1.HasValue)
                    return fBase.Uuid_1.Value;
                else
                    return Guid.Empty;
            }

            set { fBase.Uuid_1 = value; }
        }

        public Guid ParentInstanceUuid {
            get {
                if (fBase.Uuid_2.HasValue)
                    return fBase.Uuid_2.Value;
                else
                    return Guid.Empty;
            }

            set { fBase.Uuid_2 = value; }
        }

        public string Filename {
            get { return fBase.String64_2; }
            set { fBase.String64_2 = value; }
        }

        public string InstanceName {
            get { return fBase.String64_3; }
            set { fBase.String64_3 = value; }
        }

        public string UserDefinedName {
            get { return fBase.String64_4; }
            set { fBase.String64_4 = value; }
        }

        public string Description {
            get { return fBase.Text_1; }
            set { fBase.Text_1 = value; }
        }

        public pnVaultAgeInfoNode() : base(ENodeType.kNodeAgeInfo) { }
        public pnVaultAgeInfoNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultAgeLinkNode : pnVaultNodeAccess {

        public bool Unlocked {
            get {
                if (fBase.Int32_1.HasValue)
                    return Convert.ToBoolean(fBase.Int32_1.Value);
                else
                    return false;
            }

            set { fBase.Int32_1 = Convert.ToInt32(value); }
        }

        public bool Volatile {
            get {
                if (fBase.Int32_2.HasValue)
                    return Convert.ToBoolean(fBase.Int32_2.Value);
                else
                    return false;
            }

            set { fBase.Int32_2 = Convert.ToInt32(value); }
        }

        public string SpawnPoints {
            get { return Encoding.UTF8.GetString(fBase.Blob_1); }
            set { fBase.Blob_1 = Encoding.UTF8.GetBytes(value); }
        }

        public pnVaultAgeLinkNode() : base(ENodeType.kNodeAgeLink) { }
        public pnVaultAgeLinkNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultChronicleNode : pnVaultNodeAccess {

        public int EntryType {
            get {
                if (fBase.Int32_1.HasValue)
                    return fBase.Int32_1.Value;
                else
                    return 0;
            }

            set { fBase.Int32_1 = value; }
        }

        public string EntryName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public string EntryValue {
            get { return fBase.Text_1; }
            set { fBase.Text_1 = value; }
        }

        public pnVaultChronicleNode() : base(ENodeType.kNodeChronicle) { }
        public pnVaultChronicleNode(pnVaultNode node) : base(node) { }
    }

    public class pnVaultFolderNode : pnVaultNodeAccess {

        public EStandardNode FolderType {
            get {
                if (fBase.Int32_1.HasValue)
                    return (EStandardNode)fBase.Int32_1.Value;
                else
                    return EStandardNode.kUserDefinedNode;
            }

            set { fBase.Int32_1 = (int)value; }
        }

        public string FolderName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public pnVaultFolderNode() : base(ENodeType.kNodeFolder) { }
        protected pnVaultFolderNode(ENodeType type) : base(type) { }
        public pnVaultFolderNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultAgeInfoListNode : pnVaultFolderNode {
        public pnVaultAgeInfoListNode() : base(ENodeType.kNodeAgeInfoList) { }
        public pnVaultAgeInfoListNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultPlayerInfoListNode : pnVaultFolderNode {
        public pnVaultPlayerInfoListNode() : base(ENodeType.kNodePlayerInfoList) { }
        public pnVaultPlayerInfoListNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultImageNode : pnVaultNodeAccess {
        public enum ImgType { kNone, kJPEG }

        public ImgType ImageType {
            get {
                if (fBase.Int32_1.HasValue)
                    return (ImgType)fBase.Int32_1.Value;
                else
                    return ImgType.kNone;
            }

            set { fBase.Int32_1 = (int)value; }
        }

        public string ImageName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public byte[] ImageData {
            get {
                // Extra 4 byte length :(
                byte[] eapSucks = new byte[fBase.Blob_1.Length - 4];
                Buffer.BlockCopy(fBase.Blob_1, 4, eapSucks, 0, eapSucks.Length);
                return eapSucks;
            }
            
            set {
                if (value == null)
                    fBase.Blob_1 = null;
                else {
                    // Prepend the 4 byte length :(
                    byte[] eapSucks = new byte[value.Length + 4];
                    Buffer.BlockCopy(BitConverter.GetBytes(eapSucks.Length), 0, eapSucks, 0, 4);
                    Buffer.BlockCopy(value, 0, eapSucks, 4, value.Length);
                    fBase.Blob_1 = eapSucks;
                }
            }
        }

        public pnVaultImageNode() : base(ENodeType.kNodeImage) { }
        public pnVaultImageNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultMarkerListNode : pnVaultNodeAccess {

        // TODO: Find/Create an enum for me?
        public int GameType {
            get {
                if (fBase.Int32_1.HasValue)
                    return fBase.Int32_1.Value;
                else
                    return 0;
            }

            set { fBase.Int32_1 = value; }
        }

        public int RoundLength {
            get {
                if (fBase.Int32_2.HasValue)
                    return fBase.Int32_2.Value;
                else
                    return 0;
            }

            set { fBase.Int32_2 = value; }
        }

        public string OwnerName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public pnVaultMarkerListNode() : base(ENodeType.kNodeMarkerList) { }
        public pnVaultMarkerListNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultMarkerNode : pnVaultNodeAccess {

        public string AgeName {
            get { return fBase.CreateAgeName; }
            set { fBase.CreateAgeName = value; }
        }

        public int Torans {
            get {
                if (fBase.Int32_1.HasValue)
                    return fBase.Int32_1.Value;
                else
                    return 0;
            }

            set { fBase.Int32_1 = value; }
        }

        public int HSpans {
            get {
                if (fBase.Int32_2.HasValue)
                    return fBase.Int32_2.Value;
                else
                    return 0;
            }

            set { fBase.Int32_2 = value; }
        }

        public int VSpans {
            get {
                if (fBase.Int32_3.HasValue)
                    return fBase.Int32_3.Value;
                else
                    return 0;
            }

            set { fBase.Int32_3 = value; }
        }

        public uint PosX {
            get {
                if (fBase.UInt32_1.HasValue)
                    return fBase.UInt32_1.Value;
                else
                    return 0;
            }

            set { fBase.UInt32_1 = value; }
        }

        public uint PosY {
            get {
                if (fBase.UInt32_2.HasValue)
                    return fBase.UInt32_2.Value;
                else
                    return 0;
            }

            set { fBase.UInt32_2 = value; }
        }

        public uint PosZ {
            get {
                if (fBase.UInt32_3.HasValue)
                    return fBase.UInt32_3.Value;
                else
                    return 0;
            }

            set { fBase.UInt32_3 = value; }
        }

        public string MarkerText {
            get { return fBase.Text_1; }
            set { fBase.Text_1 = value; }
        }

        public pnVaultMarkerNode() : base(ENodeType.kNodeMarker) { }
        public pnVaultMarkerNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultPlayerInfoNode : pnVaultNodeAccess {

        public bool Online {
            get {
                if (fBase.Int32_1.HasValue)
                    return Convert.ToBoolean(fBase.Int32_1.Value);
                else
                    return false;
            }

            set { fBase.Int32_1 = Convert.ToInt32(value); }
        }

        public uint PlayerID {
            get {
                if (fBase.UInt32_1.HasValue)
                    return fBase.UInt32_1.Value;
                else
                    return 0;
            }

            set { fBase.UInt32_1 = value; }
        }

        public Guid AgeInstanceUuid {
            get {
                if (fBase.Uuid_1.HasValue)
                    return fBase.Uuid_1.Value;
                else
                    return Guid.Empty;
            }

            set { fBase.Uuid_1 = value; }
        }

        public string AgeInstanceName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public string PlayerName {
            get { return fBase.IString64_1; }
            set { fBase.IString64_1 = value; }
        }

        public pnVaultPlayerInfoNode() : base(ENodeType.kNodePlayerInfo) { }
        public pnVaultPlayerInfoNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultPlayerNode : pnVaultNodeAccess {

        public bool Banned {
            get {
                if (fBase.Int32_1.HasValue)
                    return Convert.ToBoolean(fBase.Int32_1.Value);
                else
                    return false;
            }

            set { fBase.Int32_1 = Convert.ToInt32(value); }
        }

        public bool Explorer {
            get {
                if (fBase.Int32_2.HasValue)
                    return Convert.ToBoolean(fBase.Int32_2.Value);
                else
                    return false;
            }

            set { fBase.Int32_2 = Convert.ToInt32(value); }
        }

        public TimeSpan OnlineTime {
            get {
                if (fBase.UInt32_1.HasValue)
                    return TimeSpan.FromSeconds((double)fBase.UInt32_1);
                else
                    return TimeSpan.FromSeconds(0.0d);
            }

            set { fBase.UInt32_1 = (uint)value.Seconds; }
        }

        public Guid AccountUuid {
            get {
                if (fBase.Uuid_1.HasValue)
                    return fBase.Uuid_1.Value;
                else
                    return Guid.Empty;
            }

            set { fBase.Uuid_1 = value; }
        }

        public string AvatarShape {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public string PlayerName {
            get { return fBase.IString64_1; }
            set { fBase.IString64_1 = value; }
        }

        public pnVaultPlayerNode() : base(ENodeType.kNodePlayer) { }
        public pnVaultPlayerNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultSDLNode : pnVaultNodeAccess {

        public EStandardNode StateIdent {
            get {
                if (fBase.Int32_1.HasValue)
                    return (EStandardNode)fBase.Int32_1.Value;
                else
                    return EStandardNode.kAgeInstanceSDLNode;
            }

            set { fBase.Int32_1 = (int)value; }
        }

        public string StateName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public byte[] Record {
            get { return fBase.Blob_1; }
            set { fBase.Blob_1 = value; }
        }

        public pnVaultSDLNode() : base(ENodeType.kNodeSDL) { }
        public pnVaultSDLNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultSystemNode : pnVaultNodeAccess {

        public int CCRStatus {
            get {
                if (fBase.Int32_1.HasValue)
                    return fBase.Int32_1.Value;
                else
                    return 0;
            }

            set { fBase.Int32_1 = value; }
        }

        public pnVaultSystemNode() : base(ENodeType.kNodeSystem) { }
        public pnVaultSystemNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultTextNode : pnVaultNodeAccess {

        public ENoteType NodeType {
            get {
                if (fBase.Int32_1.HasValue)
                    return (ENoteType)fBase.Int32_1.Value;
                else
                    return ENoteType.kNoteGeneric;
            }

            set { fBase.Int32_1 = (int)value; }
        }

        public ENoteType NodeSubType {
            get {
                if (fBase.Int32_2.HasValue)
                    return (ENoteType)fBase.Int32_2.Value;
                else
                    return ENoteType.kNoteGeneric;
            }

            set { fBase.Int32_2 = (int)value; }
        }

        public string NoteName {
            get { return fBase.String64_1; }
            set { fBase.String64_1 = value; }
        }

        public string Text {
            get { return fBase.Text_1; }
            set { fBase.Text_1 = value; }
        }

        public pnVaultTextNode() : base(ENodeType.kNodeTextNote) { }
        public pnVaultTextNode(pnVaultNode node) : base(node) { }
    }
}
