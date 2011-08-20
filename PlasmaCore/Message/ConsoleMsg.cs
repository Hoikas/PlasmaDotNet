using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class plConsoleMsg : plMessage {

        public enum Cmd {
            ExecuteFile, AddLine, ExecuteLine,
        }

        Cmd fCommand;
        string fString;

        public Cmd Type {
            get { return fCommand; }
            set { fCommand = value; }
        }

        public string Value {
            get { return fString; }
            set { fString = value; }
        }

        public plConsoleMsg() {
            fBCastFlags |= plBCastFlags.kBCastByExactType;
        }

        public override void Read(hsStream s, plResManager mgr) {
            base.Read(s, mgr);

            fCommand = (Cmd)s.ReadInt();
            fString = s.ReadStdString();
        }

        public override void Write(hsStream s, plResManager mgr) {
            base.Write(s, mgr);

            s.WriteInt((int)fCommand);
            s.WriteStdString(fString);
        }
    }
}
