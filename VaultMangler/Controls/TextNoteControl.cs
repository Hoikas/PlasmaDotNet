using System;
using System.Windows.Forms;
using Plasma;

namespace VaultMangler.Controls {
    public partial class TextNoteControl : NodeBackedControl {

        public override pnVaultNode VaultNode {
            set {
                pnVaultTextNode note = new pnVaultTextNode(value);
                fNoteTitle.Text = note.NoteName;
                fNoteText.Text = note.Text;
            }
        }

        public TextNoteControl() {
            InitializeComponent();
        }

        public override void SaveDamage(pnVaultNode node) {
            pnVaultTextNode note = new pnVaultTextNode(node);
            note.NoteName = fNoteTitle.Text;
            note.Text = fNoteText.Text;
        }
    }
}
