using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plClientGuid : plCreatable {

        [Flags]
        enum Flags {
            kAccountUUID = 0x1,
            kPlayerID = 0x2,
            kTempPlayerID = 0x4,
            kCCRLevel = 0x8,
            kProtectedLogin = 0x10,
            kBuildType = 0x20,
            kPlayerName = 0x40,
            kSrcAddr = 0x80,
            kSrcPort = 0x100,
            kReserved = 0x200,
            kClientKey = 0x400
        }

        Flags fFlags;
        Guid fAcctUUID;
        uint fPlayerID;
        string fPlayerName;
        byte fCCRLevel;
        bool fProtectedLogin;
        byte fBuildType;
        uint fSrcAddr;
        ushort fSrcPort, fReserved;
        string fClientKey;

        #region Flag Properties
        public bool HasAccount {
            get { return ((fFlags & Flags.kAccountUUID) != 0); }
        }

        public bool HasBuildType {
            get { return ((fFlags & Flags.kBuildType) != 0); }
            set {
                if (HasBuildType && !value)
                    fFlags &= ~Flags.kBuildType;
                else if (!HasBuildType && value)
                    fFlags |= Flags.kBuildType;
            }
        }

        public bool HasCCRLevel {
            get { return ((fFlags & Flags.kCCRLevel) != 0); }
            set {
                if (HasCCRLevel && !value)
                    fFlags &= ~Flags.kCCRLevel;
                else if (!HasCCRLevel && value)
                    fFlags |= Flags.kCCRLevel;
            }
        }

        public bool HasClientKey {
            get { return ((fFlags & Flags.kClientKey) != 0); }
        }

        public bool HasPlayerID {
            get {
                return ((fFlags & Flags.kPlayerID) != 0) ||
                       ((fFlags & Flags.kTempPlayerID) != 0);
            }

            set {
                //Handle unsetting of both flags (why, Cyan, why???)
                if (((fFlags & Flags.kTempPlayerID) != 0) && !value)
                    fFlags &= ~Flags.kTempPlayerID;
                if (((fFlags & Flags.kPlayerID) != 0) && !value)
                    fFlags &= ~Flags.kPlayerID;

                //Just set the one flag.
                //If we need to set both, add that later...
                if (!HasPlayerID && value)
                    fFlags |= Flags.kPlayerID;
            }
        }

        public bool HasPlayerName {
            get { return ((fFlags & Flags.kPlayerName) != 0); }
        }

        public bool HasProtectedLogin {
            get { return ((fFlags & Flags.kProtectedLogin) != 0); }
            set {
                if (HasProtectedLogin && !value)
                    fFlags &= ~Flags.kProtectedLogin;
                else if (!HasProtectedLogin && value)
                    fFlags |= Flags.kProtectedLogin;
            }
        }

        public bool HasReserved {
            get { return ((fFlags & Flags.kReserved) != 0); }
            set {
                if (HasReserved && !value)
                    fFlags &= ~Flags.kReserved;
                else if (!HasReserved && value)
                    fFlags |= Flags.kReserved;
            }
        }

        public bool HasSrcAddr {
            get { return ((fFlags & Flags.kSrcAddr) != 0); }
            set {
                if (HasSrcAddr && !value)
                    fFlags &= ~Flags.kSrcAddr;
                else if (!HasSrcAddr && value)
                    fFlags |= Flags.kSrcAddr;
            }
        }

        public bool HasSrcPort {
            get { return ((fFlags & Flags.kSrcPort) != 0); }
            set {
                if (HasSrcPort && !value)
                    fFlags &= ~Flags.kSrcPort;
                else if (!HasSrcPort && value)
                    fFlags |= Flags.kSrcPort;
            }
        }
        #endregion

        public override void Read(hsStream s, hsResMgr mgr) {
            fFlags = (Flags)s.ReadShort();

            if (HasAccount)
                fAcctUUID = new Guid(s.ReadBytes(16));
            if (HasPlayerID)
                fPlayerID = s.ReadUInt();
            if (HasPlayerName)
                fPlayerName = s.ReadStdString();
            if (HasCCRLevel)
                fCCRLevel = s.ReadByte();
            if (HasProtectedLogin)
                fProtectedLogin = s.ReadBool();
            if (HasBuildType)
                fBuildType = s.ReadByte();
            if (HasSrcAddr)
                fSrcAddr = s.ReadUInt();
            if (HasSrcPort)
                fSrcPort = s.ReadUShort();
            if (HasReserved)
                fReserved = s.ReadUShort();
            if (HasClientKey)
                fClientKey = s.ReadStdString();
        }

        public override void Write(hsStream s, hsResMgr mgr) {
            s.WriteShort((short)fFlags);

            if (HasAccount)
                s.WriteBytes(fAcctUUID.ToByteArray());
            if (HasPlayerID)
                s.WriteUInt(fPlayerID);
            if (HasPlayerName)
                s.WriteStdString(fPlayerName);
            if (HasCCRLevel)
                s.WriteByte(fCCRLevel);
            if (HasProtectedLogin)
                s.WriteBool(fProtectedLogin);
            if (HasBuildType)
                s.WriteByte(fBuildType);
            if (HasSrcAddr)
                s.WriteUInt(fSrcAddr);
            if (HasSrcPort)
                s.WriteUShort(fSrcPort);
            if (HasReserved)
                s.WriteUShort(fReserved);
            if (HasClientKey)
                s.WriteStdString(fClientKey);
        }
    }
}
