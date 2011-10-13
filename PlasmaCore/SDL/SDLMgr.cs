using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {
    internal static class plSDL {

        [Flags]
        internal enum ContentsFlags {
            kHasUoid = 0x1,
            kHasNotificationInfo = 0x2,
            kHasTimeStamp = 0x4,
            kSameAsDefault = 0x8,
            kHasDirtyFlag = 0x10,
            kWantTimeStamp = 0x20,
            kAddedVarLengthIO = 0x8000,     // <---- more Cyan hacks
        };

        internal static int VariableLengthRead(hsStream s, int size) {
            if (size < 0x100)
                return (int)s.ReadByte();
            else if (size < 0x10000)
                return (int)s.ReadShort();
            else
                return s.ReadInt();
        }

        internal static void VariableLengthWrite(hsStream s, int size, int value) {
            if (size < 0x100)
                s.WriteByte((byte)value);
            else if (size < 0x10000)
                s.WriteShort((short)value);
            else
                s.WriteInt(value);
        }
    }

    public partial class plSDLMgr {

        List<plStateDescriptor> fDescriptors = new List<plStateDescriptor>();

        public List<plStateDescriptor> Descriptors {
            get { return fDescriptors; }
        }

        public byte[] DumpStateRecord(plStateDataRecord record, hsResMgr mgr) {
            MemoryStream ms = new MemoryStream();
            hsStream s = new hsStream(ms);
            s.Version = mgr.Version;

            byte[] buf = null;
            try {
                record.WriteStreamHeader(s, mgr);
                record.Write(s, mgr);
            } catch (Exception e) {
                throw new plSDLException("Failed to dump StateDataRecord", e);
            } finally {
                buf = ms.ToArray();
                s.Close();
                ms.Close();
            }

            return buf;
        }

        public plStateDescriptor FindDescriptor(plSDVarDescriptor sd) {
            foreach (plStateDescriptor desc in fDescriptors)
                if (desc.Name == sd.Descriptor &&
                    desc.Version == sd.Version)
                    return desc;
            return null;
        }

        public plStateDataRecord ParseStateRecord(byte[] record, hsResMgr mgr) {
            MemoryStream ms = new MemoryStream(record);
            hsStream s = new hsStream(ms);
            s.Version = mgr.Version;

            plStateDataRecord result = new plStateDataRecord(this);
            try {
                result.ReadStreamHeader(s, mgr);
                result.Read(s, mgr);
            } catch (Exception e) {
                throw new plSDLException("Failed to parse StateDataRecord", e);
            } finally {
                s.Close();
                ms.Close();
            }

            return result;
        }

        public void Read(hsStream s) {
            short count = s.ReadShort();
            fDescriptors.Capacity = (int)count; // Optimization

            try {
                for (short i = 0; i < count; i++) {
                    plStateDescriptor desc = new plStateDescriptor();
                    desc.Read(s);
                    fDescriptors.Add(desc);
                }
            } catch (Exception e) {
                throw new plSDLException("Failed to read State Descriptors", e);
            }
        }
    }
}
