using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plasma;

namespace VaultMangler {
    public partial class MainForm : Form {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        pnAuthClient fAuthCli = new pnAuthClient();
        pnFileClient fFileCli = new pnFileClient();
        pnGateClient fGateCli = new pnGateClient();

        public MainForm() {
            InitializeComponent();

            fVaultTree.AuthCli = fAuthCli;
            fVaultTree.SetStatus += SetStatus;
            fNodeControl.AuthCli = fAuthCli;
            fNodeControl.VaultTree = fVaultTree; // hax?

            fAuthCli.BranchID = 1;
            fFileCli.BranchID = 1;
            fGateCli.BranchID = 1;

            // haaaaax
            fAuthCli.PlayerInfo += (uint playerID, string name, string model) => fTempHoldingAvatars.Add(new Avatar(playerID, name));
        }

        private void SetStatus(string status="") {
            if (InvokeRequired)
                BeginInvoke(new Action(() => fStatusLabel.Text = status));
            else
                fStatusLabel.Text = status;
        }

        private void IDisconnect() {
            SetStatus();
            if (fAuthCli.SocketConnected) {
                fAuthCli.Disconnected -= IDisconnectedFromAuth;
                fAuthCli.Close();
            }
            BeginInvoke(new Action(() => fLogoutMenuItem.Visible = false));
            BeginInvoke(new Action(() => fLoginMenuItem.Visible = true));
        }

        private void ILogin(object sender, EventArgs e) {
            LoginForm lf = new LoginForm();
            DialogResult dr = lf.ShowDialog(this);

            switch (dr) {
                case DialogResult.Abort:
                    MessageBox.Show("There are no valid shard configuration files to use", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case DialogResult.OK:
                    // Looks good here... time to login
                    fLoginMenuItem.Visible = false;
                    fLogoutMenuItem.Visible = true;
                    SetStatus(String.Format("Logging into {0}...", lf.Shard.ToString()));
                    ILogin(lf.Account, lf.Password, lf.Shard);
                    break;
            }
        }

        private void ILogout(object sender, EventArgs e) {
            IDisconnect();
        }

        private void IQuit(object sender, EventArgs e) {
            Application.Exit();
        }

        #region Crazy Login Crap
        List<Avatar> fTempHoldingAvatars = new List<Avatar>();
        string fCacheUser;
        string fCachePass;

        private void ILogin(string acct, string pass, ServerIni shard) {
            fAuthCli.Host = shard.AuthHost; // educated guess
            fAuthCli.Port = shard.Port;
            fAuthCli.G = shard.AuthG;
            fAuthCli.N = shard.AuthN;
            fAuthCli.X = shard.AuthX;
            fAuthCli.ProductID = fFileCli.ProductID = fGateCli.ProductID = shard.ProductID;
            fGateCli.Host = shard.GateHost;
            fGateCli.Port = shard.Port;
            fGateCli.G = shard.GateG;
            fGateCli.N = shard.GateN;
            fGateCli.X = shard.GateX;

            fAuthCli.Connected += IConnectedToAuth;
            fAuthCli.Disconnected += IDisconnectedFromSomeServer;
            fFileCli.Connected += IConnectedToFile;
            fFileCli.Disconnected += IDisconnectedFromSomeServer;
            fGateCli.Connected += IConnectedToGate;
            fGateCli.Disconnected += IDisconnectedFromGate;

            // haaaax
            fCacheUser = acct;
            fCachePass = pass;

            if (shard.BuildId.HasValue) {
                fAuthCli.BuildID = (uint)shard.BuildId.Value;
                fAuthCli.ConnectAsync();
            } else {
                fGateCli.ConnectAsync();
            }
        }

        private void ILoginFailed(ENetError result) {
            MessageBox.Show(this, String.Format("Authentication Failed\r\nReason: {0}", result.ToString()),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            SetStatus();
            fLoginMenuItem.Visible = true;
            fLogoutMenuItem.Visible = false;
        }

        private void IConnectedToGate() {
            pnGateFileIP got_file_ip = (string host, object param) => {
                fFileCli.Port = fGateCli.Port; // may be overrwritten by host
                fFileCli.Host = host;
                fFileCli.ConnectAsync();
            };
            fGateCli.GetFileSrvIP(false, new pnCallback(got_file_ip));
        }

        private void IConnectedToFile() {
            // NOTE: may go to IDisconnectedFromGate (MOULa kicks gate/auth IP reqs off)
            pnFileBuildId got_build_id = (ENetError result, uint buildID, object param) => {
                fFileCli.Disconnected -= IDisconnectedFromSomeServer;
                fAuthCli.BuildID = buildID;

                // we should ask GateKeeperSrv for the auth IP, but that causes bad problems on MOULa
                // so we'll just assume everyone is on the same server. *whistles*
                fGateCli.Disconnected -= IDisconnectedFromGate;
                fGateCli.Close();
                fFileCli.Close();
                fAuthCli.ConnectAsync();
            };

            fFileCli.GetBuildId(new pnCallback(got_build_id));
        }

        private void IDisconnectedFromSomeServer() {
            BeginInvoke(new Action<ENetError>(ILoginFailed), new object[] { ENetError.kNetErrDisconnected });
            try {
                fFileCli.Disconnected -= IDisconnectedFromSomeServer;
            } catch { }
            try {
                fAuthCli.Disconnected -= IDisconnectedFromSomeServer;
            } catch { }
        }

        private void IDisconnectedFromGate() {
            fFileCli.Close();
            fGateCli.Close(); // let's make sure...
            fGateCli.Disconnected -= IDisconnectedFromGate;
            ILoginFailed(ENetError.kNetErrDisconnected);
        }

        private void IConnectedToAuth() {
            pnAuthLoggedIn on_login = (ENetError result, Guid acctGuid, uint[] droidKey, object param) => {
                fAuthCli.Disconnected -= IDisconnectedFromSomeServer;
                if (result == ENetError.kNetSuccess) {
                    fAuthCli.Disconnected += IDisconnectedFromAuth; // permanent handler

                    // MOSS wants you to do the silly secure download before setting a player.
                    // There seems to be a hole where if you ask for the manifest but never download
                    // it you get into a state equivalent to download completed...
                    fAuthCli.RequestFileList("python", "pak", new pnCallback(new Action<ENetError>(IGotStupidSecureFileList)));
                } else {
                    BeginInvoke(new Action<ENetError>(ILoginFailed), new object[] { result });
                    fAuthCli.Close();
                }
            };
            fAuthCli.Login(fCacheUser, fCachePass, new pnCallback(on_login));
        }

        private void IGotStupidSecureFileList(ENetError junk) {
            BeginInvoke(new Action(IAskAboutAvatars));
        }

        private void IDisconnectedFromAuth() {
            IDisconnect();
        }

        private void IAskAboutAvatars() {
            if (fTempHoldingAvatars.Count > 0) {
                AvatarForm af = new AvatarForm();
                af.Avatars = fTempHoldingAvatars.ToArray();
                if (af.ShowDialog(this) == DialogResult.OK) {
                    SetStatus(String.Format("Setting Active VNodeMgr ID:{0}...", af.PlayerID));
                    fAuthCli.SetPlayer(af.PlayerID, new pnCallback(new pnAuthPlayerSet(IOnPlayerSet), af.PlayerID));
                } else {
                    IDisconnect();
                }
            } else {
                IDisconnect();
                ILoginFailed(ENetError.kNetErrPlayerNotFound);
            }
        }

        private void IOnPlayerSet(ENetError result, object param) {
            if (InvokeRequired) {
                BeginInvoke(new pnAuthPlayerSet(IOnPlayerSet), new object[] { result, param });
                return;
            }

            if (result == ENetError.kNetSuccess) {
                fVaultTree.PlayerID = (uint)param;
                SetStatus();
            } else {
                IDisconnect();
                ILoginFailed(result);
            }
        }
        #endregion
    }
}
