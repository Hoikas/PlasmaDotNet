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
    public partial class OverallNodeControl : UserControl {

        pnAuthClient fClient;
        public pnAuthClient AuthCli {
            set { fClient = value; }
        }

        VaultTreeControl fTree;
        public VaultTreeControl VaultTree {
            set {
                fTree = value;
                fTree.NodeSelected += INodeSelected;
            }
        }

        NodeBackedControl fDetailCtrl;

        public OverallNodeControl() {
            InitializeComponent();
        }

        private void INodeSelected(pnVaultNode node) {
            fBasicInfo.VaultNode = node;
            fNodeDetailPanel.Controls.Clear();
            if (node == null)
                return;

            // Lazy node details panel
            switch (node.NodeType) {
                case ENodeType.kNodeChronicle:
                    fDetailCtrl = new ChronicleControl();
                    break;
                case ENodeType.kNodeImage:
                    fDetailCtrl = new ImageNodeControl();
                    break;
                case ENodeType.kNodeMarkerList:
                    fDetailCtrl = new MarkerNodeControl();
                    break;
                case ENodeType.kNodePlayerInfo:
                    fDetailCtrl = new PlayerInfoControl();
                    break;
                case ENodeType.kNodeTextNote:
                    fDetailCtrl = new TextNoteControl();
                    break;
                default:
                    fDetailCtrl = null;
                    break;
            }

            if (fDetailCtrl != null) {
                fDetailCtrl.VaultNode = node;
                fDetailCtrl.Dock = DockStyle.Fill;
                fNodeDetailPanel.Controls.Add(fDetailCtrl);
            }
        }

        private void IResetNode(object sender, EventArgs e) {
            pnVaultNode node = fTree.SelectedNode;
            if (node == null)
                return;

            fBasicInfo.VaultNode = node;
            if (fDetailCtrl != null)
                fDetailCtrl.VaultNode = node;
        }

        private void ISaveNode(object sender, EventArgs e) {
            pnVaultNode node = fTree.SelectedNode;
            if (node == null)
                return;

            fBasicInfo.SaveDamage(node);
            if (fDetailCtrl != null)
                fDetailCtrl.SaveDamage(node);
            fClient.SaveVaultNode(node); // we don't really care about the results
        }

        private void IViewNodeDetail(object sender, LinkLabelLinkClickedEventArgs e) {
            pnVaultNode node = fTree.SelectedNode;
            if (node != null) {
                NodeDetailForm ndf = new NodeDetailForm();
                ndf.VaultNode = node;
                ndf.ShowDialog();

                ndf.SaveDamage(node);
                INodeSelected(node); // hax?
            }
        }
    }
}
