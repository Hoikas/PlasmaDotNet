using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plasma;

namespace VaultMangler.Controls {
    public partial class PlayerInfoControl : NodeBackedControl {

        public override pnVaultNode VaultNode {
            set {
                pnVaultPlayerInfoNode info = new pnVaultPlayerInfoNode(value);
                fPlayerName.Text = info.PlayerName;
                fPlayerId.Text = info.PlayerID.ToString();
                fOnline.Checked = info.Online;
                fAgeInstanceName.Text = info.AgeInstanceName;
                fAgeInstanceUuid.Text = info.AgeInstanceUuid.ToString();

                IOnlineChecked(null, null);
            }
        }

        public PlayerInfoControl() {
            InitializeComponent();
        }

        private void IOnlineChecked(object sender, EventArgs e) {
            fAgeInstanceName.Enabled = fOnline.Checked;
            fAgeInstanceUuid.Enabled = fOnline.Checked;
        }
    }
}
