using System;
using System.Text;
using System.Windows.Forms;

namespace VaultMangler.Controls {
    public class NumericTextBox : TextBox {

        string fLastGoodText = "00000000";

        public uint ValueUnsigned {
            get { return uint.Parse(Text); }
        }

        public int ValueSinged {
            get { return int.Parse(Text); }
        }

        protected override void OnTextChanged(EventArgs e) {
            if (Text != fLastGoodText) {
                Int64 garbage;
                if (Int64.TryParse(Text, out garbage)) {
                    fLastGoodText = Text;
                    base.OnTextChanged(e);
                } else {
                    Text = fLastGoodText;
                }
            }
        }
    }
}
