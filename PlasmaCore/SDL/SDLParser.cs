using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class plSDLMgr {

        enum ParseState { kStateDesc, kVariable, kVersion, kEnd }

        public void ReadDescriptors(string path) {
            // TODO: EncryptedStream support
            if (Directory.Exists(path)) {
                foreach (string sdl in Directory.EnumerateFiles(path, "*.sdl")) {
                    FileStream fs = new FileStream(Path.Combine(path, sdl), FileMode.Open, FileAccess.Read);
                    ReadDescriptors(fs);
                    fs.Close();
                }
            } else if (File.Exists(path)) {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                hsStream s = new hsStream(fs);
                ReadDescriptors(fs);
                s.Close();
                fs.Close();
            } else
                throw new FileNotFoundException(path);
        }

        public void ReadDescriptors(Stream s) {
            hsTokenStream toks = new hsTokenStream(s);
            while (toks.Available)
                IReadDescriptor(toks);
        }

        private void IReadDescriptor(hsTokenStream toks) {
            plStateDescriptor desc = null;
            string name = null; int version; // Temporary variables
            
            ParseState state = ParseState.kStateDesc;
            while (state != ParseState.kEnd) {
                switch (state) {
                    case ParseState.kStateDesc:
                        string statedesc = toks.NextToken();
                        if (statedesc.ToUpper() != "STATEDESC")
                            throw new plSDLException(String.Format("Invalid token \"{0}\" Expected: \"STATEDESC\"", statedesc));
                        
                        name = toks.NextToken();
                        state = ParseState.kVersion;
                        break;
                    case ParseState.kVersion:
                        string bracket = toks.NextToken();
                        if (bracket != "{")
                            throw new plSDLException(String.Format("Invalid token \"{0}\" Expected: \"{\"", bracket));

                        string vertok = toks.NextToken();
                        if (vertok.ToUpper() != "VERSION")
                            throw new plSDLException(String.Format("Invalid token \"{0}\" Expected: \"VERSION\"", vertok));

                        version = Convert.ToInt32(toks.NextToken());
                        desc = new plStateDescriptor(name, version);
                        state = ParseState.kVariable;
                        break;
                    case ParseState.kVariable:
                        string vartok = toks.NextToken();
                        if (vartok == "}") {
                            state = ParseState.kEnd;
                            break;
                        } else if (vartok.ToUpper() != "VAR")
                            throw new plSDLException(String.Format("Invalid token \"{0}\" Expected: \"VAR\"", vartok));

                        string type = toks.NextToken();
                        string varName = toks.NextToken();

                        plVarDescriptor var = null;
                        if (type.StartsWith("$"))
                            var = new plSDVarDescriptor(varName, type.Substring(1));
                        else
                            var = new plSimpleVarDescriptor(varName, type);

                        // Optional tokens
                        for (string next = toks.Peek(); next.ToUpper() != "VAR" && next != "}"; next = toks.Peek()) {
                            next = toks.NextToken();

                            // Let's combine any broken up statements...
                            if (toks.Peek() == "=") {
                                next += toks.NextToken();
                                next += toks.NextToken();
                            }

                            // Sometimes, silly Cyan ends lines with a semicolon
                            if (next.EndsWith(";"))
                                next = next.Substring(0, next.Length - 1);

                            if (next.ToUpper().StartsWith("DEFAULT=")) {
                                string def = next.Substring(8);
                                if (def.StartsWith("("))
                                    def = def.Substring(1).Substring(0, def.Length - 2);
                                if (var is plSimpleVarDescriptor)
                                    ((plSimpleVarDescriptor)var).Default = def;
                                else
                                    throw new plSDLException("SDVarDescriptors do not support default values", new NotSupportedException());
                            } else if (next.ToUpper().StartsWith("DEFAULTOPTION")) {
                                string defopt = next.Substring(14);
                                if (defopt == "hidden")
                                    // Cyan's code does not handle this case; however, is is
                                    // present in the SDL files. I am assuming someone forgot
                                    // what the rules are XD
                                    var.Internal = true;
                                else if (defopt == "red") { /* CYAN!!! */ } 
                                else if (defopt == "VAULT")
                                    var.AlwaysNew = true;
                            } else if (next.ToUpper().StartsWith("DISPLAYOPTION=")) {
                                string option = next.Substring(14);
                                if (option.ToLower() == "hidden")
                                    var.Internal = true;
                                // FIXME: Cyan caches the display option in a string
                            } else if (next.ToUpper() == "INTERNAL") // Deprecated
                                var.Internal = true;
                            else if (next.ToUpper() == "PHASED")     // Deprecated
                                var.AlwaysNew = true;
                            else
                                throw new plSDLException("Unsupported optional token", new NotSupportedException(next));
                        }

                        desc.Variables.Add(var);
                        break;
                }
            }

            fDescriptors.Add(desc);
        }
    }
}
