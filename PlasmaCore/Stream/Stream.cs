using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {
    public class hsStream {

        protected Stream fBaseStream;
        protected BinaryReader fReader;
        protected BinaryWriter fWriter;
        protected plVersion fVersion = plVersion.MystOnline; // Default to MOUL
        private readonly char[] fMystNerd = new char[] { 'm', 'y', 's', 't', 'n', 'e', 'r', 'd' };

        /// <summary>
        /// Gets or sets the Plasma Version used by this Stream
        /// </summary>
        public plVersion Version {
            get { return fVersion; }
            set { fVersion = value; }
        }

        public Stream BaseStream {
            get { return fBaseStream; }
        }

        protected hsStream() { }

        public hsStream(Stream s) {
            fBaseStream = s;
            if (s.CanRead)
                fReader = new BinaryReader(s);

            if (s.CanWrite)
                fWriter = new BinaryWriter(s);
        }

        public hsStream(string filename) {
            fBaseStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            fReader = new BinaryReader(fBaseStream);
            fWriter = new BinaryWriter(fBaseStream);
        }

        public void Close() {
            fReader.Close();
            fWriter.Close();
            fBaseStream.Close();
        }

        public bool ReadBool() {
            return fReader.ReadBoolean();
        }

        public byte ReadByte() {
            return fReader.ReadByte();
        }

        public byte[] ReadBytes(int count) {
            if (count == 0) return new byte[0];
            return fReader.ReadBytes(count);
        }

        public string ReadCString(int size) {
            byte[] buf = fReader.ReadBytes(size);
            return Encoding.UTF8.GetString(buf);
        }

        public double ReadDouble() {
            return fReader.ReadDouble();
        }

        public float ReadFloat() {
            return fReader.ReadSingle();
        }

        public string ReadSafeString() {
            short info = fReader.ReadInt16();
            if (fVersion.IsPlasma20) {
                if ((info & 0xF000) == 0) fReader.ReadInt16(); //Garbage

                int size = (info & 0x0FFF);
                if (size > 0) {
                    byte[] data = ReadBytes(size);
                    if ((data[0] & 0x80) != 0)
                        for (int i = 0; i < size; i++)
                            data[i] = (byte)(~data[i]);
                    return Encoding.UTF8.GetString(data);
                } else return String.Empty;
            } else if (fVersion.IsPlasma21) {
                byte[] buf = fReader.ReadBytes(info);
                for (int i = 0; i < info; i++)
                    buf[i] ^= (byte)fMystNerd[i % 8];
                return Encoding.UTF8.GetString(buf);
            } else return String.Empty;
        }

        public string ReadSafeWString() {
            int size = (int)(fReader.ReadInt16() & 0x0FFF);
            byte[] data = ReadBytes(size * 2);
            if ((data[0] & 0x80) != 0)
                for (int i = 0; i < data.Length; i++)
                    data[i] = (byte)(~data[i]);
            return Encoding.Unicode.GetString(data);
        }

        public string ReadStdString() {
            short len = fReader.ReadInt16();
            byte[] data = fReader.ReadBytes(len);
            return Encoding.UTF8.GetString(data);
        }

        public short ReadShort() {
            return fReader.ReadInt16();
        }

        public int ReadInt() {
            return fReader.ReadInt32();
        }

        public ushort ReadUShort() {
            return fReader.ReadUInt16();
        }

        public uint ReadUInt() {
            return fReader.ReadUInt32();
        }

        public ulong ReadULong() {
            return fReader.ReadUInt64();
        }

        public void Rewind() {
            fBaseStream.Position = 0;
        }

        public void Rewind(int count) {
            fBaseStream.Position -= (long)count;
        }

        public void Seek(int pos) {
            fBaseStream.Position = (long)pos;
        }

        public void WriteBool(bool data) {
            fWriter.Write(data);
        }

        public void WriteByte(byte data) {
            fWriter.Write(data);
        }

        public void WriteBytes(byte[] data) {
            if (data == null) return;
            if (data.Length == 0) return;
            fWriter.Write(data);
        }

        public void WriteDouble(double data) {
            fWriter.Write(data);
        }

        public void WriteFloat(float data) {
            fWriter.Write(data);
        }

        public void WriteInt(int data) {
            fWriter.Write(data);
        }

        public void WriteSafeString(string data) {
            if (data == null)
                data = String.Empty;
            
            byte[] buf = Encoding.UTF8.GetBytes(data);
            short info = (short)(buf.Length & 0x0FFF);
            fWriter.Write((short)(info | 0xF000));

            byte[] str = new byte[info];
            for (int i = 0; i < info; i++)
                str[i] = (byte)(~buf[i]);
            WriteBytes(str);
        }

        public void WriteSafeWString(string data) {
            if (data == null)
                data = String.Empty;

            byte[] buf = Encoding.Unicode.GetBytes(data);
            WriteShort((short)((buf.Length / 2) | 0xF000));
            for (int i = 0; i < (buf.Length / 2); i++) {
                short wchar = BitConverter.ToInt16(buf, i * 2);
                WriteShort((short)(~wchar));
            }

            WriteShort(0);
        }

        public void WriteStdString(string data) {
            if (data == null)
                fWriter.Write((short)0);
            else {
                byte[] buf = Encoding.UTF8.GetBytes(data);
                fWriter.Write((short)buf.Length);
                if (buf.Length != 0) 
                    fWriter.Write(buf);
            }
        }

        public void WriteShort(short data) {
            fWriter.Write(data);
        }

        public void WriteUInt(uint data) {
            fWriter.Write(data);
        }

        public void WriteULong(ulong data) {
            fWriter.Write(data);
        }

        public void WriteUShort(ushort data) {
            fWriter.Write(data);
        }
    }

    /// <summary>
    /// Plasma Stream that requires the user to manually flush the output buffer
    /// </summary>
    /// <remarks>
    /// This is most useful in network situations because MOUL expects the entire message buffer to be on the wire
    /// when it reads it in. EAP was silly and used memcpy, strdup, and all kinds of silyl devilry.
    /// </remarks>
    public class plBufferedStream : hsStream {

        protected MemoryStream fWriteStream;

        public plBufferedStream(Stream baseStream) {
            fBaseStream = baseStream;
            fWriteStream = new MemoryStream();

            if (baseStream.CanRead)
                fReader = new BinaryReader(baseStream);
            fWriter = new BinaryWriter(fWriteStream);
        }

        public new void Close() {
            Flush();
            fWriteStream.Close();
            base.Close();
        }

        public void Flush() {
            if (fWriteStream.Length == 0) return;

            byte[] buf = fWriteStream.ToArray();
            fBaseStream.Write(buf, 0, buf.Length);
            fWriteStream.SetLength(0);
        }
    }
}
