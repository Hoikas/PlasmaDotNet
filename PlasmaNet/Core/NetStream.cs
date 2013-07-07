using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using OpenSSL;

namespace Plasma {
    public class pnSocketStream : Stream {

        #region Properties
        public override bool CanRead {
            get { return true; }
        }

        public override bool CanSeek {
            get { return false; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override void Flush() { }

        public override long Length {
            get { throw new NotSupportedException(); }
        }

        public override long Position {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        #endregion

        RC4 fRead;
        RC4 fWrite;
        Socket fSocket;

        public pnSocketStream(Socket socket, byte[] key) {
            fSocket = socket;
            if (key != null) {
                fRead = new RC4(key);
                fWrite = new RC4(key);
            }
        }

        public override int Read(byte[] buffer, int offset, int count) {
            // Receive data from the socket
            byte[] recv = new byte[count];
            for (int i = 0; i < count; )
                i += fSocket.Receive(recv, i, count - i, SocketFlags.None);

            // Decrypt and do some finalization
            if (fRead != null) {
                byte[] temp = fRead.Transform(recv);
                Buffer.BlockCopy(temp, 0, buffer, offset, count);
            } else
                Buffer.BlockCopy(recv, 0, buffer, offset, count);
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotSupportedException();
        }

        public override void SetLength(long value) {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            if (fWrite != null) {
                byte[] temp = new byte[count];
                Buffer.BlockCopy(buffer, offset, temp, 0, count);
                byte[] toSend = fWrite.Transform(temp);
                fSocket.Send(toSend);
            } else
                fSocket.Send(buffer, offset, count, SocketFlags.None);
        }
    }

    /// <summary>
    /// Plasma Stream that requires the user to manually flush the output buffer
    /// </summary>
    /// <remarks>
    /// This is most useful in network situations because MOUL expects the entire message buffer to be on the wire
    /// when it reads it in. EAP was silly and used memcpy, strdup, and all kinds of silly devilry.
    /// </remarks>
    public class plBufferedStream : hsStream {

        protected MemoryStream fWriteStream;
        private bool fPrependLength = false;

        /// <summary>
        /// Gets or sets if we should prepend an Int32 length to the flushed stream.
        /// </summary>
        public bool PrependLength {
            get { return fPrependLength; }
            set { fPrependLength = value; }
        }

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
            if (fPrependLength) {
                byte[] temp = new byte[buf.Length + 4];

                // Copy the total length, then the actual buffer
                Buffer.BlockCopy(BitConverter.GetBytes(temp.Length), 0, temp, 0, 4);
                Buffer.BlockCopy(buf, 0, temp, 4, buf.Length);
                fBaseStream.Write(temp, 0, temp.Length);
            }
                fBaseStream.Write(buf, 0, buf.Length);
            fWriteStream.SetLength(0);
        }
    }

    /// <summary>
    /// Plasma Network Read/Write Helpers
    /// </summary>
    /// <remarks>
    /// Cyan's MOUL Network coder "eap" is the sole reason this exists--he writes strings in too many different
    /// ways to the network!
    /// </remarks>
    public static partial class pnHelpers {

        /// <summary>
        /// Converts a hex string to a byte array.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string hex) {
            byte[] buf = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
                buf[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return buf;
        }

        /// <summary>
        /// Converts an array of Bytes to a hex String
        /// </summary>
        /// <param name="buf">Array of Bytes to convert</param>
        /// <returns>Resulting hex String</returns>
        public static string GetString(byte[] buf) {
            return BitConverter.ToString(buf).Replace("-", null).ToLower();
        }

        /// <summary>
        /// Reads in a UTF-16 string with an int32 size
        /// </summary>
        /// <param name="s">Stream to read from</param>
        /// <returns>Resulting string</returns>
        public static string ReadString(hsStream s) {
            byte[] data = s.ReadBytes(s.ReadInt());
            return Encoding.Unicode.GetString(data).Replace("\0", null);
        }

        /// <summary>
        /// Reads in a UTF-16 string with an int16 size and a predefined maximum size
        /// </summary>
        /// <param name="s">Stream to read from</param>
        /// <param name="maxSize">Maximum string size</param>
        /// <returns>Resulting string</returns>
        public static string ReadString(hsStream s, int maxSize) {
            int size = (int)s.ReadShort();
            string data = Encoding.Unicode.GetString(s.ReadBytes(size * 2));
            if (data.Length > maxSize)
                return data.Remove(maxSize);
            return data;
        }

        public static Guid ReadUuid(hsStream s) {
            return new Guid(s.ReadBytes(16));
        }

        /// <summary>
        /// Writes a UTF-16 string with an int32 size
        /// </summary>
        /// <param name="s">Stream to write to</param>
        /// <param name="data">String to write to the stream</param>
        public static void WriteString(hsStream s, string data) {
            byte[] str = Encoding.Unicode.GetBytes(data);
            s.WriteInt(str.Length + 2);
            s.WriteBytes(str);
            s.WriteUShort((ushort)0);
        }

        public static void WriteString(hsStream s, string data, int maxSize) {
            if (data == null) {
                s.WriteShort(0);
                return;
            }

            byte[] buf;
            if (data.Length > maxSize)
                buf = Encoding.Unicode.GetBytes(data.Remove(maxSize));
            else
                buf = Encoding.Unicode.GetBytes(data);

            s.WriteShort((short)(buf.Length / 2));
            s.WriteBytes(buf);
        }

        public static void WriteUuid(hsStream s, Guid uuid) {
            s.WriteBytes(uuid.ToByteArray());
        }
    }
}
