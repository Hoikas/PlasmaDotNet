using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Plasma {
    public static class hsColorRGBA {

        public static Color ReadRGB(hsStream s) {
            float r = s.ReadFloat();
            float g = s.ReadFloat();
            float b = s.ReadFloat();

            return Color.FromArgb(Convert.ToInt32(r * 255.0f),
                Convert.ToInt32(g * 255.0f),
                Convert.ToInt32(b * 255.0f));
        }

        public static Color ReadRGB8(hsStream s) {
            return Color.FromArgb((int)s.ReadByte(), (int)s.ReadByte(), (int)s.ReadByte());
        }

        public static Color ReadRGBA(hsStream s) {
            float r = s.ReadFloat();
            float g = s.ReadFloat();
            float b = s.ReadFloat();
            float a = s.ReadFloat();

            return Color.FromArgb(Convert.ToInt32(a * 255), 
                Convert.ToInt32(r * 255.0f),
                Convert.ToInt32(g * 255.0f), 
                Convert.ToInt32(b * 255.0f));
        }

        public static Color ReadRGBA8(hsStream s) {
            int r = (int)s.ReadByte();
            int g = (int)s.ReadByte();
            int b = (int)s.ReadByte();
            int a = (int)s.ReadByte();

            return Color.FromArgb(a, r, g, b);
        }

        public static void WriteRGB(hsStream s, Color c) {
            s.WriteFloat(Convert.ToSingle(c.R) / 255.0f);
            s.WriteFloat(Convert.ToSingle(c.G) / 255.0f);
            s.WriteFloat(Convert.ToSingle(c.B) / 255.0f);
        }

        public static void WriteRGB8(hsStream s, Color c) {
            s.WriteByte(c.R);
            s.WriteByte(c.G);
            s.WriteByte(c.B);
        }

        public static void WriteRGBA(hsStream s, Color c) {
            s.WriteFloat(Convert.ToSingle(c.R) / 255.0f);
            s.WriteFloat(Convert.ToSingle(c.G) / 255.0f);
            s.WriteFloat(Convert.ToSingle(c.B) / 255.0f);
            s.WriteFloat(Convert.ToSingle(c.A) / 255.0f);
        }

        public static void WriteRGBA8(hsStream s, Color c) {
            s.WriteByte(c.R);
            s.WriteByte(c.G);
            s.WriteByte(c.B);
            s.WriteByte(c.A);
        }
    }
}
