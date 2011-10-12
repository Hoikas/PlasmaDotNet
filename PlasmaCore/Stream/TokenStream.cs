using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Plasma {
    public class hsTokenStream {

        Queue<string> fTokens = new Queue<string>();

        public bool Available {
            get { return (fTokens.Count != 0); }
        }

        public int Tokens {
            get { return fTokens.Count; }
        }

        string[] fBlockDelimers = new string[] { "'", "\"" };
        char[] fTokenDelimers = new char[] { ' ', '\t' };
        string[] fLineComments = new string[] { "//", "#", ";" };
        string[] fBlockComment = new string[] { "/*", "*/" };

        public hsTokenStream(string filename) {
            StreamReader r = new StreamReader(filename);
            IReadTokens(r);
            r.Close();
        }

        public hsTokenStream(Stream parent) {
            StreamReader r = new StreamReader(parent);
            IReadTokens(r);
            r.Close();
        }

        private void IReadTokens(StreamReader r) {
            bool comment_block = false;
            int in_block = -1; // -1 for no, 0 - Int32.MaxValue = compare with fBlockDelimers
            string token_block = String.Empty;

            while (!r.EndOfStream) {
                // Split the current line by tokens
                string line = r.ReadLine();
                string[] tok = line.Split(fTokenDelimers, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < tok.Length; i++) {
                    // First, check to see if this is a comment
                    bool line_comment = false;
                    foreach (string comment in fLineComments) {
                        if (tok[i].StartsWith(comment)) {
                            line_comment = true;
                            break;
                        }
                    }

                    // If it is a line comment, break the line parse and go to the next line
                    if (line_comment)
                        break;

                    // Now, if we are in a comment block, let's see if this is an ending
                    // If not, make sure we aren't beginning a comment block
                    //        If not, save the token :)
                    if (comment_block) {
                        if (tok[i] == fBlockComment[1])
                            comment_block = false; // Yayayay! We can save the ***next*** token :)
                    } else if (tok[i].StartsWith(fBlockComment[0]))
                        comment_block = true; // Start ignoring :(
                    else if (in_block != -1) {
                        token_block += " " + tok[i].Remove(tok[i].Length - 1);

                        // Check to see if this is the end of a quote block
                        // Be sure to filter out escaped stuff too...
                        if (tok[i].EndsWith(fBlockDelimers[in_block]) &&
                            !tok[i].EndsWith("\\" + fBlockDelimers[in_block])) {

                            fTokens.Enqueue(token_block);
                            token_block = String.Empty;
                            in_block = -1;
                        }
                    } else {
                        // Check to see if this is the start of a quote block
                        // Be sure to filter out escaped stuff too...
                        // Futhermore, make sure we don't end with a quote.
                        for (int j = 0; j < fBlockDelimers.Length; j++) {
                            bool starts = (tok[i].StartsWith(fBlockDelimers[j]) &&
                                !tok[i].StartsWith("\\" + fBlockDelimers[j]));
                            bool ends   = (tok[i].EndsWith(fBlockDelimers[j]) &&
                                !tok[i].EndsWith("\\" + fBlockDelimers[j]));

                            if (starts && !ends) {
                                in_block = j;
                                break;
                            } else if (starts && ends) {
                                // Starting and ending quotes because this is a 1 token block
                                tok[i] = tok[i].Substring(1);
                                tok[i] = tok[i].Remove(tok[i].Length - 1);
                                break;
                            }
                        }

                        if (in_block == -1)
                            fTokens.Enqueue(tok[i]); // Wooo! We have a token :D
                        else
                            token_block += tok[i].Substring(1);
                    } // if ... else
                } // for "tokens in line"
            } // while (!r.EndOfStream)
        } // IReadTokens

        public string NextToken() {
            if (fTokens.Count > 0)
                return fTokens.Dequeue();
            else
                return null;
        }

        public string Peek() {
            if (fTokens.Count > 0)
                return fTokens.Peek();
            else
                return null;
        }
    }
}
