using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    /// <summary>
    /// Reference to a KeyedObject in a Registry Page
    /// </summary>
    public sealed class plKey {

        plUoid fKeyData;
        hsKeyedObject fPtr;

        /// <summary>
        /// Get the internal data structure
        /// </summary>
        internal plUoid Uoid {
            get { return fKeyData; }
        }

        /// <summary>
        /// Gets the KeyedObject this Key points to
        /// </summary>
        public hsKeyedObject Object {
            get { return fPtr; }
            internal set { fPtr = value; }
        }

        /// <summary>
        /// Gets the Location of the Registry Page this Key belongs to
        /// </summary>
        public plLocation Location {
            get { return fKeyData.fLocation; }
        }

        public plLoadMask LoadMask {
            get { return fKeyData.fLoadMask; }
            set { fKeyData.fLoadMask = value; }
        }

        /// <summary>
        /// Gets the Plasma Type of the KeyedObject this Key points to
        /// </summary>
        public ushort ClassType {
            get { return fKeyData.fClassType; }
        }

        /// <summary>
        /// Gets or sets the name of this Key
        /// </summary>
        public string ObjectName {
            get { return fKeyData.fObjectName; }
            set { fKeyData.fObjectName = value; }
        }

        public uint ObjectID {
            get { return fKeyData.fObjectID; }
        }

        public uint CloneID {
            get { return fKeyData.fCloneID; }
            set { fKeyData.fCloneID = value; }
        }

        public uint ClonePlayerID {
            get { return fKeyData.fClonePlayerID; }
            set { fKeyData.fClonePlayerID = value; }
        }

        internal plKey() { }
        internal plKey(plUoid uoid) {
            fKeyData = uoid;
        }

        internal plKey(ushort type, string objName) {
            fKeyData = new plUoid(type, objName);
        }

        public plKey Clone(uint id, uint playerID) {
            plKey key = new plKey(fKeyData.Clone());
            key.fKeyData.fCloneID = id;
            key.fKeyData.fClonePlayerID = playerID;
            return key;
        }

        public override bool Equals(object obj) {
            if (!(obj is plKey)) return false;
            plKey key = (plKey)obj;
            return fKeyData.Equals(key.Uoid);
        }

        /// <summary>
        /// Creates a FAKE plKey based on the information given.
        /// </summary>
        /// <remarks>
        /// This key will NOT be added to (or checked by) the internal KeyCollector; therefore, you should 
        /// ONLY use this method if you know what you're doing! Othwerise, you will cause duplicate keys
        /// and completely screw the garbage collection.
        /// </remarks>
        /// <param name="loc">Key's Location</param>
        /// <param name="type">Managed Type ID</param>
        /// <param name="name">Object Name</param>
        /// <param name="id">Object ID</param>
        /// <returns>Fabricated key</returns>
        public static plKey Fabricate(plLocation loc, ushort type, string name, uint id) {
            plUoid uoid = new plUoid(type, name);
            uoid.fLocation = loc;
            uoid.fObjectID = id;
            return new plKey(uoid);
        }

        public override string ToString() {
            return fKeyData.ToString();
        }
    }

    internal sealed class plUoid {
        enum ContentsFlags {
            kHasCloneIDs = 0x1,
            kHasLoadMask = 0x2,
            kHasLoadMask2 = 0x4,
        }

        internal plLocation fLocation = new plLocation();
        internal plLoadMask fLoadMask = plLoadMask.Always;
        internal ushort fClassType;
        internal string fObjectName;
        internal uint fObjectID, fCloneID, fClonePlayerID;

        internal plUoid() { }
        internal plUoid(ushort type, string name) {
            fObjectName = name;
            fClassType = type;
        }

        public plUoid Clone() {
            return (plUoid)MemberwiseClone();
        }

        public override bool Equals(object obj) {
            if (!(obj is plUoid)) return false;
            if (obj == null) return false;

            plUoid rhs = (plUoid)obj;
            if (fLocation.Equals(rhs.fLocation))
                if (fClassType == rhs.fClassType)
                    if (fObjectName.Equals(rhs.fObjectName))
                        return true;
            return false;
        }

        public void Read(hsStream s) {
            ContentsFlags contents = (ContentsFlags)s.ReadByte();
            fLocation.Read(s);
            if ((contents & ContentsFlags.kHasLoadMask) != 0)
                if (!s.Version.IsPlasma21) fLoadMask.Read(s);
            fClassType = plManagedType.Read(s);
            if (s.Version.IsUruLive || s.Version.IsPlasma21)
                fObjectID = s.ReadUInt();
            fObjectName = s.ReadSafeString();
            if (s.Version.IsPlasma20) {
                if ((contents & ContentsFlags.kHasCloneIDs) != 0) {
                    fCloneID = s.ReadUInt();
                    fClonePlayerID = s.ReadUInt();
                }
            }

            if ((contents & (ContentsFlags.kHasLoadMask | ContentsFlags.kHasLoadMask2)) != 0)
                if (s.Version.IsPlasma21)
                    fLoadMask.Read(s);
        }

        public void Write(hsStream s) {
            ContentsFlags contents = IGetContentFlags(s.Version);
            s.WriteByte((byte)contents);
            fLocation.Write(s);
            if (s.Version.IsPlasma20)
                if ((contents & ContentsFlags.kHasLoadMask) != 0)
                    fLoadMask.Write(s);
            plManagedType.Write(s, fClassType);
            if (s.Version.IsUruLive || s.Version.IsPlasma21)
                s.WriteUInt(fObjectID);
            s.WriteSafeString(fObjectName);
            if (s.Version.IsPlasma20)
                if ((contents & ContentsFlags.kHasCloneIDs) != 0) {
                    s.WriteUInt(fCloneID);
                    s.WriteUInt(fClonePlayerID);
                }

            if (s.Version.IsPlasma21)
                if ((contents & (ContentsFlags.kHasLoadMask | ContentsFlags.kHasLoadMask2)) != 0)
                    fLoadMask.Write(s);
        }

        private ContentsFlags IGetContentFlags(plVersion v) {
            ContentsFlags contents = (ContentsFlags)0;
            if (fLoadMask != plLoadMask.Always) {
                contents |= ContentsFlags.kHasLoadMask;
                if (v.IsPlasma21)
                    contents |= ContentsFlags.kHasLoadMask2;
            }

            if (v.IsPlasma20)
                if (fCloneID != 0 || fClonePlayerID != 0)
                    contents |= ContentsFlags.kHasCloneIDs;

            return contents;
        }

        public override string ToString() {
            return String.Format("{0} {1} {2}", fLocation, plManagedType.ClassName(fClassType), fObjectName);
        }
    }

    public class plLoadMask {

        #region Members/Properties
        byte[] fQuality = new byte[2];

        public static plLoadMask Always {
            get {
                return new plLoadMask(0xFF, 0xFF);
            }
        }

        public bool Used {
            get { return (fQuality[0] != 0xFF && fQuality[1] != 0xFF); }
        }
        #endregion

        public plLoadMask() { }
        public plLoadMask(byte m1, byte m2) {
            fQuality[0] = m1;
            fQuality[1] = m2;
        }

        public override bool Equals(object obj) {
            if (!(obj is plLoadMask)) return false;

            plLoadMask rhs = (plLoadMask)obj;
            return (fQuality[0] == rhs.fQuality[0] &&
                    fQuality[1] == rhs.fQuality[1]);
        }

        public void Read(hsStream s) {
            byte mask = s.ReadByte();
            fQuality[0] = (byte)((mask >> 4) | 0xF0);
            fQuality[1] = (byte)(mask | 0xF0);
        }

        public void Write(hsStream s) {
            byte mask = (byte)((fQuality[1] & 0x0F) | (fQuality[0] << 4));
            s.WriteByte(mask);
        }
    }
}
