using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Color = System.Drawing.Color;

namespace Plasma {
    internal static class plStateVarNotificationInfo {

        /// <summary>
        /// Reads a "Hint String" used for SDL Notifications
        /// </summary>
        /// <param name="s">Stream to read from</param>
        /// <returns>Hint String</returns>
        public static string Read(hsStream s) {
            s.ReadByte(); // Save Flags (aka Garbage)
            return s.ReadSafeString();
        }

        public static void Write(hsStream s, string hint) {
            s.WriteByte(0);
            s.WriteSafeString(hint);
        }
    }

    public abstract class plStateVariable {

        string fHint;

        /// <summary>
        /// Gets whether or not this value has been changed from the last saved state
        /// </summary>
        public abstract bool Dirty {
            get;
        }

        public abstract bool Used {
            get;
        }

        public abstract plVarDescriptor Descriptor {
            get;
        }

        public virtual void Read(hsStream s, plResManager mgr) {
            plSDL.ContentsFlags savFlags = (plSDL.ContentsFlags)s.ReadByte();
            if (savFlags.HasFlag(plSDL.ContentsFlags.kHasNotificationInfo))
                fHint = plStateVarNotificationInfo.Read(s);
        }

        public virtual void Write(hsStream s, plResManager mgr) {
            if (fHint == null)
                s.WriteByte(0);
            else {
                s.WriteByte((byte)plSDL.ContentsFlags.kHasNotificationInfo);
                plStateVarNotificationInfo.Write(s, fHint);
            }
        }
    }

    public class plSimpleStateVariable : plStateVariable, IEnumerable<object> {

        [Flags]
        protected enum Flags {
            kDirty = 0x1,
            kUsed = 0x2
        }

        Flags fFlags;
        plSimpleVarDescriptor fDesc;
        DateTime fModified = plUnifiedTime.Epoch;
        List<object> fValues = new List<object>();

        /// <summary>
        /// Gets or sets a simple state value at the provided index
        /// </summary>
        /// <param name="index">The index you with to manipulate</param>
        /// <returns>The user specified state value if this variable is used. Otherwise, the default value
        /// as specified in the SDL file</returns>
        public object this[int index] {
            get {
                // If we're not used, then let's be nice and return the default value...
                if (!Used)
                    return fDesc.DefaultValue;

                if (!fDesc.VariableLength && index > fDesc.Count)
                    throw new IndexOutOfRangeException();
                else if (fValues.Count > index)
                    return fDesc.DefaultValue;
                else
                    return fValues[index];
            }

            set {
                // We'll pretend NULL == "default state"
                object val = value;
                if (value == fDesc.DefaultValue)
                    val = null;

                if (!fDesc.VariableLength && index > fDesc.Count)
                    throw new IndexOutOfRangeException();
                else if (fValues.Count > index) {
                    fValues.Insert(index, val);
                } else
                    fValues[index] = val;

                // Set the flags
                fFlags |= Flags.kDirty;
                fFlags &= ~Flags.kUsed;
                foreach (object i in fValues) {
                    if (i != null) {
                        fFlags |= Flags.kUsed;
                        break;
                    }
                }
            }
        }

        public override plVarDescriptor Descriptor {
            get { return fDesc; }
        }

        public override bool Dirty {
            get { return fFlags.HasFlag(Flags.kDirty); }
        }

        public override bool Used {
            get { return fFlags.HasFlag(Flags.kUsed); }
        }

        internal plSimpleStateVariable(plSimpleVarDescriptor desc) {
            fDesc = desc;
        }

        #region IEnumerable<object> Members
        public IEnumerator<object> GetEnumerator() {
            return fValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return fValues.GetEnumerator();
        }
        #endregion

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            // HOW MANY TIMES WILL WE READ THIS *&^*$^@#ing BYTE?!?!?!
            // ... At least there is no io version *scnr*
            plSDL.ContentsFlags savFlags = (plSDL.ContentsFlags)s.ReadByte();
            if (savFlags.HasFlag(plSDL.ContentsFlags.kHasTimeStamp))
                fModified = plUnifiedTime.Read(s);

            if (!savFlags.HasFlag(plSDL.ContentsFlags.kSameAsDefault)) {
                int count = fDesc.Count;
                if (fDesc.VariableLength)
                    count = s.ReadInt();
                fValues.Clear();
                fValues.Capacity = count; // Optimization

                for (int i = 0; i < count; i++)
                    IReadData(s, mgr, i);
                fFlags |= Flags.kUsed;
            }
        }

        private void IReadData(hsStream s, plResManager mgr, int index) {
            switch (fDesc.Type) {
                case plAtomicType.kAgeTimeOfDay:
                    // Nothing to read in...
                    break;
                case plAtomicType.kBool:
                    fValues.Insert(index, s.ReadBool());
                    break;
                case plAtomicType.kByte:
                    fValues.Insert(index, s.ReadByte());
                    break;
                case plAtomicType.kCreatable:
                    ushort hClass = plManagedType.Read(s);
                    plCreatable pCre = plFactory.Create(hClass);
                    s.ReadInt(); // Size...
                    pCre.Read(s, mgr);

                    fValues.Insert(index, pCre);
                    break;
                case plAtomicType.kDouble:
                    fValues.Insert(index, s.ReadDouble());
                    break;
                case plAtomicType.kFloat:
                    fValues.Insert(index, s.ReadFloat());
                    break;
                case plAtomicType.kInt:
                    fValues.Insert(index, s.ReadInt());
                    break;
                case plAtomicType.kKey:
                    // Yes, we really do read a UOID rather than a KEY
                    fValues.Insert(index, mgr.ReadUoid(s));
                    break;
                case plAtomicType.kPoint3:
                    hsPoint3 point = new hsPoint3();
                    point.Read(s);
                    fValues.Insert(index, point);
                    break;
                case plAtomicType.kQuaternion:
                    hsQuat quat = new hsQuat();
                    quat.Read(s);
                    fValues.Insert(index, quat);
                    break;
                case plAtomicType.kRGB:
                    fValues.Insert(index, hsColorRGBA.ReadRGB(s));
                    break;
                case plAtomicType.kRGB8:
                    fValues.Insert(index, hsColorRGBA.ReadRGB8(s));
                    break;
                case plAtomicType.kRGBA:
                    fValues.Insert(index, hsColorRGBA.ReadRGBA(s));
                    break;
                case plAtomicType.kRGBA8:
                    fValues.Insert(index, hsColorRGBA.ReadRGBA8(s));
                    break;
                case plAtomicType.kShort:
                    fValues.Insert(index, s.ReadShort());
                    break;
                case plAtomicType.kString32:
                    string str = Encoding.UTF8.GetString(s.ReadBytes(32));
                    fValues.Insert(index, str);
                    break;
                case plAtomicType.kTime:
                    fValues.Insert(index, plUnifiedTime.Read(s));
                    break;
                case plAtomicType.kVector3:
                    hsVector3 vec = new hsVector3();
                    vec.Read(s);
                    fValues.Insert(index, vec);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            // Stupid, stupid, stupid.
            plSDL.ContentsFlags savFlags = 0;
            if (fModified != plUnifiedTime.Epoch)
                savFlags |= plSDL.ContentsFlags.kHasTimeStamp;
            if (!Used)
                savFlags |= plSDL.ContentsFlags.kSameAsDefault;
            s.WriteByte((byte)savFlags);

            if (savFlags.HasFlag(plSDL.ContentsFlags.kHasTimeStamp))
                plUnifiedTime.Write(s, fModified);

            if (Used) {
                if (fDesc.VariableLength)
                    s.WriteInt(fValues.Count);
                for (int i = 0; i < fValues.Count; i++)
                    IWriteData(s, mgr, i);
            }

            fFlags &= ~Flags.kDirty;
        }

        private void IWriteData(hsStream s, plResManager mgr, int index) {
            switch (fDesc.Type) {
                case plAtomicType.kAgeTimeOfDay:
                    // Nothing to write
                    break;
                case plAtomicType.kBool:
                    s.WriteBool((bool)fValues[index]);
                    break;
                case plAtomicType.kByte:
                    s.WriteByte((byte)fValues[index]);
                    break;
                case plAtomicType.kCreatable:
                    plCreatable pCre = (plCreatable)fValues[index];
                    plManagedType.Write(s, plFactory.ClassIndex(pCre));

                    // Le sigh...
                    MemoryStream ms = new MemoryStream();
                    hsStream hack = new hsStream(ms);
                    hack.Version = s.Version;
                    pCre.Write(hack, mgr);
                    byte[] pCreBuf = ms.ToArray();
                    hack.Close();
                    ms.Close();

                    s.WriteInt(pCreBuf.Length);
                    s.WriteBytes(pCreBuf);
                    break;
                case plAtomicType.kDouble:
                    s.WriteDouble((double)fValues[index]);
                    break;
                case plAtomicType.kFloat:
                    s.WriteFloat((float)fValues[index]);
                    break;
                case plAtomicType.kInt:
                    s.WriteInt((int)fValues[index]);
                    break;
                case plAtomicType.kKey:
                    mgr.WriteUoid(s, (plKey)fValues[index]);
                    break;
                case plAtomicType.kPoint3:
                    ((hsPoint3)fValues[index]).Write(s);
                    break;
                case plAtomicType.kQuaternion:
                    ((hsQuat)fValues[index]).Write(s);
                    break;
                case plAtomicType.kRGB:
                    hsColorRGBA.WriteRGB(s, (Color)fValues[index]);
                    break;
                case plAtomicType.kRGB8:
                    hsColorRGBA.WriteRGB8(s, (Color)fValues[index]);
                    break;
                case plAtomicType.kRGBA:
                    hsColorRGBA.WriteRGBA(s, (Color)fValues[index]);
                    break;
                case plAtomicType.kRGBA8:
                    hsColorRGBA.WriteRGBA8(s, (Color)fValues[index]);
                    break;
                case plAtomicType.kShort:
                    s.WriteShort((short)fValues[index]);
                    break;
                case plAtomicType.kString32:
                    byte[] buf = new byte[32];
                    int count = ((string)fValues[index]).Length;
                    if (count > 32)
                        count = 32;
                    Encoding.UTF8.GetBytes((string)fValues[index], 0, count, buf, 0);
                    s.WriteBytes(buf);
                    break;
                case plAtomicType.kTime:
                    plUnifiedTime.Write(s, (DateTime)fValues[index]);
                    break;
                case plAtomicType.kVector3:
                    ((hsVector3)fValues[index]).Write(s);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public class plSDStateVariable : plStateVariable, IEnumerable<plStateDataRecord> {

        plSDVarDescriptor fDesc;
        plSDLMgr fMgr;
        List<plStateDataRecord> fRecords = new List<plStateDataRecord>();

        public plStateDataRecord this[int index] {
            get { return fRecords[index]; }
            set {
                if (!fDesc.VariableLength && fRecords.Count < index)
                    throw new IndexOutOfRangeException();
                else if (fDesc.VariableLength && fRecords.Count < index)
                    fRecords.Insert(index, value);
                else
                    fRecords[index] = value;
            }
        }

        public override plVarDescriptor Descriptor {
            get { return fDesc; }
        }

        public override bool Dirty {
            get {
                foreach (plStateDataRecord rec in fRecords)
                    if (rec.Dirty) return true;
                return false;
            }
        }

        public override bool Used {
            get {
                foreach (plStateDataRecord rec in fRecords)
                    if (rec.Used) return true;
                return false;
            }
        }

        internal plSDStateVariable(plSDLMgr mgr, plSDVarDescriptor desc) {
            fMgr = mgr;
            fDesc = desc;
        }

        #region IEnumerable
        public IEnumerator<plStateDataRecord> GetEnumerator() {
            return fRecords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return fRecords.GetEnumerator();
        }
        #endregion

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);
            s.ReadByte(); // saveFlags--Garbage here

            int count = fDesc.Count;
            if (fDesc.VariableLength)
                count = s.ReadInt();
            fRecords.Clear();
            fRecords.Capacity = count; // Optimization

            // Cyan's trick to prevent sending unused data records
            int varsize = fDesc.VariableLength ? Int32.MaxValue : count;
            int used = plSDL.VariableLengthRead(s, varsize);
            bool all = (used == count);

            // Look up the State Descriptor... We need to supply this to the
            // State Data Record ourselves because there is no embedded header
            plStateDescriptor desc = fMgr.FindDescriptor(fDesc);
            for (int i = 0; i < count; i++) {
                fRecords.Insert(i, new plStateDataRecord(fMgr));
                fRecords[i].Descriptor = desc;
            }

            // Now actually read them in
            for (int i = 0; i < used; i++) {
                int index = i;
                if (!all)
                    index = plSDL.VariableLengthRead(s, varsize);
                fRecords[index].Read(s, mgr);
            }
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);
            s.WriteByte(0);

            // Variable length hack
            int count = fDesc.Count;
            if (fDesc.VariableLength) {
                count = fRecords.Count;
                s.WriteInt(count);
            }

            // More hacks...
            List<int> recs = new List<int>();
            recs.Capacity = fRecords.Count;
            for (int i = 0; i < fRecords.Count; i++)
                if (fRecords[i].Used)
                    recs.Add(i);

            int varsize = fDesc.VariableLength ? Int32.MaxValue : count;
            bool all = (recs.Count == fRecords.Count);
            plSDL.VariableLengthWrite(s, varsize, recs.Count);

            for (int i = 0; i < recs.Count; i++) {
                int varID = recs[i];
                if (!all)
                    plSDL.VariableLengthWrite(s, varsize, varID);
                fRecords[varID].Write(s, mgr);
            }
        }
    }
}
