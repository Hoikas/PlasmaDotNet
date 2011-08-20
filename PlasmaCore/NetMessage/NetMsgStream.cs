using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Plasma {
    public abstract class plNetMsgStream : plNetMessage {

        protected plNetMsgStreamHelper fHelper = new plNetMsgStreamHelper();

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);
            fHelper.Read(s, mgr);
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);
            fHelper.Write(s, mgr);
        }
    }

    public class plNetMsgStreamHelper : plCreatable {
        enum Compression {
            kNone,
            kFailed,
            kZlib,
            kDont,
        }

        byte[] fBuffer;
        plVersion fVersion;

        public hsStream Stream {
            get {
                if (fBuffer == null)
                    return null;

                MemoryStream ms = new MemoryStream(fBuffer);
                hsStream s = new hsStream(ms);
                s.Version = fVersion;
                return s;
            }

            set {
                if (value == null)
                    return;

                int oldpos = (int)value.BaseStream.Position;
                value.Seek(0);
                fBuffer = value.ReadBytes((int)value.BaseStream.Length);
                value.Seek(oldpos);
                fVersion = value.Version;
            }
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            //Cache it.
            fVersion = mgr.Version;

            //Cyan stores these values, but we're just going to
            //    save the stream and have fun with it...
            fBuffer = new byte[s.ReadInt()];
            Compression type = (Compression)s.ReadByte();
            uint len = s.ReadUInt();

            if (type == Compression.kZlib) {
                short streamType = s.ReadShort();
                byte[] buf = s.ReadBytes((int)len - 2);

                //Create a zlib-compatible inflator
                //Note: incoming has no zlib header/footer
                //      System.IO.Compression sucks.
                Inflater zlib = new Inflater(true);
                zlib.Inflate(buf);

                Buffer.BlockCopy(BitConverter.GetBytes(streamType), 0, fBuffer, 0, 2);
                Buffer.BlockCopy(buf, 0, fBuffer, 2, buf.Length);
            } else
                fBuffer = s.ReadBytes((int)len);
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            //We're not going to compress the stream...
            //Hope this is okay!
            s.WriteInt(fBuffer.Length);
            s.WriteByte((byte)Compression.kNone);
            s.WriteInt(fBuffer.Length);
            s.WriteBytes(fBuffer);
        }
    }
}
