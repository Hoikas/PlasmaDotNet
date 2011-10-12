using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Plasma {

    public class hsException : Exception {
        public hsException() { }
        public hsException(string message) : base(message) { }
        public hsException(string message, Exception inner) : base(message, inner) { }
    }

    public class plVersionException : hsException {
        public plVersionException() { }
        public plVersionException(uint version) : base("Invalid Plasma Version") {
            base.Data.Add("plVersionUInt32", version);
        }

        public plVersionException(plVersion version)
            : base("Invalid Plasma Version") {
            base.Data.Add("plVersion", version);
        }
    }

    public class plFactoryException : hsException {
        public plFactoryException() { }
        public plFactoryException(string message) : base(message) { }
    }

    public class plSDLException : hsException {
        public plSDLException() { }
        public plSDLException(string message) : base(message) { }
        public plSDLException(string message, Exception inner) : base(message, inner) { }
    }

    public class plZlibException : hsException {
        public plZlibException() { }
        public plZlibException(string message) : base(message) { }
        public plZlibException(string message, Exception inner) : base(message, inner) { }
    }
}
