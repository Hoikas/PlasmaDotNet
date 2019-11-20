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
    public partial class ChronicleControl : NodeBackedControl {

        public override pnVaultNode VaultNode {
            set {
                pnVaultChronicleNode chron = new pnVaultChronicleNode(value);
                fName.Text = chron.EntryName;
                fValue.Text = chron.EntryValue;
            }
        }

        public ChronicleControl() {
            InitializeComponent();
        }

        public override void SaveDamage(pnVaultNode node) {
            pnVaultChronicleNode chron = new pnVaultChronicleNode(node);
            chron.EntryName = fName.Text;
            chron.EntryValue = fValue.Text;
        }
    }
}
