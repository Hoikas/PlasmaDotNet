using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace VaultMangler {
    public partial class LoginForm : Form {

        public string Account {
            get { return fAccountBox.Text; }
        }

        public string Password {
            get { return fPasswordBox.Text; }
        }

        public ServerIni Shard {
            get { return (ServerIni)fServerComboBox.SelectedItem; }
        }

        private RegistryKey RegKey {
            get { return Registry.CurrentUser.CreateSubKey("Software\\Plasma.NET\\Vault Mangler"); }
        }

        public LoginForm() {
            InitializeComponent();
            InitializeServerList();
            IFillInDetails();
        }

        private void InitializeServerList() {
            IEnumerable<string> inis = Directory.EnumerateFiles(Environment.CurrentDirectory, "server*.ini");
            foreach (string ini in inis) {
                ServerIni tryParseIni = ServerIni.CreateFromFile(ini);
                if (tryParseIni != null)
                    fServerComboBox.Items.Add(tryParseIni);
            }
        }

        private void IFillInDetails() {
            using (RegistryKey key = RegKey) {
                string shard = (string)key.GetValue("LastShard", "");
                fServerComboBox.SelectedIndex = fServerComboBox.FindStringExact(shard);
                fAccountBox.Text = (string)key.GetValue("Account", "");
                fPasswordBox.Text = (string)key.GetValue("Password", "");
                if (!String.IsNullOrEmpty(fAccountBox.Text) && !String.IsNullOrEmpty(fPasswordBox.Text))
                    fRemember.Checked = true;
            }
        }

        private void ILogin(object sender, EventArgs e) {
            if (String.IsNullOrWhiteSpace(Account)) {
                MessageBox.Show("You must specify an account", "Account Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrWhiteSpace(Password)) {
                MessageBox.Show("You must specify a password", "Password Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fRemember.Checked) {
                DialogResult dr = MessageBox.Show(this,
                    "Your password will be saved in the registry in plain text. Is this OK?",
                    "Security Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes)
                    return;
            }

            using (RegistryKey key = RegKey) {
                if (fRemember.Checked) {
                    key.SetValue("LastShard", Shard.ToString(), RegistryValueKind.String);
                    key.SetValue("Account", Account);
                    key.SetValue("Password", Password);
                } else {
                    key.DeleteValue("Account", false);
                    key.DeleteValue("Password", false);
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void IOnFormShown(object sender, EventArgs e) {
            if (fServerComboBox.Items.Count == 0)
                DialogResult = DialogResult.Abort;
        }
    }
}
