using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using OpenSSL;

namespace Plasma {
    public class pnRC4SocketStream : Stream {

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

        public pnRC4SocketStream(Socket socket, byte[] key) {
            fRead = new RC4(key);
            fWrite = new RC4(key);
            fSocket = socket;
        }

        public override int Read(byte[] buffer, int offset, int count) {
            // Receive data from the socket
            byte[] recv = new byte[count];
            if (fSocket.Receive(recv) != count)
                throw new EndOfStreamException("Socket read returned less data than requested");
            
            // Decrypt and do some finalization
            byte[] temp = fRead.Transform(recv);
            Buffer.BlockCopy(temp, 0, buffer, offset, count);
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotSupportedException();
        }

        public override void SetLength(long value) {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            byte[] temp = new byte[count];
            Buffer.BlockCopy(buffer, offset, temp, 0, count);
            byte[] toSend = fWrite.Transform(temp);
            fSocket.Send(toSend);
        }
    }

    /// <summary>
    /// Plasma Network Read/Write Helpers
    /// </summary>
    /// <remarks>
    /// Cyan's MOUL Network coder "eap" is the sole reason this exists--he writes strings in too many different
    /// ways to the network!
    /// </remarks>
    public static class pnHelpers {

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
