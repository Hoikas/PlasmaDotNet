using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pnVaultNode {

        uint? fNodeID;
        DateTime? fCreateTime, fModifyTime;
        string fCreateAgeName;
        Guid? fCreateAgeGuid;
        uint? fCreatorID;
        Guid? fCreatorGuid;
        ENodeType fNodeType = ENodeType.kNodeInvalid;
        int?[] fInt32 = new int?[4];
        uint?[] fUInt32 = new uint?[4];
        Guid?[] fGuid = new Guid?[4];
        string[] fString64 = new string[6];
        string[] fIString64 = new string[2];
        string[] fText = new string[2];
        byte[][] fBlob = new byte[2][];

        #region Getters/Setters
        /// <summary>
        /// Gets or sets a vault node's ID
        /// </summary>
        /// <remarks>
        /// This value cannot be changed after it has been set
        /// </remarks>
        public uint NodeID {
            get {
                if (fNodeID.HasValue) 
                    return fNodeID.Value;
                else 
                    return 0;
            }

            set {
                if (fNodeID.HasValue)
                    throw new NotSupportedException("You cannot change this value!");
                else
                    fNodeID = value;
            }
        }

        /// <summary>
        /// Gets or sets the creation time of this vault node
        /// </summary>
        /// <remarks>
        /// This value cannot be changed after it has been set
        /// </remarks>
        public DateTime CreateTime {
            get {
                if (fCreateTime.HasValue) 
                    return fCreateTime.Value;
                else 
                    return plUnifiedTime.Epoch;
            }
            set { fCreateTime = value; }
        }

        /// <summary>
        /// Gets or sets the last time this vault node was modified
        /// </summary>
        public DateTime ModifyTime {
            get {
                if (fModifyTime.HasValue)
                    return fModifyTime.Value;
                else
                    return plUnifiedTime.Epoch;
            }
            set { fModifyTime = value; }
        }

        /// <summary>
        /// Gets or sets the name of the age that created this vault node
        /// </summary>
        ///  <remarks>
        /// You cannot change this value once it has been set
        /// </remarks>
        public string CreateAgeName {
            get { return fCreateAgeName; }
            set {
                if (fCreateAgeName == null)
                    fCreateAgeName = value;
                else
                    throw new NotSupportedException("You cannot change this value!");
            }
        }

        /// <summary>
        /// Gets or sets the Guid of the age that created this vault node
        /// </summary>
        /// <remarks>
        /// You cannot change this value once it has been set
        /// </remarks>
        public Guid CreateAgeUuid {
            get {
                if (fCreateAgeGuid.HasValue)
                    return fCreateAgeGuid.Value;
                else
                    return Guid.Empty;
            }

            set {
                if (fCreateAgeGuid.HasValue)
                    throw new NotSupportedException("You cannot change this value!");
                else
                    fCreateAgeGuid = value;
            }
        }

        /// <summary>
        /// Gets or sets the ID of the vault node that created this vault node
        /// </summary>
        /// <remarks>
        /// You cannot change this value once it has been set
        /// </remarks>
        public uint CreatorID {
            get {
                if (fCreatorID.HasValue)
                    return fCreatorID.Value;
                else
                    return 0;
            }

            set {
                if (fCreatorID.HasValue)
                    throw new NotSupportedException("You cannot change this value!");
                else
                    fCreatorID = value;
            }
        }

        /// <summary>
        /// Gets or sets the Guid of the account that created this vault node
        /// </summary>
        ///  <remarks>
        /// You cannot change this value once it has been set
        /// </remarks>
        public Guid CreatorUuid {
            get {
                if (fCreatorGuid.HasValue)
                    return fCreatorGuid.Value;
                else
                    return Guid.Empty;
            }

            set {
                if (fCreatorGuid.HasValue)
                    throw new NotSupportedException("You cannot change this value!");
                else
                    fCreatorGuid = value;
            }
        }

        /// <summary>
        /// Gets or sets the vault node's type
        /// </summary>
        ///  <remarks>
        /// You cannot change this value once it has been set
        /// </remarks>
        public ENodeType NodeType {
            get { return fNodeType; }
            set {
                if (fNodeType == ENodeType.kNodeInvalid)
                    fNodeType = value;
                else
                    throw new NotSupportedException("You cannot change this value!");
            }
        }

        public int? Int32_1 {
            get { return fInt32[0]; }
            set { fInt32[0] = value; }
        }

        public int? Int32_2 {
            get { return fInt32[1]; }
            set { fInt32[1] = value; }
        }

        public int? Int32_3 {
            get { return fInt32[2]; }
            set { fInt32[2] = value; }
        }

        public int? Int32_4 {
            get { return fInt32[3]; }
            set { fInt32[3] = value; }
        }

        public uint? UInt32_1 {
            get { return fUInt32[0]; }
            set { fUInt32[0] = value; }
        }

        public uint? UInt32_2 {
            get { return fUInt32[1]; }
            set { fUInt32[1] = value; }
        }

        public uint? UInt32_3 {
            get { return fUInt32[2]; }
            set { fUInt32[2] = value; }
        }

        public uint? UInt32_4 {
            get { return fUInt32[3]; }
            set { fUInt32[3] = value; }
        }

        public Guid? Uuid_1 {
            get { return fGuid[0]; }
            set { fGuid[0] = value; }
        }

        public Guid? Uuid_2 {
            get { return fGuid[1]; }
            set { fGuid[1] = value; }
        }

        public Guid? Uuid_3 {
            get { return fGuid[2]; }
            set { fGuid[2] = value; }
        }

        public Guid? Uuid_4 {
            get { return fGuid[3]; }
            set { fGuid[3] = value; }
        }

        public string String64_1 {
            get { return fString64[0]; }
            set { fString64[0] = value; }
        }

        public string String64_2 {
            get { return fString64[1]; }
            set { fString64[1] = value; }
        }

        public string String64_3 {
            get { return fString64[2]; }
            set { fString64[2] = value; }
        }

        public string String64_4 {
            get { return fString64[3]; }
            set { fString64[3] = value; }
        }

        public string String64_5 {
            get { return fString64[4]; }
            set { fString64[4] = value; }
        }

        public string String64_6 {
            get { return fString64[5]; }
            set { fString64[5] = value; }
        }

        public string IString64_1 {
            get { return fIString64[0]; }
            set { fIString64[0] = value; }
        }

        public string IString64_2 {
            get { return fIString64[1]; }
            set { fIString64[1] = value; }
        }

        public string Text_1 {
            get { return fText[0]; }
            set { fText[0] = value; }
        }

        public string Text_2 {
            get { return fText[1]; }
            set { fText[1] = value; }
        }

        public byte[] Blob_1 {
            get { return fBlob[0]; }
            set { fBlob[0] = value; }
        }

        public byte[] Blob_2 {
            get { return fBlob[1]; }
            set { fBlob[1] = value; }
        }
        #endregion

        /// <summary>
        /// Gets or sets the value of the specified field of this vault node
        /// </summary>
        /// <param name="field">The vault field requested</param>
        /// <returns>The value of the field</returns>
        /// <remarks>While pnVaultNodeFields is indeed a bitfield, you should only pass ONE field to this
        /// indexer. We do NOT support getting multiple field values in one call</remarks>
        public object this[pnVaultNodeFields field] {
            get {
                switch (field) {
                    case pnVaultNodeFields.Blob_1:
                        return fBlob[0];
                    case pnVaultNodeFields.Blob_2:
                        return fBlob[1];
                    case pnVaultNodeFields.CreateAgeName:
                        return fCreateAgeName;
                    case pnVaultNodeFields.CreateAgeUuid:
                        if (fCreateAgeGuid.HasValue)
                            return fCreateAgeGuid.Value;
                        break;
                    case pnVaultNodeFields.CreateTime:
                        if (fCreateTime.HasValue)
                            return fCreateTime;
                        break;
                    case pnVaultNodeFields.CreatorIdx:
                        if (fCreatorID.HasValue)
                            return fCreatorID.Value;
                        break;
                    case pnVaultNodeFields.CreatorUuid:
                        if (fCreatorGuid.HasValue)
                            return fCreatorGuid.Value;
                        break;
                    case pnVaultNodeFields.Int32_1:
                        if (fInt32[0].HasValue)
                            return fInt32[0];
                        break;
                    case pnVaultNodeFields.Int32_2:
                        if (fInt32[1].HasValue)
                            return fInt32[1];
                        break;
                    case pnVaultNodeFields.Int32_3:
                        if (fInt32[2].HasValue)
                            return fInt32[2];
                        break;
                    case pnVaultNodeFields.Int32_4:
                        if (fInt32[3].HasValue)
                            return fInt32[3];
                        break;
                    case pnVaultNodeFields.IString64_1:
                        return fIString64[0];
                    case pnVaultNodeFields.IString64_2:
                        return fIString64[1];
                    case pnVaultNodeFields.ModifyTime:
                        if (fModifyTime.HasValue)
                            return fModifyTime.Value;
                        break;
                    case pnVaultNodeFields.NodeIdx:
                        if (fNodeID.HasValue)
                            return fNodeID;
                        break;
                    case pnVaultNodeFields.NodeType:
                        // Returning this as an integer makes the most sense for the
                        // intended use case. (That being constructing SQL queries)
                        return (int)fNodeType;
                    case pnVaultNodeFields.String64_1:
                        return fString64[0];
                    case pnVaultNodeFields.String64_2:
                        return fString64[1];
                    case pnVaultNodeFields.String64_3:
                        return fString64[2];
                    case pnVaultNodeFields.String64_4:
                        return fString64[3];
                    case pnVaultNodeFields.String64_5:
                        return fString64[4];
                    case pnVaultNodeFields.String64_6:
                        return fString64[5];
                    case pnVaultNodeFields.Text_1:
                        return fText[0];
                    case pnVaultNodeFields.Text_2:
                        return fText[1];
                    case pnVaultNodeFields.UInt32_1:
                        if (fUInt32[0].HasValue)
                            return fUInt32[0];
                        break;
                    case pnVaultNodeFields.UInt32_2:
                        if (fUInt32[1].HasValue)
                            return fUInt32[1];
                        break;
                    case pnVaultNodeFields.UInt32_3:
                        if (fUInt32[2].HasValue)
                            return fUInt32[2];
                        break;
                    case pnVaultNodeFields.UInt32_4:
                        if (fUInt32[3].HasValue)
                            return fInt32[3];
                        break;
                    case pnVaultNodeFields.Uuid_1:
                        if (fGuid[0].HasValue)
                            return fGuid[0];
                        break;
                    case pnVaultNodeFields.Uuid_2:
                        if (fGuid[1].HasValue)
                            return fGuid[1];
                        break;
                    case pnVaultNodeFields.Uuid_3:
                        if (fGuid[2].HasValue)
                            return fGuid[2];
                        break;
                    case pnVaultNodeFields.Uuid_4:
                        if (fGuid[3].HasValue)
                            return fInt32[3];
                        break;
                }

                // If we're still here, then we must have been asked for an unused field...
                // We'll call that NULL :)
                return null;
            }

            set {
                // We'll be using the public setters here because they often implement some
                // useful validation and/or sanity checks that we don't want to bypass.
                switch (field) {
                    case pnVaultNodeFields.Blob_1:
                        Blob_1 = (byte[])value;
                        break;
                    case pnVaultNodeFields.Blob_2:
                        Blob_2 = (byte[])value;
                        break;
                    case pnVaultNodeFields.CreateAgeName:
                        CreateAgeName = value.ToString();
                        break;
                    case pnVaultNodeFields.CreateAgeUuid:
                        CreateAgeUuid = (Guid)value;
                        break;
                    case pnVaultNodeFields.CreateTime:
                        CreateTime = (DateTime)value;
                        break;
                    case pnVaultNodeFields.CreatorIdx:
                        CreatorID = (uint)value;
                        break;
                    case pnVaultNodeFields.CreatorUuid:
                        CreatorUuid = (Guid)value;
                        break;
                    case pnVaultNodeFields.Int32_1:
                        Int32_1 = (int)value;
                        break;
                    case pnVaultNodeFields.Int32_2:
                        Int32_2 = (int)value;
                        break;
                    case pnVaultNodeFields.Int32_3:
                        Int32_3 = (int)value;
                        break;
                    case pnVaultNodeFields.Int32_4:
                        Int32_4 = (int)value;
                        break;
                    case pnVaultNodeFields.IString64_1:
                        IString64_1 = value.ToString();
                        break;
                    case pnVaultNodeFields.IString64_2:
                        IString64_2 = value.ToString();
                        break;
                    case pnVaultNodeFields.ModifyTime:
                        ModifyTime = (DateTime)value;
                        break;
                    case pnVaultNodeFields.NodeIdx:
                        NodeID = (uint)value;
                        break;
                    case pnVaultNodeFields.NodeType:
                        NodeType = (ENodeType)Convert.ToInt32(value);
                        break;
                    case pnVaultNodeFields.String64_1:
                        String64_1 = value.ToString();
                        break;
                    case pnVaultNodeFields.String64_2:
                        String64_2 = value.ToString();
                        break;
                    case pnVaultNodeFields.String64_3:
                        String64_3 = value.ToString();
                        break;
                    case pnVaultNodeFields.String64_4:
                        String64_4 = value.ToString();
                        break;
                    case pnVaultNodeFields.String64_5:
                        String64_5 = value.ToString();
                        break;
                    case pnVaultNodeFields.String64_6:
                        String64_6 = value.ToString();
                        break;
                    case pnVaultNodeFields.Text_1:
                        Text_1 = value.ToString();
                        break;
                    case pnVaultNodeFields.Text_2:
                        Text_2 = value.ToString();
                        break;
                    case pnVaultNodeFields.UInt32_1:
                        UInt32_1 = (uint)value;
                        break;
                    case pnVaultNodeFields.UInt32_2:
                        UInt32_2 = (uint)value;
                        break;
                    case pnVaultNodeFields.UInt32_3:
                        UInt32_3 = (uint)value;
                        break;
                    case pnVaultNodeFields.UInt32_4:
                        UInt32_4 = (uint)value;
                        break;
                    case pnVaultNodeFields.Uuid_1:
                        Uuid_1 = (Guid)value;
                        break;
                    case pnVaultNodeFields.Uuid_2:
                        Uuid_2 = (Guid)value;
                        break;
                    case pnVaultNodeFields.Uuid_3:
                        Uuid_3 = (Guid)value;
                        break;
                    case pnVaultNodeFields.Uuid_4:
                        Uuid_4 = (Guid)value;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets a bitfield describing the fields used by this vault node
        /// </summary>
        public pnVaultNodeFields Fields {
            get {
                pnVaultNodeFields f = (pnVaultNodeFields)0;
                if (fNodeID.HasValue)
                    f |= pnVaultNodeFields.NodeIdx;
                if (fCreateTime.HasValue)
                    f |= pnVaultNodeFields.CreateTime;
                if (fCreateAgeName != null)
                    f |= pnVaultNodeFields.CreateAgeName;
                if (fCreateAgeGuid.HasValue)
                    f |= pnVaultNodeFields.CreateAgeUuid;
                if (fCreatorID.HasValue)
                    f |= pnVaultNodeFields.CreatorIdx;
                if (fCreatorGuid.HasValue)
                    f |= pnVaultNodeFields.CreatorUuid;
                if (fNodeType != ENodeType.kNodeInvalid)
                    f |= pnVaultNodeFields.NodeType;
                if (fInt32[0].HasValue)
                    f |= pnVaultNodeFields.Int32_1;
                if (fInt32[1].HasValue)
                    f |= pnVaultNodeFields.Int32_2;
                if (fInt32[2].HasValue)
                    f |= pnVaultNodeFields.Int32_3;
                if (fInt32[3].HasValue)
                    f |= pnVaultNodeFields.Int32_4;
                if (fUInt32[0].HasValue)
                    f |= pnVaultNodeFields.UInt32_1;
                if (fUInt32[1].HasValue)
                    f |= pnVaultNodeFields.UInt32_2;
                if (fUInt32[2].HasValue)
                    f |= pnVaultNodeFields.UInt32_3;
                if (fUInt32[3].HasValue)
                    f |= pnVaultNodeFields.UInt32_4;
                if (fGuid[0].HasValue)
                    f |= pnVaultNodeFields.Uuid_1;
                if (fGuid[1].HasValue)
                    f |= pnVaultNodeFields.Uuid_2;
                if (fGuid[2].HasValue)
                    f |= pnVaultNodeFields.Uuid_3;
                if (fGuid[3].HasValue)
                    f |= pnVaultNodeFields.Uuid_4;
                if (fString64[0] != null)
                    f |= pnVaultNodeFields.String64_1;
                if (fString64[1] != null)
                    f |= pnVaultNodeFields.String64_2;
                if (fString64[2] != null)
                    f |= pnVaultNodeFields.String64_3;
                if (fString64[3] != null)
                    f |= pnVaultNodeFields.String64_4;
                if (fString64[4] != null)
                    f |= pnVaultNodeFields.String64_5;
                if (fString64[5] != null)
                    f |= pnVaultNodeFields.String64_6;
                if (fIString64[0] != null)
                    f |= pnVaultNodeFields.IString64_1;
                if (fIString64[1] != null)
                    f |= pnVaultNodeFields.IString64_2;
                if (fText[0] != null)
                    f |= pnVaultNodeFields.Text_1;
                if (fText[1] != null)
                    f |= pnVaultNodeFields.Text_2;
                if (fBlob[0] != null)
                    if (fBlob[0].Length != 0)
                        f |= pnVaultNodeFields.Blob_1;
                if (fBlob[1] != null)
                    if (fBlob[1].Length != 0)
                        f |= pnVaultNodeFields.Blob_2;
                return f;
            }
        }

        public pnVaultNode() { }
        public pnVaultNode(ENodeType type) { fNodeType = type; }

        public void Read(hsStream s) {
            pnVaultNodeFields f = (pnVaultNodeFields)s.ReadULong();
            for (ulong bit = 1; bit != 0 && bit <= (ulong)f; bit <<= 1) {
                switch (f & (pnVaultNodeFields)bit) {
                    case pnVaultNodeFields.Blob_1:
                        fBlob[0] = s.ReadBytes(s.ReadInt());
                        break;
                    case pnVaultNodeFields.Blob_2:
                        fBlob[1] = s.ReadBytes(s.ReadInt());
                        break;
                    case pnVaultNodeFields.CreateAgeName:
                        fCreateAgeName = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.CreateAgeUuid:
                        fCreateAgeGuid = pnHelpers.ReadUuid(s);
                        break;
                    case pnVaultNodeFields.CreateTime:
                        fCreateTime = plUnifiedTime.Epoch.AddSeconds((double)s.ReadInt());
                        break;
                    case pnVaultNodeFields.CreatorIdx:
                        fCreatorID = s.ReadUInt();
                        break;
                    case pnVaultNodeFields.CreatorUuid:
                        fCreatorGuid = pnHelpers.ReadUuid(s);
                        break;
                    case pnVaultNodeFields.Int32_1:
                        fInt32[0] = s.ReadInt();
                        break;
                    case pnVaultNodeFields.Int32_2:
                        fInt32[1] = s.ReadInt();
                        break;
                    case pnVaultNodeFields.Int32_3:
                        fInt32[2] = s.ReadInt();
                        break;
                    case pnVaultNodeFields.Int32_4:
                        fInt32[3] = s.ReadInt();
                        break;
                    case pnVaultNodeFields.IString64_1:
                        fIString64[0] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.IString64_2:
                        fIString64[1] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.ModifyTime:
                        fModifyTime = plUnifiedTime.Epoch.AddSeconds((double)s.ReadInt());
                        break;
                    case pnVaultNodeFields.NodeIdx:
                        fNodeID = s.ReadUInt();
                        break;
                    case pnVaultNodeFields.NodeType:
                        fNodeType = (ENodeType)s.ReadUInt();
                        break;
                    case pnVaultNodeFields.String64_1:
                        fString64[0] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.String64_2:
                        fString64[1] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.String64_3:
                        fString64[2] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.String64_4:
                        fString64[3] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.String64_5:
                        fString64[4] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.String64_6:
                        fString64[5] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.Text_1:
                        fText[0] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.Text_2:
                        fText[1] = pnHelpers.ReadString(s);
                        break;
                    case pnVaultNodeFields.UInt32_1:
                        fUInt32[0] = s.ReadUInt();
                        break;
                    case pnVaultNodeFields.UInt32_2:
                        fUInt32[1] = s.ReadUInt();
                        break;
                    case pnVaultNodeFields.UInt32_3:
                        fUInt32[2] = s.ReadUInt();
                        break;
                    case pnVaultNodeFields.UInt32_4:
                        fUInt32[3] = s.ReadUInt();
                        break;
                    case pnVaultNodeFields.Uuid_1:
                        fGuid[0] = pnHelpers.ReadUuid(s);
                        break;
                    case pnVaultNodeFields.Uuid_2:
                        fGuid[1] = pnHelpers.ReadUuid(s);
                        break;
                    case pnVaultNodeFields.Uuid_3:
                        fGuid[2] = pnHelpers.ReadUuid(s);
                        break;
                    case pnVaultNodeFields.Uuid_4:
                        fGuid[3] = pnHelpers.ReadUuid(s);
                        break;
                }
            }
        }

        public void Write(hsStream s) {
            pnVaultNodeFields f = Fields;
            s.WriteULong((ulong)f);
            for (ulong bit = 1; bit != 0 && bit <= (ulong)f; bit <<= 1) {
                switch (f & (pnVaultNodeFields)bit) {
                    case pnVaultNodeFields.Blob_1:
                        s.WriteInt(fBlob[0].Length);
                        s.WriteBytes(fBlob[0]);
                        break;
                    case pnVaultNodeFields.Blob_2:
                        s.WriteInt(fBlob[1].Length);
                        s.WriteBytes(fBlob[1]);
                        break;
                    case pnVaultNodeFields.CreateAgeName:
                        pnHelpers.WriteString(s, fCreateAgeName);
                        break;
                    case pnVaultNodeFields.CreateAgeUuid:
                        pnHelpers.WriteUuid(s, fCreateAgeGuid.Value);
                        break;
                    case pnVaultNodeFields.CreateTime:
                        TimeSpan cts = fCreateTime.Value - plUnifiedTime.Epoch;
                        s.WriteUInt((uint)cts.Seconds);
                        break;
                    case pnVaultNodeFields.CreatorIdx:
                        s.WriteUInt(fCreatorID.Value);
                        break;
                    case pnVaultNodeFields.CreatorUuid:
                        pnHelpers.WriteUuid(s, fCreatorGuid.Value);
                        break;
                    case pnVaultNodeFields.Int32_1:
                        s.WriteInt(fInt32[0].Value);
                        break;
                    case pnVaultNodeFields.Int32_2:
                        s.WriteInt(fInt32[1].Value);
                        break;
                    case pnVaultNodeFields.Int32_3:
                        s.WriteInt(fInt32[2].Value);
                        break;
                    case pnVaultNodeFields.Int32_4:
                        s.WriteInt(fInt32[3].Value);
                        break;
                    case pnVaultNodeFields.IString64_1:
                        pnHelpers.WriteString(s, fIString64[0]);
                        break;
                    case pnVaultNodeFields.IString64_2:
                        pnHelpers.WriteString(s, fIString64[1]);
                        break;
                    case pnVaultNodeFields.ModifyTime:
                        TimeSpan mts = fModifyTime.Value - plUnifiedTime.Epoch;
                        s.WriteUInt((uint)mts.Seconds);
                        break;
                    case pnVaultNodeFields.NodeIdx:
                        s.WriteUInt(fNodeID.Value);
                        break;
                    case pnVaultNodeFields.NodeType:
                        s.WriteUInt((uint)fNodeType);
                        break;
                    case pnVaultNodeFields.String64_1:
                        pnHelpers.WriteString(s, fString64[0]);
                        break;
                    case pnVaultNodeFields.String64_2:
                        pnHelpers.WriteString(s, fString64[1]);
                        break;
                    case pnVaultNodeFields.String64_3:
                        pnHelpers.WriteString(s, fString64[2]);
                        break;
                    case pnVaultNodeFields.String64_4:
                        pnHelpers.WriteString(s, fString64[3]);
                        break;
                    case pnVaultNodeFields.String64_5:
                        pnHelpers.WriteString(s, fString64[4]);
                        break;
                    case pnVaultNodeFields.String64_6:
                        pnHelpers.WriteString(s, fString64[5]);
                        break;
                    case pnVaultNodeFields.Text_1:
                        pnHelpers.WriteString(s, fText[0]);
                        break;
                    case pnVaultNodeFields.Text_2:
                        pnHelpers.WriteString(s, fText[1]);
                        break;
                    case pnVaultNodeFields.UInt32_1:
                        s.WriteUInt(fUInt32[0].Value);
                        break;
                    case pnVaultNodeFields.UInt32_2:
                        s.WriteUInt(fUInt32[1].Value);
                        break;
                    case pnVaultNodeFields.UInt32_3:
                        s.WriteUInt(fUInt32[2].Value);
                        break;
                    case pnVaultNodeFields.UInt32_4:
                        s.WriteUInt(fUInt32[3].Value);
                        break;
                    case pnVaultNodeFields.Uuid_1:
                        pnHelpers.WriteUuid(s, fGuid[0].Value);
                        break;
                    case pnVaultNodeFields.Uuid_2:
                        pnHelpers.WriteUuid(s, fGuid[1].Value);
                        break;
                    case pnVaultNodeFields.Uuid_3:
                        pnHelpers.WriteUuid(s, fGuid[2].Value);
                        break;
                    case pnVaultNodeFields.Uuid_4:
                        pnHelpers.WriteUuid(s, fGuid[3].Value);
                        break;
                }
            }
        }

        public byte[] ToArray() {
            // Lots of overhead for such a simple operation...
            // All because eap sucks
            MemoryStream ms = new MemoryStream();
            hsStream s = new hsStream(ms);

            Write(s);
            byte[] buf = ms.ToArray();

            s.Close();
            ms.Close();

            return buf;
        }
    }

    public class pnVaultNodeRef {

        public uint fParent;
        public uint fChild;
        public uint fSaver;

        public pnVaultNodeRef() { }
        public pnVaultNodeRef(uint parent, uint child, uint saver) {
            fParent = parent;
            fChild = child;
            fSaver = saver;
        }

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
