using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plStateDataRecord : plCreatable, ICloneable,
        IEnumerable<plStateVariable>, IEnumerable<plSimpleStateVariable>, IEnumerable<plSDStateVariable> {

        private const byte kIoVersion = 6;

        plSDLMgr fMgr;
        plStateDescriptor fDesc;
        plKey fUoid;
        List<plSimpleStateVariable> fSimpleVars = new List<plSimpleStateVariable>();
        List<plSDStateVariable> fSDVars = new List<plSDStateVariable>();

        public plStateVariable this[string key] {
            get {
                foreach (plSimpleStateVariable v in fSimpleVars)
                    if (v.Descriptor.Name == key)
                        return v;
                foreach (plSDStateVariable v in fSDVars)
                    if (v.Descriptor.Name == key)
                        return v;
                throw new ArgumentException("key");
            }
        }

        public plStateDescriptor Descriptor {
            get { return fDesc; }
            set {
                fSimpleVars.Clear();
                fSDVars.Clear();

                if (value == null)
                    return;
                fDesc = value;

                for (int i = 0; i < value.Variables.Count; i++) {
                    plVarDescriptor desc = value.Variables[i];
                    if (desc is plSimpleVarDescriptor)
                        fSimpleVars.Add(new plSimpleStateVariable((plSimpleVarDescriptor)desc));
                    else
                        fSDVars.Add(new plSDStateVariable(fMgr, (plSDVarDescriptor)desc));
                }
            }
        }

        public bool Dirty {
            get {
                foreach (plSimpleStateVariable v in fSimpleVars)
                    if (v.Dirty) return true;
                foreach (plSDStateVariable v in fSDVars)
                    if (v.Dirty) return true;
                return false;
            }
        }

        public bool Used {
            get {
                foreach (plSimpleStateVariable v in fSimpleVars)
                    if (v.Used) return true;
                foreach (plSDStateVariable v in fSDVars)
                    if (v.Used) return true;
                return false;
            }
        }

        public plStateDataRecord(plSDLMgr mgr) {
            fMgr = mgr;
        }

        /// <summary>
        /// Creates a copy of this state record with all variables at the default state
        /// </summary>
        /// <returns>Newly created state record</returns>
        public object Clone() {
            plStateDataRecord rec = new plStateDataRecord(fMgr);
            rec.Descriptor = fDesc;
            return rec;
        }

        #region IEnumerable
        IEnumerator<plStateVariable> IEnumerable<plStateVariable>.GetEnumerator() {
            // Return the list in the same order as the SDL file :)
            List<plStateVariable> list = new List<plStateVariable>();
            foreach (plVarDescriptor desc in fDesc.Variables) {
                if (desc is plSDVarDescriptor) {
                    foreach (plSDStateVariable sd in fSDVars)
                        if (sd.Descriptor == desc)
                            list.Add(sd);
                } else {
                    foreach (plSimpleStateVariable sv in fSimpleVars)
                        if (sv.Descriptor == desc)
                            list.Add(sv);
                }
            }

            return list.GetEnumerator();
        }

        IEnumerator<plSDStateVariable> IEnumerable<plSDStateVariable>.GetEnumerator() {
            return fSDVars.GetEnumerator();
        }

        IEnumerator<plSimpleStateVariable> IEnumerable<plSimpleStateVariable>.GetEnumerator() {
            return fSimpleVars.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<plStateVariable>)this).GetEnumerator();
        }
        #endregion

        internal void ReadStreamHeader(hsStream s, plResManager mgr) {
            plSDL.ContentsFlags savFlags = (plSDL.ContentsFlags)s.ReadShort();
            if (!savFlags.HasFlag(plSDL.ContentsFlags.kAddedVarLengthIO))
                throw new NotSupportedException("Bad StateDataRecord IO Flag");
            
            string name = s.ReadSafeString();
            int version = (int)s.ReadUShort();
            foreach (plStateDescriptor desc in fMgr.Descriptors)
                if (desc.Name == name &&
                    desc.Version == version)
                    Descriptor = desc;

            if (fDesc == null)
                throw new NotSupportedException("SDL Descriptor not found");

            if (savFlags.HasFlag(plSDL.ContentsFlags.kHasUoid))
                fUoid = mgr.ReadUoid(s);
        }

        public override void Read(hsStream s, plResManager mgr) {
            s.ReadUShort(); // fFlags--but they're rubbish
            if (s.ReadByte() != kIoVersion)
                throw new NotSupportedException("Bad StateDataRecord IO Version");

            // Simple state variables
            int count = plSDL.VariableLengthRead(s, fDesc.Variables.Count);
            bool all = (count == fSimpleVars.Count); // Cyanic hack for smaller blobs
            for (int i = 0; i < count; i++) {
                int idx = i;
                if (!all)
                    idx = plSDL.VariableLengthRead(s, fDesc.Variables.Count);
                fSimpleVars[idx].Read(s, mgr);
            }

            // SD state variables
            count = plSDL.VariableLengthRead(s, fDesc.Variables.Count);
            all = (count == fSDVars.Count); // Cyanic hack for smaller blobs
            for (int i = 0; i < count; i++) {
                int idx = i;
                if (!all)
                    idx = plSDL.VariableLengthRead(s, fDesc.Variables.Count);
                fSDVars[idx].Read(s, mgr);
            }
        }

        internal void WriteStreamHeader(hsStream s, plResManager mgr) {
            plSDL.ContentsFlags cf = plSDL.ContentsFlags.kAddedVarLengthIO;
            if (fUoid != null)
                cf |= plSDL.ContentsFlags.kHasUoid;
            s.WriteShort((short)cf);
            s.WriteSafeString(fDesc.Name);
            s.WriteUShort((ushort)fDesc.Version);
            if (fUoid != null)
                mgr.WriteUoid(s, fUoid);
        }

        public override void Write(hsStream s, plResManager mgr) {
            s.WriteUShort(0); // Flags (Rubbish)
            s.WriteByte(kIoVersion);

            // Collect the variables we need to write...
            List<int> simple = new List<int>();
            simple.Capacity = fSimpleVars.Count;
            for (int i = 0; i < fSimpleVars.Count; i++)
                if (fSimpleVars[i].Used)
                    simple.Add(i);

            List<int> sd = new List<int>();
            sd.Capacity = fSDVars.Count;
            for (int i = 0; i < fSDVars.Count; i++)
                if (fSDVars[i].Used)
                    sd.Add(i);

            // Write out the collected Simple Variables
            bool all = (simple.Count == fSimpleVars.Count);
            plSDL.VariableLengthWrite(s, fDesc.Variables.Count, simple.Count);
            for (int i = 0; i < simple.Count; i++) {
                int varID = simple[i];
                if (!all)
                    plSDL.VariableLengthWrite(s, fDesc.Variables.Count, varID);
                fSimpleVars[varID].Write(s, mgr);
            }

            // Write out all the collected SD Variables
            all = (sd.Count == fSDVars.Count);
            plSDL.VariableLengthWrite(s, fDesc.Variables.Count, sd.Count);
            for (int i = 0; i < sd.Count; i++) {
                int varID = sd[i];
                if (!all)
                    plSDL.VariableLengthWrite(s, fDesc.Variables.Count, varID);
                fSDVars[varID].Write(s, mgr);
            }
        }
    }
}
