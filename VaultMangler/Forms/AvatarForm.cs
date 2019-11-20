using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VaultMangler {
    public partial class AvatarForm : Form {

        public Avatar[] Avatars {
            set { fAvatarBox.Items.AddRange(value); }
        }

        public uint PlayerID {
            get {
                if (fAvatarBox.SelectedItem != null)
                    return ((Avatar)fAvatarBox.SelectedItem).fPlayerID;
                return 0;
            }
        }

        public AvatarForm() {
            InitializeComponent();
        }

        private void IOkayButtonClick(object sender, EventArgs e) {
            if (fAvatarBox.SelectedItem == null)
                MessageBox.Show("You must select an avatar", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                DialogResult = DialogResult.OK;
        }
    }

    public class Avatar {
        public uint fPlayerID;
        public string fName;

        public Avatar(uint player, string name) {
            fPlayerID = player;
            fName = name;
        }

        public override string ToString() {
            return String.Format("{0} ({1})", fName, fPlayerID);
        }
    }
}
