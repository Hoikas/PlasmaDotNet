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
            get { return fBase.fCreatorIdx.Value; }
            set { fBase.fCreatorIdx = value; }
        }

        public Guid CreatorUUID {
            get { return fBase.CreatorUuid; }
            set { fBase.CreatorUuid = value; }
        }

        /// <summary>
        /// Gets the node ID
        /// </summary>
        public uint NodeID {
            get { return fBase.ID; }
        }

        public pnVaultNodeAccess() { }
        public pnVaultNodeAccess(pnVaultNode n) { fBase = n; }
        public pnVaultNodeAccess(ENodeType type) { fBase = new pnVaultNode(type); }
    }

    public sealed class pnVaultAgeNode : pnVaultNodeAccess {

        public Guid Instance {
            get { return fBase.fUuid[0]; }
            set { fBase.fUuid[0] = value; }
        }

        public Guid ParentInstance {
            get { return fBase.fUuid[1]; }
            set { fBase.fUuid[1] = value; }
        }

        public string AgeName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
        }

        public pnVaultAgeNode() : base(ENodeType.kNodeAge) { }
        public pnVaultAgeNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultAgeInfoNode : pnVaultNodeAccess {

        public int SequenceNumber {
            get { return fBase.fInt32[0].Value; }
            set { fBase.fInt32[0] = value; }
        }

        public bool Public {
            get { return Convert.ToBoolean(fBase.fInt32[1]); }
            set { fBase.fInt32[1] = Convert.ToInt32(value); }
        }

        public int Language {
            get { return fBase.fInt32[2].Value; }
            set { fBase.fInt32[2] = value; }
        }

        public uint AgeNodeID {
            get { return fBase.fUInt32[0].Value; }
            set { fBase.fUInt32[0] = value; }
        }

        public uint TsarID {
            get { return fBase.fUInt32[1].Value; }
            set { fBase.fUInt32[1] = value; }
        }

        public uint Flags {
            get { return fBase.fUInt32[2].Value; }
            set { fBase.fUInt32[2] = value; }
        }

        public Guid InstanceUUID {
            get { return fBase.fUuid[0]; }
            set { fBase.fUuid[0] = value; }
        }

        public Guid ParentInstanceUUID {
            get { return fBase.fUuid[1]; }
            set { fBase.fUuid[1] = value; }
        }

        public string Filename {
            get { return fBase.fString64[1]; }
            set { fBase.fString64[1] = value; }
        }

        public string InstanceName {
            get { return fBase.fString64[2]; }
            set { fBase.fString64[2] = value; }
        }

        public string UserDefinedName {
            get { return fBase.fString64[3]; }
            set { fBase.fString64[3] = value; }
        }

        public string Description {
            get { return fBase.fText[0]; }
            set { fBase.fText[0] = value; }
        }

        public pnVaultAgeInfoNode() : base(ENodeType.kNodeAgeInfo) { }
        public pnVaultAgeInfoNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultAgeLinkNode : pnVaultNodeAccess {

        public bool Unlocked {
            get { return Convert.ToBoolean(fBase.fInt32[0]); }
            set { fBase.fInt32[0] = Convert.ToInt32(value); }
        }

        public bool Volatile {
            get { return Convert.ToBoolean(fBase.fInt32[1]); }
            set { fBase.fInt32[1] = Convert.ToInt32(value); }
        }

        public string SpawnPoints {
            get { return Encoding.UTF8.GetString(fBase.fBlob[0]); }
            set { fBase.fBlob[0] = Encoding.UTF8.GetBytes(value); }
        }

        public pnVaultAgeLinkNode() : base(ENodeType.kNodeAgeLink) { }
        public pnVaultAgeLinkNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultChronicleNode : pnVaultNodeAccess {

        public int EntryType {
            get { return fBase.fInt32[0].Value; }
            set { fBase.fInt32[0] = value; }
        }

        public string EntryName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
        }

        public string EntryValue {
            get { return fBase.fText[0]; }
            set { fBase.fText[0] = value; }
        }

        public pnVaultChronicleNode() : base(ENodeType.kNodeChronicle) { }
        public pnVaultChronicleNode(pnVaultNode node) : base(node) { }
    }

    public class pnVaultFolderNode : pnVaultNodeAccess {

        public EStandardNode FolderType {
            get { return (EStandardNode)fBase.fInt32[0]; }
            set { fBase.fInt32[0] = (int)value; }
        }

        public string FolderName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
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
            get { return (ImgType)fBase.fInt32[0]; }
            set { fBase.fInt32[0] = (int)value; }
        }

        public string ImageName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
        }

        public byte[] ImageData {
            get { return fBase.fBlob[0]; }
            set { fBase.fBlob[0] = value; }
        }

        public pnVaultImageNode() : base(ENodeType.kNodeImage) { }
        public pnVaultImageNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultMarkerListNode : pnVaultNodeAccess {

        public int GameType {
            get { return fBase.fInt32[0].Value; }
            set { fBase.fInt32[0] = value; }
        }

        public int RoundLength {
            get { return fBase.fInt32[1].Value; }
            set { fBase.fInt32[1] = value; }
        }

        public string OwnerName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
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
            get { return fBase.fInt32[0].Value; }
            set { fBase.fInt32[0] = value; }
        }

        public int HSpans {
            get { return fBase.fInt32[1].Value; }
            set { fBase.fInt32[1] = value; }
        }

        public int VSpans {
            get { return fBase.fInt32[2].Value; }
            set { fBase.fInt32[2] = value; }
        }

        public uint PosX {
            get { return fBase.fUInt32[0].Value; }
            set { fBase.fUInt32[0] = value; }
        }

        public uint PosY {
            get { return fBase.fUInt32[1].Value; }
            set { fBase.fUInt32[1] = value; }
        }

        public uint PosZ {
            get { return fBase.fUInt32[2].Value; }
            set { fBase.fUInt32[2] = value; }
        }

        public string MarkerText {
            get { return fBase.fText[0]; }
            set { fBase.fText[0] = value; }
        }

        public pnVaultMarkerNode() : base(ENodeType.kNodeMarker) { }
        public pnVaultMarkerNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultPlayerInfoNode : pnVaultNodeAccess {

        public bool Online {
            get { return Convert.ToBoolean(fBase.fInt32[0]); }
            set { fBase.fInt32[0] = Convert.ToInt32(value); }
        }

        public uint PlayerID {
            get { return fBase.fUInt32[0].Value; }
            set { fBase.fUInt32[0] = value; }
        }

        public Guid AgeInstanceUUID {
            get { return fBase.fUuid[0]; }
            set { fBase.fUuid[0] = value; }
        }

        public string AgeInstanceName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
        }

        public string PlayerName {
            get { return fBase.fIString64[0]; }
            set { fBase.fIString64[0] = value; }
        }

        public pnVaultPlayerInfoNode() : base(ENodeType.kNodePlayerInfo) { }
        public pnVaultPlayerInfoNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultPlayerNode : pnVaultNodeAccess {

        public bool Banned {
            get { return Convert.ToBoolean(fBase.fInt32[0]); }
            set { fBase.fInt32[0] = Convert.ToInt32(value); }
        }

        public bool Explorer {
            get { return Convert.ToBoolean(fBase.fInt32[1]); }
            set { fBase.fInt32[1] = Convert.ToInt32(value); }
        }

        public TimeSpan OnlineTime {
            get { return TimeSpan.FromSeconds((double)fBase.fUInt32[0]); }
            set { fBase.fUInt32[0] = (uint)value.TotalSeconds; }
        }

        public Guid AccountUUID {
            get { return fBase.fUuid[0]; }
            set { fBase.fUuid[0] = value; }
        }

        public string AvatarShape {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
        }

        public string PlayerName {
            get { return fBase.fIString64[0]; }
            set { fBase.fIString64[0] = value; }
        }

        public pnVaultPlayerNode() : base(ENodeType.kNodePlayer) { }
        public pnVaultPlayerNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultSDLNode : pnVaultNodeAccess {

        public int StateIdent {
            get { return fBase.fInt32[0].Value; }
            set { fBase.fInt32[0] = value; }
        }

        public string StateName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
        }

        /*
         * public StateDataRecord StateData {
         * 
         * }
         */

        public pnVaultSDLNode() : base(ENodeType.kNodeSDL) { }
        public pnVaultSDLNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultSystemNode : pnVaultNodeAccess {

        public int CCRStatus {
            get { return fBase.fInt32[0].Value; }
            set { fBase.fInt32[0] = value; }
        }

        public pnVaultSystemNode() : base(ENodeType.kNodeSystem) { }
        public pnVaultSystemNode(pnVaultNode node) : base(node) { }
    }

    public sealed class pnVaultTextNode : pnVaultNodeAccess {

        public ENoteType NodeType {
            get { return (ENoteType)fBase.fInt32[0]; }
            set { fBase.fInt32[0] = (int)value; }
        }

        public ENoteType NodeSubType {
            get { return (ENoteType)fBase.fInt32[1]; }
            set { fBase.fInt32[1] = (int)value; }
        }

        public string NoteName {
            get { return fBase.fString64[0]; }
            set { fBase.fString64[0] = value; }
        }

        public string Text {
            get { return fBase.fText[0]; }
            set { fBase.fText[0] = value; }
        }

        public pnVaultTextNode() : base(ENodeType.kNodeTextNote) { }
        public pnVaultTextNode(pnVaultNode node) : base(node) { }
    }
}
