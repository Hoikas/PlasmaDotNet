using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Color = System.Drawing.Color;

namespace Plasma {
    public class plStateDescriptor {

        private const byte kIoVersion = 1;

        string fName;
        int fVersion = -1;
        List<plVarDescriptor> fVariables = new List<plVarDescriptor>();

        /// <summary>
        /// Gets or sets an variable descriptor
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public plVarDescriptor this[int index] {
            get { return fVariables[index]; }
        }

        public string Name {
            get { return fName; }
        }

        public List<plVarDescriptor> Variables {
            get { return fVariables; }
        }

        public int Version {
            get { return fVersion; }
        }

        public plStateDescriptor() { }

        public plStateDescriptor(string name, int version) {
            fName = name;
            fVersion = version;
        }

        public void Read(hsStream s) {
            if (s.ReadByte() != kIoVersion)
                throw new NotSupportedException("Bad StateDescriptor IO Version");

            fName = s.ReadSafeString();
            fVersion = (int)s.ReadShort();

            short count = s.ReadShort();
            fVariables.Capacity = count; // Optimization
            for (short i = 0; i < count; i++) {
                plVarDescriptor var = null;

                bool isSD = s.ReadBool();
                if (isSD)
                    var = new plSDVarDescriptor();
                else
                    var = new plSimpleVarDescriptor();
                var.Read(s);
                fVariables.Add(var);
            }
        }

        public void Write(hsStream s) {
            throw new NotImplementedException();
        }
    }

    public enum plAtomicType {
        kNone = -1,

        // atomic types
        kInt,
        kFloat,
        kBool,
        kString32,
        kKey,               // plKey - basically a uoid
        kStateDescriptor,   // this var refers to another state descriptor
        kCreatable,         // plCreatable - basically a classIdx and a read/write buffer
        kDouble,
        kTime,              // double which is automatically converted to server clock and back, use for animation times
        kByte,
        kShort,
        kAgeTimeOfDay,      // float which is automatically set to the current age time of day (0-1)

        // the following are a vector of floats
        kVector3 = 50,  // atomicCount=3
        kPoint3,        // atomicCount=3
        kRGB,           // atomicCount=3
        kRGBA,          // atomicCount=4
        kQuaternion,    // atomicCount=4
        kRGB8,          // atomicCount=3
        kRGBA8,         // atomicCount=4
    }

    public abstract class plVarDescriptor {

        [Flags]
        enum Flags {
            kInternal = 0x1,
            kAlwaysNew = 0x2,
            kVariableLength = 0x4
        }

        private const byte kIoVersion = 3;

        Flags fFlags;
        string fName;
        int fCount; // As specified by the SDL file
        protected plAtomicType fType = plAtomicType.kNone; // As specified by the SDL file
        string fDisplayOptions;

        public bool AlwaysNew {
            get { return fFlags.HasFlag(Flags.kAlwaysNew); }
            set {
                if (value)
                    fFlags |= Flags.kAlwaysNew;
                else
                    fFlags &= ~Flags.kAlwaysNew;
            }
        }

        public int Count {
            get { return fCount; }
        }

        public abstract string Default {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if this value is internal (whether or not it should be listed in the Vault Manager)
        /// </summary>
        public bool Internal {
            get { return fFlags.HasFlag(Flags.kInternal); }
            set {
                if (value)
                    fFlags |= Flags.kInternal;
                else
                    fFlags &= ~Flags.kInternal;
            }
        }

        public string Name {
            get { return fName; }
        }

        public plAtomicType Type {
            get { return fType; }
        }

        public bool VariableLength {
            get { return fFlags.HasFlag(Flags.kVariableLength); }
            set {
                if (value)
                    fFlags |= Flags.kVariableLength;
                else
                    fFlags &= ~Flags.kVariableLength;
            }
        }

        protected plVarDescriptor() { }
        protected plVarDescriptor(string name) {
            if (name.EndsWith("[]")) {
                VariableLength = true;
                fName = name.Replace("[]", null);
            } else {
                string[] hack = name.Split(new char[] { '[' });
                hack[1] = hack[1].Replace("]", null);
                fName = hack[0];
                fCount = Convert.ToInt32(hack[1]);
            }
        }

        public virtual void Read(hsStream s) {
            if (s.ReadByte() != kIoVersion)
                throw new NotSupportedException("Bad VarDescriptor IO Version");

            fName = s.ReadSafeString();
            string displayOptions = s.ReadStdString(); // TODO
            fCount = s.ReadInt();
            fType = (plAtomicType)s.ReadByte();
            Default = s.ReadSafeString();
            fFlags = (Flags)s.ReadInt();
        }

        public virtual void Write(hsStream s) {
            throw new NotImplementedException();
        }
    }

    public class plSimpleVarDescriptor : plVarDescriptor {

        plAtomicType fAtomicType;
        int fAtomicCount = 1;
        object fDefaultValue;

        /// <summary>
        /// Gets or sets an string representation of the default value, formatted
        /// for an SDL file
        /// </summary>
        public override string Default {
            get { throw new NotImplementedException(); }
            set {
                string[] split = null;
                if (fAtomicCount > 1)
                    split = value.Split(new char[] { ',' });

                switch (fType) {
                    case plAtomicType.kBool:
                        if (value == "1") // Because Convert.ToBoolean sucks
                            fDefaultValue = true;
                        else if (value == "0")
                            fDefaultValue = false;
                        else
                            fDefaultValue = Convert.ToBoolean(value);
                        break;
                    case plAtomicType.kByte:
                        fDefaultValue = Convert.ToByte(value);
                        break;
                    case plAtomicType.kDouble:
                        fDefaultValue = Convert.ToDouble(value);
                        break;
                    case plAtomicType.kFloat:
                        fDefaultValue = Convert.ToSingle(value);
                        break;
                    case plAtomicType.kInt:
                        fDefaultValue = Convert.ToInt32(value);
                        break;
                    case plAtomicType.kKey:
                        if (value.ToLower() != "nil")
                            throw new NotSupportedException("PLKEY Default Value");
                        break;
                    case plAtomicType.kPoint3:
                        fDefaultValue = new hsPoint3(Convert.ToSingle(split[0]),
                            Convert.ToSingle(split[1]), Convert.ToSingle(split[2]));
                        break;
                    case plAtomicType.kQuaternion:
                        fDefaultValue = new hsQuat(Convert.ToSingle(split[0]),
                            Convert.ToSingle(split[1]), Convert.ToSingle(split[2]), Convert.ToSingle(split[3]));
                        break;
                    case plAtomicType.kRGB:
                        fDefaultValue = Color.FromArgb((int)(Convert.ToSingle(split[0]) * 255.0f),
                            (int)(Convert.ToSingle(split[1]) * 255.0f),
                            (int)(Convert.ToSingle(split[2]) * 255.0f));
                        break;
                    case plAtomicType.kRGB8:
                        fDefaultValue = Color.FromArgb(Convert.ToInt32(split[0]), 
                            Convert.ToInt32(split[1]),
                            Convert.ToInt32(split[2]));
                        break;
                    case plAtomicType.kRGBA:
                        // C# likes ARGB, but Cyan uses RGBA ;)
                        fDefaultValue = Color.FromArgb((int)(Convert.ToSingle(split[3]) * 255.0f),
                            (int)(Convert.ToSingle(split[0]) * 255.0f),
                            (int)(Convert.ToSingle(split[1]) * 255.0f),
                            (int)(Convert.ToSingle(split[2]) * 255.0f));
                        break;
                    case plAtomicType.kRGBA8:
                        // C# likes ARGB, but Cyan uses RGBA ;)
                        fDefaultValue = Color.FromArgb(Convert.ToInt32(split[3]),
                            Convert.ToInt32(split[0]), 
                            Convert.ToInt32(split[1]), 
                            Convert.ToInt32(split[2]));
                        break;
                    case plAtomicType.kShort:
                        fDefaultValue = Convert.ToInt16(value);
                        break;
                    case plAtomicType.kString32:
                        fDefaultValue = value;
                        break;
                    case plAtomicType.kTime:
                        // I *think* this is correct
                        fDefaultValue = plUnifiedTime.Epoch.AddSeconds(Convert.ToDouble(value));
                        break;
                    case plAtomicType.kVector3:
                        fDefaultValue = new hsVector3(Convert.ToSingle(split[0]),
                            Convert.ToSingle(split[1]), Convert.ToSingle(split[2]));
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        /// <summary>
        /// Gets the translated default value for this state variable
        /// </summary>
        public object DefaultValue {
            get { return fDefaultValue; }
        }

        internal plSimpleVarDescriptor() { }

        internal plSimpleVarDescriptor(string name, string type) : base(name) {
            string itype = type.ToUpper();
            if (itype == "VECTOR3") {
                fAtomicCount = 3;
                fAtomicType = plAtomicType.kFloat;
                fType = plAtomicType.kVector3;
            } else if (itype == "POINT3") {
                fAtomicCount = 3;
                fAtomicType = plAtomicType.kFloat;
                fType = plAtomicType.kPoint3;
            } else if (itype == "RGB") {
                fAtomicCount = 3;
                fAtomicType = plAtomicType.kFloat;
                fType = plAtomicType.kRGB;
            } else if (itype == "RGBA") {
                fAtomicCount = 4;
                fAtomicType = plAtomicType.kFloat;
                fType = plAtomicType.kRGBA;
            } else if (itype == "RGB8") {
                fAtomicCount = 3;
                fAtomicType = plAtomicType.kByte;
                fType = plAtomicType.kRGB8;
            } else if (itype == "RGBA8") {
                fAtomicCount = 4;
                fAtomicType = plAtomicType.kByte;
                fType = plAtomicType.kRGBA8;
            } else if (itype == "QUAT" || itype == "QUATERNION") {
                fAtomicCount = 4;
                fAtomicType = plAtomicType.kFloat;
                fType = plAtomicType.kQuaternion;
            } else if (itype == "INT")
                fAtomicType = plAtomicType.kInt;
            else if (itype == "SHORT")
                fAtomicType = plAtomicType.kShort;
            else if (itype == "BYTE")
                fAtomicType = plAtomicType.kByte;
            else if (itype == "FLOAT")
                fAtomicType = plAtomicType.kFloat;
            else if (itype == "DOUBLE")
                fAtomicType = plAtomicType.kDouble;
            else if (itype == "TIME")
                fAtomicType = plAtomicType.kTime;
            else if (itype == "AGETIMEOFDAY")
                fAtomicType = plAtomicType.kAgeTimeOfDay;
            else if (itype == "BOOL")
                fAtomicType = plAtomicType.kBool;
            else if (itype == "STRING32")
                fAtomicType = plAtomicType.kString32;
            else if (itype == "PLKEY")
                fAtomicType = plAtomicType.kKey;
            else if (itype == "MESSAGE" || itype == "CREATABLE")
                fAtomicType = plAtomicType.kCreatable;
            else
                throw new plSDLException("Invalid SimpleVarDescriptor Type: " + type);

            if (fType == plAtomicType.kNone)
                fType = fAtomicType;
        }

        public override void Read(hsStream s) {
            base.Read(s);

            s.WriteShort((short)fAtomicCount);
            s.WriteByte((byte)fAtomicType);
        }
    }

    public class plSDVarDescriptor : plVarDescriptor {

        string fDescName;
        int fVersion = - 1;

        public override string Default {
            get { return null; }
            set { /* Purposefully left blank */ }
        }

        public string Descriptor {
            get { return fDescName; }
        }

        public int Version {
            get { return fVersion; }
            set { fVersion = value; }
        }

        internal plSDVarDescriptor() { }

        internal plSDVarDescriptor(string varName, string descName) : base(varName) {
            fDescName = descName;
            fType = plAtomicType.kStateDescriptor;
        }

        public override void Read(hsStream s) {
            base.Read(s);

            fDescName = s.ReadSafeString();
            fVersion = (int)s.ReadShort();
        }
    }
}
