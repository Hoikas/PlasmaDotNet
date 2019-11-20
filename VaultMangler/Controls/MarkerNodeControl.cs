using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Plasma;

namespace VaultMangler.Controls {
    public partial class MarkerNodeControl : NodeBackedControl {

        private byte[] fGameData;

        public override pnVaultNode VaultNode {
            set {
                pnVaultMarkerListNode game = new pnVaultMarkerListNode(value);
                fGameData = game.GameData;
                fGameNameTextBox.Text = game.GameName;
                fGameRewardTextBox.Text = game.GameReward;
            }
        }

        public MarkerNodeControl() {
            InitializeComponent();
        }

        private void IExportGameAsYAML(object sender, LinkLabelLinkClickedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".yaml";
            sfd.Filter = "YAML Ain't Markup Language (*.yaml)|*.yaml";
            if (sfd.ShowDialog(this) == DialogResult.OK) {
                hsStream s = new hsStream(new MemoryStream(fGameData));
                if (s.BaseStream.Length == 0) {
                    MessageBox.Show("Cannot export GameMgr style marker games!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                uint numMarkers = s.ReadUInt();
                if (numMarkers == 0) {
                    MessageBox.Show("Empty marker game.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                StringBuilder yaml = new StringBuilder();
                yaml.AppendLine(String.Format("name: {0}", fGameNameTextBox.Text));
                for (uint i = 0; i < numMarkers; i++) {
                    uint markerId = s.ReadUInt(); // garbage in this format
                    string age = s.ReadSafeString();
                    float x = s.ReadFloat();
                    float y = s.ReadFloat();
                    float z = s.ReadFloat();
                    string text = s.ReadSafeString();

                    yaml.AppendLine(String.Format("-   age: {0}", age));
                    yaml.AppendLine(String.Format("    coords: [{0}, {1}, {2}]", x, y, z));
                    yaml.AppendLine(String.Format("    text: {0}", text));
                }
                s.Close();

                StreamWriter writer = File.CreateText(sfd.FileName);
                writer.Write(yaml.ToString());
                writer.Close();
            }
        }
    }
}
