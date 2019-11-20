using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Plasma;
using VaultMangler.Properties;

namespace VaultMangler.Controls {
    public partial class VaultTreeControl : UserControl {

        /// <summary>
        /// Sorts vault backed tree nodes
        /// </summary>
        class NodeSorter : System.Collections.IComparer {
            Dictionary<uint, pnVaultNode> fNodePtr; // ptr to main collection

            public NodeSorter(Dictionary<uint, pnVaultNode> col) { fNodePtr = col; }

            public int Compare(object x, object y) {
                TreeNode lhs = (TreeNode)x;
                TreeNode rhs = (TreeNode)y;

                // SPECIAL CASE: Sort age links by the age infos...
                if (fNodePtr.ContainsKey((uint)lhs.Tag) && fNodePtr.ContainsKey((uint)rhs.Tag)) {
                    pnVaultNode lhs_node = fNodePtr[(uint)lhs.Tag];
                    pnVaultNode rhs_node = fNodePtr[(uint)rhs.Tag];

                    // null nodes are things we've requested but not received
                    if (lhs_node != null && rhs_node != null) {
                        if (lhs_node.NodeType == ENodeType.kNodeAgeLink && rhs_node.NodeType == ENodeType.kNodeAgeLink) {
                            // make sure we actually have age infos
                            if (lhs.Nodes.Count > 0 && rhs.Nodes.Count > 0) {
                                // Text should be equal to the age instance name
                                // if it's not, we're still downloading the vault, so who cares.
                                return lhs.Nodes[0].Text.CompareTo(rhs.Nodes[0].Text);
                            }
                        }
                    }

                    // If either node is null, we'll compare their IDs to make it quick
                    else
                        return ((uint)lhs.Tag).CompareTo((uint)rhs.Tag);
                }

                // If we have a null name... good luck
                if (lhs.Text == null)
                    return -1;
                else if (rhs.Text == null)
                    return 1;
                else
                    // Fall back to string sorting
                    return lhs.Text.CompareTo(rhs.Text);
            }
        }

        class VaultNodeTreeNodes {
            public uint fNodeID;
            public List<TreeNode> fTreeNodes = new List<TreeNode>();
            public SortedSet<uint> fParentIDs = new SortedSet<uint>();
            public SortedSet<uint> fChildrenIDs = new SortedSet<uint>();
            public bool fIsRoot;

            public VaultNodeTreeNodes(uint nodeID, bool root=false) {
                fNodeID = nodeID;
                fIsRoot = root;
            }
        }

        enum VaultIcons {
            kDefaultIcon,
            kGenericFolder, kAgeFolder, kPlayerFolder,
            kTextNote, kPicture,
            kMalePlayer, kFemalePlayer, kSpecialPlayer,
            kOnlinePlayerInfo, kOfflinePlayerInfo,
            kAgeMgr, kAgeInfo, kAgeLink,
            kChronicleParent, kChronicleChild,
            kSDL, kSystem,
            kMarkerGame,
        }

        pnAuthClient fClient;
        public pnAuthClient AuthCli {
            set {
                fClient = value;
                fClient.Disconnected += IOnDisconnect;
                fClient.VaultNodeAdded += INodeAdded;
                fClient.VaultNodeChanged += INodeChanged;
            }
        }

        public uint PlayerID {
            get { return fPlayerID; }
            set {
                Cleanup();
                fPlayerID = value;
                fClient.FetchVaultNodeRefs(value, new pnCallback(new pnAuthVaultNodeRefsFetched(INodeTreeFetched), value));
            }
        }

        public event NodeSelected NodeSelected;
        public pnVaultNode SelectedNode {
            get {
                if (fTreeView.SelectedNode != null)
                    return fVaultNodes[(uint)fTreeView.SelectedNode.Tag];
                return null;
            }
        }

        plDebugLog fLog = plDebugLog.GetLog("vault");
        Dictionary<uint, pnVaultNode> fVaultNodes = new Dictionary<uint, pnVaultNode>();
        Dictionary<uint, VaultNodeTreeNodes> fVaultTreeNodes = new Dictionary<uint, VaultNodeTreeNodes>();
        SortedSet<uint> fPendingNodes = new SortedSet<uint>();
        System.Timers.Timer fUpdateTimer = new System.Timers.Timer(1000.0);
        int fNumNodeDLoads = 0;
        uint fPlayerID = 0;

        public VaultTreeControl() {
            InitializeComponent();

            // this is slow as tits, so disable it.
            //fTreeView.TreeViewNodeSorter = new NodeSorter(fVaultNodes);

            // Periodically update while downloading nodes.
            fUpdateTimer.Elapsed += IOnUpdateTime;

            // Setup images -- see VaultIcons
            fTreeView.ImageList = new ImageList();
            fTreeView.ImageList.Images.Add(Resources.cog);
            fTreeView.ImageList.Images.Add(Resources.folder);
            fTreeView.ImageList.Images.Add(Resources.folder_image);
            fTreeView.ImageList.Images.Add(Resources.folder_user);
            fTreeView.ImageList.Images.Add(Resources.note);
            fTreeView.ImageList.Images.Add(Resources.picture);
            fTreeView.ImageList.Images.Add(Resources.user);
            fTreeView.ImageList.Images.Add(Resources.user_female);
            fTreeView.ImageList.Images.Add(Resources.user_gray);
            fTreeView.ImageList.Images.Add(Resources.user_green);
            fTreeView.ImageList.Images.Add(Resources.user_red);
            fTreeView.ImageList.Images.Add(Resources.book);
            fTreeView.ImageList.Images.Add(Resources.book_open);
            fTreeView.ImageList.Images.Add(Resources.book_link);
            fTreeView.ImageList.Images.Add(Resources.tag_red);
            fTreeView.ImageList.Images.Add(Resources.tag_blue);
            fTreeView.ImageList.Images.Add(Resources.script_gear);
            fTreeView.ImageList.Images.Add(Resources.database);
            fTreeView.ImageList.Images.Add(Resources.sport_golf);
        }

        private void Cleanup() {
            fTreeView.Nodes.Clear();
            fVaultNodes.Clear();
            fVaultTreeNodes.Clear();
            fNumNodeDLoads = 0;

            NodeSelected?.Invoke(null);
        }

        private void IAddChildNode(object sender, EventArgs e) {
            AddChildNodeForm acnf = new AddChildNodeForm();
            acnf.ParentEnabled = false;
            acnf.ParentID = (uint)fTreeView.SelectedNode.Tag;
            acnf.SaverID = PlayerID;
            if (acnf.ShowDialog() == DialogResult.OK) {
                fClient.AddVaultNode(acnf.ParentID, acnf.ChildID, acnf.SaverID);
            }
        }

        private void IChildNodeCreated(ENetError result, uint nodeID, object param) {
            if (result != ENetError.kNetSuccess) {
                MessageBox.Show("Failed to create child node", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            fClient.AddVaultNode((uint)param, nodeID, 0);
        }

        private void ICreateChildFolderNode(object sender, EventArgs e) {
            ICreateChildNode(ENodeType.kNodeFolder);
        }

        private void ICreateChildImageNode(object sender, EventArgs e) {
            ICreateChildNode(ENodeType.kNodeImage);
        }

        private void ICreateChildTextNode(object sender, EventArgs e) {
            ICreateChildNode(ENodeType.kNodeTextNote);
        }

        private void ICreateChildNode(ENodeType type) {
            uint parent = (uint)fTreeView.SelectedNode.Tag;
            pnVaultNode node = new pnVaultNode(type);
            fClient.CreateVaultNode(node, new pnCallback(new pnAuthVaultNodeCreated(IChildNodeCreated), parent));
        }

        private void IDownloadNode(uint nodeID) {
            Interlocked.Increment(ref fNumNodeDLoads);
            lock (fVaultNodes) {
                if (!fVaultNodes.ContainsKey(nodeID))
                    fVaultNodes.Add(nodeID, null); // so we don't redownload it 1000 times
            }
            fUpdateTimer.Enabled = true;
            fClient.FetchVaultNode(nodeID, new pnCallback(new pnAuthVaultNodeFetched(INodeDownloaded), nodeID));
            fLog.Debug(String.Format("DBG: Issued download [ID: {0}]", nodeID));
        }

        private TreeNode IGenerateTreeNode(pnVaultNode vaultNode) {
            TreeNode tn = new TreeNode();
            tn.ContextMenuStrip = fNodeContextMenu;
            tn.Name = vaultNode.NodeID.ToString();
            tn.Tag = vaultNode.NodeID;
            INameTreeNode(tn, vaultNode);
            return tn;
        }

        private string IGetStandardNodeName(pnVaultNode node) {
            if (node.NodeType == ENodeType.kNodeFolder || node.NodeType == ENodeType.kNodePlayerInfoList ||
                node.NodeType == ENodeType.kNodeAgeInfoList) {
                if (!String.IsNullOrWhiteSpace(node.String64_1))
                    return node.String64_1;
            }

            switch ((EStandardNode)(node.Int32_1 ?? (int)EStandardNode.kUserDefinedNode)) {
                case EStandardNode.kAgeDevicesFolder:
                    return "Age Devices";
                case EStandardNode.kAgeJournalsFolder:
                    return "Age Journals";
                case EStandardNode.kAgeMembersFolder:
                    return "Age Members";
                case EStandardNode.kAgeOwnersFolder:
                    return "Age Owners";
                case EStandardNode.kAgesICanVisitFolder:
                    return "Ages I Can Visit";
                case EStandardNode.kAgesIOwnFolder:
                    return "Ages I Own";
                case EStandardNode.kAllAgeGlobalSDLNodesFolder:
                    return "All Age Global SDL Nodes";
                case EStandardNode.kAllPlayersFolder:
                    return "All Players";
                case EStandardNode.kAvatarClosetFolder:
                    return "Avatar Closet";
                case EStandardNode.kAvatarOutfitFolder:
                    return "Avatar Outfit";
                case EStandardNode.kBuddyListFolder:
                    return "Buddy List";
                case EStandardNode.kCanVisitFolder:
                    return "Can Visit";
                case EStandardNode.kChildAgesFolder:
                    return "Child Ages";
                case EStandardNode.kChronicleFolder:
                    return "Chronicle Folder";
                case EStandardNode.kDeviceInboxFolder:
                    return "Device Inbox";
                case EStandardNode.kGlobalInboxFolder:
                    return "Global Inbox";
                case EStandardNode.kIgnoreListFolder:
                    return "Ignore List";
                case EStandardNode.kInboxFolder:
                    return "Inbox";
                case EStandardNode.kPeopleIKnowAboutFolder:
                    return "People I Know About";
                case EStandardNode.kPlayerInviteFolder:
                    return "Player Invites";
                case EStandardNode.kSubAgesFolder:
                    return "Subage Folder";
                default:
                    return "???";
            }
        }

        private void IOnDisconnect() {
            if (InvokeRequired)
                BeginInvoke(new MethodInvoker(Cleanup));
            else
                Cleanup();
        }

        private void IOnNodeSelect(object sender, TreeViewEventArgs e) {
            if (e.Node != null) {
                pnVaultNode node = fVaultNodes[(uint)e.Node.Tag];

                // fire this off to the OverallNodeControl
                if (node != null)
                    NodeSelected(node);
            }
        }

        private void IOnUpdateTime(object sender, System.Timers.ElapsedEventArgs e) {
            if (InvokeRequired)
                BeginInvoke(new Action(INodeTreeUpdate));
            else
                INodeTreeUpdate();
        }

        private void INameTreeNode(TreeNode tn, pnVaultNode node) {
            switch (node.NodeType) {
                case ENodeType.kNodeAge:
                    tn.Text = node.String64_1;
                    ISetIcon(tn, VaultIcons.kAgeMgr);
                    break;
                case ENodeType.kNodeAgeInfo:
                    tn.Text = node.String64_3;
                    ISetIcon(tn, VaultIcons.kAgeInfo);
                    break;
                case ENodeType.kNodeAgeInfoList:
                    tn.Text = IGetStandardNodeName(node);
                    ISetIcon(tn, VaultIcons.kAgeFolder);
                    break;
                case ENodeType.kNodeAgeLink:
                    tn.Text = "Age Link";
                    ISetIcon(tn, VaultIcons.kAgeLink);
                    break;
                case ENodeType.kNodeChronicle:
                    tn.Text = node.String64_1;
                    if (tn.Nodes.Count == 0)
                        ISetIcon(tn, VaultIcons.kChronicleChild);
                    else
                        ISetIcon(tn, VaultIcons.kChronicleParent);
                    break;
                case ENodeType.kNodeFolder:
                    tn.Text = IGetStandardNodeName(node);
                    ISetIcon(tn, VaultIcons.kGenericFolder);
                    break;
                case ENodeType.kNodeImage:
                    tn.Text = node.String64_1;
                    ISetIcon(tn, VaultIcons.kPicture);
                    break;
                case ENodeType.kNodeMarkerList:
                    tn.Text = node.Text_1;
                    ISetIcon(tn, VaultIcons.kMarkerGame);
                    break;
                case ENodeType.kNodePlayer:
                    pnVaultPlayerNode player = new pnVaultPlayerNode(node);
                    string shape = player.AvatarShape.ToLower();
                    if (shape == "male")
                        ISetIcon(tn, VaultIcons.kMalePlayer);
                    else if (shape == "female")
                        ISetIcon(tn, VaultIcons.kFemalePlayer);
                    else
                        ISetIcon(tn, VaultIcons.kSpecialPlayer);
                    tn.Text = player.PlayerName;
                    break;
                case ENodeType.kNodePlayerInfo:
                    pnVaultPlayerInfoNode info = new pnVaultPlayerInfoNode(node);
                    if (info.Online)
                        ISetIcon(tn, VaultIcons.kOnlinePlayerInfo);
                    else
                        ISetIcon(tn, VaultIcons.kOfflinePlayerInfo);
                    tn.Text = info.PlayerName;
                    break;
                case ENodeType.kNodePlayerInfoList:
                    tn.Text = IGetStandardNodeName(node);
                    ISetIcon(tn, VaultIcons.kPlayerFolder);
                    break;
                case ENodeType.kNodeSDL:
                    tn.Text = "SDL";
                    ISetIcon(tn, VaultIcons.kSDL);
                    break;
                case ENodeType.kNodeSystem:
                    tn.Text = "System";
                    ISetIcon(tn, VaultIcons.kSystem);
                    break;
                case ENodeType.kNodeTextNote:
                    tn.Text = node.String64_1;
                    ISetIcon(tn, VaultIcons.kTextNote);
                    break;
                default:
                    tn.Text = "???";
                    break;
            }
        }

        private void INodeAdded(uint parentID, uint childID, uint saverID) {
            lock (fVaultTreeNodes) {
                if (!fVaultTreeNodes.ContainsKey(parentID)) {
                    fLog.Error(String.Format("ERROR: Received add update for unknown parent [ID: {0}]", parentID));
                    return;
                }

                fVaultTreeNodes[parentID].fChildrenIDs.Add(childID);
                if (!fVaultTreeNodes.ContainsKey(childID))
                    fVaultTreeNodes.Add(childID, new VaultNodeTreeNodes(childID));
                fVaultTreeNodes[childID].fParentIDs.Add(parentID);
            }

            lock (fVaultNodes) {
                if (!fVaultNodes.ContainsKey(childID))
                    IDownloadNode(childID);
                else
                    BeginInvoke(new Action(INodeTreeUpdate));
            }
        }

        private void INodeChanged(uint nodeID) {
            lock (fVaultNodes) {
                if (fVaultNodes.ContainsKey(nodeID))
                    IDownloadNode(nodeID);
            }
        }

        private void INodeDownloaded(ENetError result, pnVaultNode node, object param) {
            int numDLoads = Interlocked.Decrement(ref fNumNodeDLoads);

            if (result != ENetError.kNetSuccess || node == null) {
                fLog.Error(String.Format("ERROR: Node Fetch failed [E: '{0}'] [ID: {1}]",
                                         result.ToString(), param));
                lock (fVaultNodes)
                    fVaultNodes.Remove((uint)param);
            } else {
                fLog.Debug(String.Format("DBG: Downloaded Node [ID: {0}]", node.NodeID));
                lock (fVaultNodes) {
                    fPendingNodes.Add(node.NodeID);
                    fVaultNodes[node.NodeID] = node;
                }
            }

            // Pumping the node tree is slow, so wait until the entire vault is downloaded.
            if (numDLoads == 0) {
                fLog.Debug("DBG: All node downloads complete, updating tree...");
                fUpdateTimer.Enabled = false;
                if (InvokeRequired)
                    BeginInvoke(new Action(INodeTreeUpdate));
                else
                    INodeTreeUpdate();
            }
        }

        private void INodeTreeFetched(ENetError result, pnVaultNodeRef[] refs, object param) {
            SortedSet<uint> requests = new SortedSet<uint>();
            foreach (pnVaultNodeRef nodeRef in refs) {
                lock (fVaultTreeNodes) {
                    if (!fVaultTreeNodes.ContainsKey(nodeRef.fParent)) {
                        bool isRoot = (uint)param == nodeRef.fParent;
                        if (isRoot)
                            fLog.Debug(String.Format("DBG: root level node [ID: {0}]", nodeRef.fParent));
                        else
                            fLog.Error(String.Format("ERROR: orphaned node [ID: {0}]", nodeRef.fParent));
                        fVaultTreeNodes.Add(nodeRef.fParent, new VaultNodeTreeNodes(nodeRef.fParent, isRoot));
                    }

                    fVaultTreeNodes[nodeRef.fParent].fChildrenIDs.Add(nodeRef.fChild);
                    if (!fVaultTreeNodes.ContainsKey(nodeRef.fChild))
                        fVaultTreeNodes.Add(nodeRef.fChild, new VaultNodeTreeNodes(nodeRef.fChild));
                    fVaultTreeNodes[nodeRef.fChild].fParentIDs.Add(nodeRef.fParent);
                }

                requests.Add(nodeRef.fParent);
                requests.Add(nodeRef.fChild);
            }

            foreach (uint nodeID in requests)
                IDownloadNode(nodeID);
        }

        private void INodeTreeUpdate() {
            lock (fVaultTreeNodes) {
                lock (fVaultNodes) {
                    fTreeView.BeginUpdate();
                    foreach (uint nodeID in fPendingNodes) {
                        IUpdateVaultTreeNode(fVaultNodes[nodeID]);
                    }
                    fTreeView.EndUpdate();
                    fPendingNodes.Clear();
                }
            }
        }

        private void ISetIcon(TreeNode tn, VaultIcons icon) {
            tn.ImageIndex = (int)icon;
            tn.SelectedImageIndex = (int)icon;
        }

        private void IUpdateVaultTreeNode(pnVaultNode node) {
            lock (fVaultTreeNodes) {
                VaultNodeTreeNodes vntn = fVaultTreeNodes[node.NodeID];

                // add relationships
                if (vntn.fIsRoot)
                    IUpdateOrCreateTreeNode(node);

                // Hopefully top-down is sufficient...
                foreach (uint parentID in vntn.fParentIDs) {
                    foreach (TreeNode tn in fVaultTreeNodes[parentID].fTreeNodes)
                        IUpdateOrCreateTreeNode(node, tn);
                }

                // Apparently not...
                foreach (uint childID in vntn.fChildrenIDs) {
                    pnVaultNode childNode;
                    lock (fVaultNodes)
                        childNode = fVaultNodes[childID];
                    if (childNode != null) {
                        foreach (TreeNode tn in vntn.fTreeNodes)
                            IUpdateOrCreateTreeNode(childNode, tn);
                    }
                }
            }
        }

        private void IUpdateOrCreateTreeNode(pnVaultNode vaultNode, TreeNode parentTreeNode = null) {
            TreeNodeCollection col;
            if (parentTreeNode == null)
                col = fTreeView.Nodes;
            else
                col = parentTreeNode.Nodes;

            TreeNode tn = col[vaultNode.NodeID.ToString()];
            if (tn == null) {
                tn = new TreeNode();
                tn.ContextMenuStrip = fNodeContextMenu;
                tn.Name = vaultNode.NodeID.ToString();
                tn.Tag = vaultNode.NodeID;
                INameTreeNode(tn, vaultNode);
                col.Add(tn);
                lock (fVaultTreeNodes)
                    fVaultTreeNodes[vaultNode.NodeID].fTreeNodes.Add(tn);
            } else {
                INameTreeNode(tn, vaultNode);
            }
        }
    }

    public delegate void NodeSelected(pnVaultNode node);
}
