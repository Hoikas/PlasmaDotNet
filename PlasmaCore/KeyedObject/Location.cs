using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public struct plLocation {
        [Flags]
        enum LocFlags {
            kLocalOnly = 0x1,
            kVolatile = 0x2,
            kReserved = 0x4,
            kBuiltIn = 0x8,
            kItinerant = 0x10
        }

        LocFlags fFlags;
        int fSeqPrefix, fPageNum;

        #region Flag Helper Properties
        /// <summary>
        /// Gets or sets whether or not we are referring to a BuiltIn/Textures page
        /// </summary>
        public bool BuiltIn {
            get { return ((fFlags & LocFlags.kBuiltIn) != 0); }
            set {
                if (value && !BuiltIn)
                    fFlags |= LocFlags.kBuiltIn;
                else if (!value && BuiltIn)
                    fFlags &= ~LocFlags.kBuiltIn;
            }
        }

        /// <summary>
        /// Gets or sets whether or not we are referring to an Itinerant page
        /// </summary>
        /// <remarks>
        /// Itinerant pages are never unloaded once they are loaded as part of an age
        /// </remarks>
        public bool Itinerant {
            get { return ((fFlags & LocFlags.kItinerant) != 0); }
            set {
                if (value && !Itinerant)
                    fFlags |= LocFlags.kItinerant;
                else if (!value && Itinerant)
                    fFlags &= ~LocFlags.kItinerant;
            }
        }

        /// <summary>
        /// Gets or sets whether or not we are referring to a local ONLY page
        /// </summary>
        public bool LocalOnly {
            get { return ((fFlags & LocFlags.kLocalOnly) != 0); }
            set {
                if (value && !LocalOnly)
                    fFlags |= LocFlags.kLocalOnly;
                else if (!value && LocalOnly)
                    fFlags &= ~LocFlags.kLocalOnly;
            }
        }

        /// <summary>
        /// Gets or sets whether or not we are referring to a Reserved page
        /// </summary>
        /// <remarks>
        /// Reserved pages are loaded by Plasma Games when the game starts.
        /// For example: GlobalClothing, Global Avatars, etc.
        /// </remarks>
        public bool Reserved {
            get { return ((fFlags & LocFlags.kReserved) != 0); }
            set {
                if (value && !Reserved)
                    fFlags |= LocFlags.kReserved;
                else if (!value && Reserved)
                    fFlags &= ~LocFlags.kReserved;
            }
        }

        /// <summary>
        /// Gets or sets whether or not we are referring to a Volatile page
        /// </summary>
        public bool Volatile {
            get { return ((fFlags & LocFlags.kVolatile) != 0); }
            set {
                if (value && !Volatile)
                    fFlags |= LocFlags.kVolatile;
                else if (!value && Volatile)
                    fFlags &= ~LocFlags.kVolatile;
            }
        }
        #endregion

        #region Standard Properties
        public bool Invalid {
            get { return ((int)fFlags == 0) && (fSeqPrefix == Int32.MaxValue) && (fPageNum == Int32.MaxValue); }
        }

        public int SequencePrefix {
            get { return fSeqPrefix; }
            set { fSeqPrefix = value; }
        }

        public int Page {
            get { return fPageNum; }
            set { fPageNum = value; }
        }
        #endregion

        public plLocation(int seqPrefix, int page) {
            fSeqPrefix = seqPrefix;
            fPageNum = page;
            fFlags = (LocFlags)0;
        }

        public void Read(hsStream s) {
            Parse(s.Version, s.ReadUInt());
            if (s.Version.IsPlasma21)
                fFlags = (LocFlags)s.ReadByte();
            else
                fFlags = (LocFlags)s.ReadShort();
        }

        public void Write(hsStream s) {
            s.WriteUInt(UnParse(s.Version));
            if (s.Version.IsPlasma21)
                s.WriteByte((byte)fFlags);
            else
                s.WriteShort((short)fFlags);
        }

        public void Parse(plVersion v, uint id) {
            if (id == UInt32.MaxValue) {
                fFlags = (LocFlags)0;
                fSeqPrefix = Int32.MaxValue;
                fPageNum = Int32.MaxValue;
                return;
            } else if (id == 0)
                return;

            if ((id & 0x80000000) != 0) {
                id -= (v.IsUruLive ? 0xFF000001 : 0xFFFF0001);
                fSeqPrefix = (int)(id >> (v.IsUruLive ? 16 : 8));
                fPageNum = (int)(id - (fSeqPrefix << (v.IsUruLive ? 16 : 8)));
                fSeqPrefix = -fSeqPrefix;
            } else {
                id -= 33;
                fSeqPrefix = (int)(id >> (v.IsUruLive ? 16 : 8));
                fPageNum = (int)(id - (fSeqPrefix << (v.IsUruLive ? 16 : 8)));
            }
        }

        public override string ToString() {
            return String.Format("({0}|{1})", fSeqPrefix, fPageNum); ;
        }

        public uint UnParse(plVersion v) {
            if (Invalid) return UInt32.MaxValue;

            uint pgNum = (uint)fPageNum;
            if (fSeqPrefix < 0)
                return (uint)(pgNum - (fSeqPrefix << (v.IsUruLive ? 16 : 8)) + (v.IsUruLive ? 0xFF000001 : 0xFFFF0001));
            else if (fSeqPrefix > 0)
                return (uint)(pgNum + (fSeqPrefix << (v.IsUruLive ? 16 : 8)) + 33);
            else
                return 0;
        }
    }
}
