using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {

    /// <summary>
    /// Plasma Engine Version
    /// </summary>
    /// <remarks>
    /// Version Sequence: Hex Isle > EoA > MOUL > MOUL Pre-5 > PotS > Prime/UU > Choru
    /// </remarks>
    public sealed class plVersion {

        ushort fEngine, fBranch;
        ushort fMajor, fMinor;

        #region Boolean Helper Properties
        /// <summary>
        /// Gets whether or not this Plasma Version is 2.0.70 (MOUL/MQO)
        /// </summary>
        public bool IsMystOnline {
            get { return this == MystOnline; }
        }

        /// <summary>
        /// Gets whether or not this Plasma Version is greater than 2.1 [EoA] or higher
        /// </summary>
        public bool IsPlasma21 {
            get { return this >= EndOfAges; }
        }

        /// <summary>
        /// Gets whether or not this Plasma Version is less than 2.0.70 [MOUL/MQO]
        /// </summary>
        public bool IsPreMystOnline {
            get { return this < MystOnline; }
        }

        /// <summary>
        /// Gets whether or not this Plasma Version is less than 2.1. [EndOfAges]
        /// </summary>
        public bool IsPlasma20 {
            get { return this < EndOfAges; }
        }

        /// <summary>
        /// Gets whether or not this Plasma Version is between 2.0.69 and 2.0.70 [Any MOUL/Live]
        /// </summary>
        public bool IsUruLive {
            get { return (this <= MystOnline) && (this >= GTLiveBeta); }
        }
        #endregion

        #region Static Helper Properties
        /// <summary>
        /// Returns a Plasma Version representing 2.0.63.11
        /// </summary>
        public static plVersion AgesBeyondMyst {
            get { return new plVersion(2, 0, 63, 11); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.1.6.0
        /// </summary>
        public static plVersion EndOfAges {
            get { return new plVersion(2, 1, 6, 0); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.0.69.1
        /// </summary>
        public static plVersion GTLiveBeta {
            get { return new plVersion(2, 0, 69, 1); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.0.69.2
        /// </summary>
        public static plVersion GTLiveBranch1 {
            get { return new plVersion(2, 0, 69, 2); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.0.69.3
        /// </summary>
        public static plVersion GTLiveBranch2 {
            get { return new plVersion(2, 0, 69, 3); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.0.69.4
        /// </summary>
        public static plVersion GTLiveBranch3 {
            get { return new plVersion(2, 0, 69, 4); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.0.69.5
        /// </summary>
        public static plVersion GTLiveBranch4 {
            get { return new plVersion(2, 0, 69, 5); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 3.0.0.0
        /// </summary>
        public static plVersion HexIsle {
            get { return new plVersion(3, 0, 0, 0); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.0.70.0
        /// </summary>
        public static plVersion MystOnline {
            get { return new plVersion(2, 0, 70, 0); }
        }

        /// <summary>
        /// Returns a Plasma Version representing 2.0.63.12
        /// </summary>
        public static plVersion PathOfTheShell {
            get { return new plVersion(2, 0, 63, 12); }
        }
        #endregion

        public plVersion(ushort engine, ushort branch, ushort major, ushort minor) {
            fEngine = engine;
            fBranch = branch;
            fMajor = major;
            fMinor = minor;
        }

        public override bool Equals(object obj) {
            if (!(obj is plVersion)) return false;
            plVersion rhs = obj as plVersion;
            return ToULong() == rhs.ToULong();
        }

        public override string ToString() {
            return String.Format("{0}.{1}.{2}.{3}", new object[] { fEngine,
                fBranch, fMajor, fMinor });
        }

        public ulong ToULong() {
            ulong data = ((ulong)fEngine << 48); //Shift over 6 bytes
            ulong bran = ((ulong)fBranch << 32); //Shift over 4 bytes
            ulong major = (ulong)(fMajor << 16); //Shift over 2 bytes

            data |= bran | major | fMinor;
            return data;
        }

        #region Operators
        public static implicit operator ulong(plVersion version) {
            return version.ToULong();
        }

        public static bool operator ==(plVersion lhs, plVersion rhs) {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(plVersion lhs, plVersion rhs) {
            return !lhs.Equals(rhs);
        }

        public static bool operator >(plVersion lhs, plVersion rhs) {
            return lhs.ToULong() > rhs.ToULong();
        }

        public static bool operator >=(plVersion lhs, plVersion rhs) {
            return lhs.ToULong() >= rhs.ToULong();
        }

        public static bool operator <(plVersion lhs, plVersion rhs) {
            return lhs.ToULong() < rhs.ToULong();
        }

        public static bool operator <=(plVersion lhs, plVersion rhs) {
            return lhs.ToULong() <= rhs.ToULong();
        }
        #endregion
    }
}
