using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession {

        private uint? ICreateStdNode(EStandardNode type) { return ICreateStdNode(type, 0, Guid.Empty); }
        private uint? ICreateStdNode(EStandardNode type, uint cIdx, Guid cGuid) {
            pnVaultNodeAccess vna = null;
            switch (type) {
                case EStandardNode.kAgeDevicesFolder:
                case EStandardNode.kAgeJournalsFolder:
                case EStandardNode.kAgeTypeJournalFolder:
                case EStandardNode.kAllAgeGlobalSDLNodesFolder: // If you create this, I'll kill you.
                case EStandardNode.kAvatarClosetFolder:
                case EStandardNode.kAvatarOutfitFolder:
                case EStandardNode.kChronicleFolder:
                case EStandardNode.kDeviceInboxFolder:
                case EStandardNode.kGameScoresFolder:
                case EStandardNode.kGlobalInboxFolder:
                case EStandardNode.kInboxFolder:
                case EStandardNode.kPlayerInviteFolder:
                case EStandardNode.kVaultMgrGlobalDataFolder:
                    vna = new pnVaultFolderNode();
                    ((pnVaultFolderNode)vna).FolderType = type;
                    break;
                case EStandardNode.kAgeMembersFolder:
                case EStandardNode.kAgeOwnersFolder:
                case EStandardNode.kAllPlayersFolder:
                case EStandardNode.kBuddyListFolder:
                case EStandardNode.kCanVisitFolder:
                case EStandardNode.kCCRPlayersFolder: // If you create this, I'll REALLY kill you.
                case EStandardNode.kHoodMembersFolder:
                case EStandardNode.kIgnoreListFolder:
                case EStandardNode.kPeopleIKnowAboutFolder:
                    vna = new pnVaultPlayerInfoListNode();
                    ((pnVaultPlayerInfoListNode)vna).FolderType = type;
                    break;
                case EStandardNode.kAgesICanVisitFolder:
                case EStandardNode.kAgesIOwnFolder:
                case EStandardNode.kChildAgesFolder:
                case EStandardNode.kPublicAgesFolder: // If you create this, I'll resurrect you to kill you again.
                case EStandardNode.kSubAgesFolder:
                    vna = new pnVaultAgeInfoListNode();
                    ((pnVaultAgeInfoListNode)vna).FolderType = type;
                    break;
                case EStandardNode.kSystemNode:
                    vna = new pnVaultSystemNode();
                    break;
                default:
                    Warn("Tried to Create a SimpleStdNode for: " + type.ToString());
                    return new uint?();
            }

            vna.CreatorID = cIdx;
            vna.CreatorUUID = cGuid;

            try {
                ICreateNode(vna.BaseNode);
            } catch (pnDbException e) {
                Error(e, "Failed to Create a SimpleStdNode");
                return new uint?();
            }

            return new uint?(vna.NodeID);
        }

        private void ICreateNode(pnVaultNode node) {
            Dictionary<string, object> dict = node.ToDictionary();

            pnSqlInsertStatement insert = new pnSqlInsertStatement();
            foreach (KeyValuePair<string, object> kvp in dict)
                insert.AddValue(kvp.Key, kvp.Value.ToString());
            insert.Table = "Nodes";
            insert.Execute(fDb);

            node.ID = pnDatabase.LastInsert(fDb);
        }

        private bool ICreateRelationship(uint parent, uint child, uint saver) {
            pnSqlInsertStatement insert = new pnSqlInsertStatement();
            insert.AddValue("ParentIdx", parent);
            insert.AddValue("ChildIdx", child);
            insert.AddValue("SaverIdx", saver);
            insert.Table = "NodeRefs";

            try {
                insert.Execute(fDb);
                return true;
            } catch (pnDbException e) {
                Error(e, String.Format("Failed to CreateRelationship {0}->{1}", parent, child));
                return false;
            }
        }

        private uint[] IFindNode(pnVaultNode node) {
            Dictionary<string, object> dict = node.ToDictionary();

            pnSqlSelectStatement select = new pnSqlSelectStatement();
            select.AddColumn("Idx");
            foreach (KeyValuePair<string, object> kvp in dict)
                if (kvp.Key != "CreateTime" && kvp.Key != "ModifyTime") // FIXME
                    select.AddWhere(kvp.Key, kvp.Value.ToString());
            select.Limit = 500; // Match Cyan's functionality
            select.Table = "Nodes";

            IDataReader r = select.Execute(fDb);
            List<uint> nodes = new List<uint>();
            while (r.Read())
                nodes.Add((uint)r["Idx"]);
            r.Close();

            return nodes.ToArray();
        }

        private pnVaultNode IMakeNode(IDataReader r) {
            pnVaultNode node = new pnVaultNode((ENodeType)((uint)r["NodeType"]), 
                pnVaultNode.ToDateTime(Convert.ToUInt32(r["CreateTime"])),
                pnVaultNode.ToDateTime(Convert.ToUInt32(r["ModifyTime"])));

            node.CreateAgeName = r["CreateAgeName"].ToString();
            node.CreateAgeUuid = new Guid(r["CreateAgeUuid"].ToString());
            node.CreatorUuid = new Guid(r["CreatorUuid"].ToString());
            // ...

            return node;
        }
    }
}