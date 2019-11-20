using System;
using Plasma;

namespace VaultMangler.Controls {
    public class NodeBackedControl : System.Windows.Forms.UserControl {
        public virtual pnVaultNode VaultNode { set { } }
        public virtual void SaveDamage(pnVaultNode node) { }
    }
}
