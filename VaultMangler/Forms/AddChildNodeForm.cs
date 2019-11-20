using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VaultMangler {
    public partial class AddChildNodeForm : Form {

        public bool ParentEnabled {
            set { fParentID.Enabled = value; }
        }

        public uint ParentID {
            get { return fParentID.ValueUnsigned; }
            set { fParentID.Text = value.ToString(); }
        }

        public uint ChildID {
            get { return fChildID.ValueUnsigned; }
            set { fChildID.Text = value.ToString(); }
        }

        public uint SaverID {
            get { return fSaverID.ValueUnsigned; }
            set { fSaverID.Text = value.ToString(); }
        }

        public AddChildNodeForm() {
            InitializeComponent();
        }
    }
}
