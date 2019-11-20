using System;
using System.Windows.Forms;
using Plasma;

namespace VaultMangler
{
    public partial class NodeDetailForm : Form {

        public pnVaultNode VaultNode {
            set {
                // Signed Ints
                if (value.Int32_1.HasValue)
                    fInt32_1.Text = value.Int32_1.ToString();
                if (value.Int32_2.HasValue)
                    fInt32_2.Text = value.Int32_2.ToString();
                if (value.Int32_3.HasValue)
                    fInt32_3.Text = value.Int32_3.ToString();
                if (value.Int32_4.HasValue)
                    fInt32_4.Text = value.Int32_4.ToString();

                // Unsigned Ints
                if (value.UInt32_1.HasValue)
                    fUInt32_1.Text = value.UInt32_1.ToString();
                if (value.UInt32_2.HasValue)
                    fUInt32_2.Text = value.UInt32_2.ToString();
                if (value.UInt32_3.HasValue)
                    fUInt32_3.Text = value.UInt32_3.ToString();
                if (value.UInt32_4.HasValue)
                    fUInt32_4.Text = value.UInt32_4.ToString();

                // UUIDs
                if (value.Uuid_1.HasValue)
                    fUuid_1.Text = value.Uuid_1.ToString();
                if (value.Uuid_2.HasValue)
                    fUuid_2.Text = value.Uuid_2.ToString();
                if (value.Uuid_3.HasValue)
                    fUuid_3.Text = value.Uuid_3.ToString();
                if (value.Uuid_4.HasValue)
                    fUuid_4.Text = value.Uuid_4.ToString();

                // Case sensitive strings
                fString64_1.Text = value.String64_1;
                fString64_2.Text = value.String64_2;
                fString64_3.Text = value.String64_3;
                fString64_4.Text = value.String64_4;
                fString64_5.Text = value.String64_5;
                fString64_6.Text = value.String64_6;

                // Case insensitive strings
                fIString64_1.Text = value.IString64_1;
                fIString64_2.Text = value.IString64_2;

                // Text
                fText_1.Text = value.Text_1;
                fText_2.Text = value.Text_2;
            }
        }

        public NodeDetailForm() {
            InitializeComponent();
        }

        private int? ISaveDamage(MaskedTextBox src, int? dst) {
            if (String.IsNullOrWhiteSpace(src.Text))
                return null;
            else
                return Convert.ToInt32(src.Text);
        }

        private uint? ISaveDamage(MaskedTextBox src, uint? dst) {
            if (String.IsNullOrWhiteSpace(src.Text))
                return null;
            else
                return Convert.ToUInt32(src.Text);
        }

        private Guid? ISaveDamage(TextBox src, Guid? dst) {
            Guid result;
            if (String.IsNullOrWhiteSpace(src.Text))
                return null;
            else if (Guid.TryParse(src.Text, out result))
                return result;
            else
                return dst;
        }

        private string ISaveDamage(TextBox src, string dst) {
            if (String.IsNullOrWhiteSpace(src.Text))
                return null;
            else
                return src.Text;
        }

        public void SaveDamage(pnVaultNode node) {
            // Signed Ints
            node.Int32_1 = ISaveDamage(fInt32_1, node.Int32_1);
            node.Int32_2 = ISaveDamage(fInt32_2, node.Int32_2);
            node.Int32_3 = ISaveDamage(fInt32_3, node.Int32_3);
            node.Int32_4 = ISaveDamage(fInt32_4, node.Int32_4);

            // Unsigned Ints
            node.UInt32_1 = ISaveDamage(fUInt32_1, node.UInt32_1);
            node.UInt32_2 = ISaveDamage(fUInt32_2, node.UInt32_2);
            node.UInt32_3 = ISaveDamage(fUInt32_3, node.UInt32_3);
            node.UInt32_4 = ISaveDamage(fUInt32_4, node.UInt32_4);

            // UUIDs
            node.Uuid_1 = ISaveDamage(fUuid_1, node.Uuid_1);
            node.Uuid_2 = ISaveDamage(fUuid_2, node.Uuid_2);
            node.Uuid_3 = ISaveDamage(fUuid_3, node.Uuid_3);
            node.Uuid_4 = ISaveDamage(fUuid_4, node.Uuid_4);

            // Case Sensitive Strings
            node.String64_1 = ISaveDamage(fString64_1, node.String64_1);
            node.String64_2 = ISaveDamage(fString64_2, node.String64_2);
            node.String64_3 = ISaveDamage(fString64_3, node.String64_3);
            node.String64_4 = ISaveDamage(fString64_4, node.String64_4);
            node.String64_5 = ISaveDamage(fString64_5, node.String64_5);
            node.String64_6 = ISaveDamage(fString64_6, node.String64_6);

            // Case Insensitive Strings
            node.IString64_1 = ISaveDamage(fIString64_1, node.IString64_1);
            node.IString64_2 = ISaveDamage(fIString64_2, node.IString64_2);

            // Long Text
            node.Text_1 = ISaveDamage(fText_1, node.Text_1);
            node.Text_2 = ISaveDamage(fText_2, node.Text_2);
        }
    }
}
