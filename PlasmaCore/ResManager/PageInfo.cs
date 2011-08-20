using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    /// <summary>
    /// Represents the data contained in a Plasma Registry Page (PRP) header
    /// </summary>
    public class plPageInfo {
        [Flags]
        public enum Flags {
            kPartialPatchFile = 0x1,
            kOldDataChecksum = 0x2,
            kOldIdxChecksum = 0x4,
            kBasicChecksum = 0x8,
            kPatchHeaderOnly = 0x10,
            kChecksumMask = kBasicChecksum | kOldIdxChecksum | kOldDataChecksum,
            kPatchFlags = kPatchHeaderOnly | kPartialPatchFile,
        }

        plLocation fLocation = new plLocation();
        string fAge;
        string fChapter;
        string fPage;
        int fReleaseVersion;
        Flags fFlags;
        uint fChecksum, fDataStart, fIndexStart;

        /// <summary>
        /// Gets the Location of the Registry Page this PageInfo describes
        /// </summary>
        public plLocation Location {
            get { return fLocation; }
        }

        /// <summary>
        /// Gets the Age name
        /// </summary>
        public string Age {
            get { return fAge; }
        }

        /// <summary>
        /// Gets the Page name
        /// </summary>
        public string Page {
            get { return fPage; }
        }

        internal int IndexStart {
            get { return (int)fIndexStart; }
            set { fIndexStart = (uint)value; }
        }

        public string GetFilename(plVersion v) {
            if (v.IsPlasma20)
                return String.Format("{0}_{1}_{2}.prp", fAge, fChapter, fPage);
            else
                return String.Format("{0}_{1}.prp", fAge, fPage);
        }

        public string GetFilenameWithoutExtension(plVersion v) {
            if (v.IsPlasma20)
                return String.Format("{0}_{1}_{2}", fAge, fChapter, fPage);
            else
                return String.Format("{0}_{1}", fAge, fPage);
        }

        public void Read(hsStream s) {
            //Magically figure out what version we have...
            uint prpVer = s.ReadUInt();
            switch (prpVer) {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    s.Version = new plVersion(2, 0, 0, 0);
                    break;
                case 6:
                    s.Version = plVersion.MystOnline;
                    break;
                default:
                    //Must be some sort of Myst 5 variant...
                    s.Rewind();
                    prpVer = (uint)s.ReadUShort();
                    switch (prpVer) {
                        case 6:
                            s.Version = plVersion.EndOfAges;
                            break;
                        case 9:
                            s.Version = plVersion.HexIsle;
                            break;
                        default:
                            throw new plVersionException(prpVer);
                    }
                    break;
            }

            if (s.Version.IsPlasma21)
                IReadClassVersions(s);

            fLocation.Read(s);
            fAge = s.ReadSafeString();
            if (s.Version.IsPreMystOnline) fChapter = s.ReadSafeString();
            else if (s.Version.IsMystOnline) fChapter = "District";
            fPage = s.ReadSafeString();

            //Some more versioning stuff for Uru...
            if (s.Version.IsMystOnline)
                s.Version = new plVersion(2, 0, s.ReadUShort(), 0);
            else if (s.Version.IsPreMystOnline)
                s.Version = new plVersion(2, 0, s.ReadUShort(), s.ReadUShort());

            if (prpVer < 6) {
                if (prpVer < 5) //IndexChecksum -- deprecated...
                    s.ReadUInt();
                if (prpVer >= 2)
                    fReleaseVersion = s.ReadInt();
                if (prpVer >= 3)
                    fFlags = (Flags)s.ReadInt();
            }

            if (prpVer >= 4)
                fChecksum = s.ReadUInt();
            if (prpVer >= 5) {
                fDataStart = s.ReadUInt();
                fIndexStart = s.ReadUInt();
            } else {
                //prm/prx???
                fDataStart = 0;
                fIndexStart = s.ReadByte();
            }

            //Garbage
            if (s.Version.IsMystOnline)
                IReadClassVersions(s);
        }

        private void IReadClassVersions(hsStream s) {
            //TODO: Save this data? For now, throw it away.
            ushort nTypes = s.ReadUShort();
            for (ushort i = 0; i < nTypes; i++) {
                ushort type = plManagedType.Read(s);
                ushort vers = s.ReadUShort();

                if (vers != 0)
                    plDebugLog.GetLog("ResManager").Warn(
                        String.Format("\t* ClassVersion: {0}, {1}", 
                        plManagedType.ClassName(type), vers));
            }
        }
    }
}
