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
    public partial class BasicNodeControl : NodeBackedControl {

        public override pnVaultNode VaultNode {
            set {
                if (value == null) {
                    fNodeIdBox.Text = "";
                    fCreatorIdBox.Text = "";
                    fCreateTimePicker.Value = plUnifiedTime.Epoch;
                    fModifyTimePicker.Value = plUnifiedTime.Epoch;
                } else {
                    fNodeIdBox.Text = value.NodeID.ToString();
                    fCreatorIdBox.Text = value.CreatorID.ToString();
                    fCreateTimePicker.Value = value.CreateTime;
                    fModifyTimePicker.Value = value.ModifyTime;
                }
            }
        }

        public BasicNodeControl() {
            InitializeComponent();
        }

        public override void SaveDamage(pnVaultNode node) {
            if (fCreateTimePicker.Value != plUnifiedTime.Epoch)
                node.CreateTime = fCreateTimePicker.Value;
            if (fModifyTimePicker.Value != plUnifiedTime.Epoch)
                node.ModifyTime = fModifyTimePicker.Value;
        }
    }
}
