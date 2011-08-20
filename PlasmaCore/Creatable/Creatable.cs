using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public abstract class plCreatable {

        public virtual void Read(hsStream s, plResManager mgr) { }
        public virtual void Write(hsStream s, plResManager mgr) { }
    }
}
