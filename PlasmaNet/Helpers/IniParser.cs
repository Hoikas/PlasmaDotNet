using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pnIniParser {

        Dictionary<string, string> fValues = new Dictionary<string, string>();

        public string this[string index] {
            get {
                if (fValues.ContainsKey(index.ToLower()))
                    return fValues[index.ToLower()];
                else
                    return null;
            }
        }

        public pnIniParser(string file) {
            hsTokenStream s = new hsTokenStream(file);
            Populate(s);
        }

        public Guid GetGuid(string index, Guid def=new Guid()) {
            if (fValues.ContainsKey(index.ToLower()))
                return new Guid(fValues[index.ToLower()]);
            else
                return def;
        }

        public int? GetInteger(string index) {
            try {
                if (fValues.ContainsKey(index.ToLower()))
                    return new int?(Convert.ToInt32(fValues[index.ToLower()]));
                else
                    return new int?();
            } catch {
                return new int?();
            }
        }

        public void Populate(hsTokenStream s) {
            while (s.Available) {
                string var = s.NextToken().ToLower();
                string val = s.NextToken();

                if (val != String.Empty && val != null)
                    // Don't throw an exception if someone is stupid and redefines
                    // an option--rather, stick it to them.
                    if (fValues.ContainsKey(var))
                        fValues[var] = val;
                    else
                        fValues.Add(var, val);
            }
        }
    }
}
