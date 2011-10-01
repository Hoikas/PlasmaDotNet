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

            if (cIdx != 0)
                vna.CreatorID = cIdx;
            if (cGuid != Guid.Empty)
                vna.CreatorUuid = cGuid;

            try {
                ICreateNode(vna.BaseNode);
            } catch (pnDbException e) {
                Error(e, "Failed to Create a SimpleStdNode");
                return new uint?();
            }

            return new uint?(vna.NodeID);
        }

        private void ICreateNode(pnVaultNode node) {
            pnSqlInsertStatement insert = new pnSqlInsertStatement();
            pnVaultNodeFields allFields = node.Fields;
            for (ulong bit = 1; bit != 0 && bit <= (ulong)allFields; bit <<= 1) {
                pnVaultNodeFields thisField = allFields & (pnVaultNodeFields)bit;
                if ((int)thisField == 0) continue;

                // Some basic sanity checks...
                switch (thisField) {
                    case pnVaultNodeFields.CreateTime:
                        // I don't care what we were told, it was created NOW
                        insert.AddValue("CreateTime", DateTime.UtcNow);
                        continue;
                    case pnVaultNodeFields.ModifyTime:
                        // I don't care what we were told, it was last modified NOW
                        insert.AddValue("ModifyTime", DateTime.UtcNow);
                        continue;
                    case pnVaultNodeFields.NodeIdx:
                        // Impossible.
                        continue;
                }

                string colName = thisField.ToString();
                insert.AddValue(colName, node[thisField]);
            }

            // We absolutely must have a correct Create/Modify time
            if (node[pnVaultNodeFields.CreateTime] == null)
                insert.AddValue("CreateTime", DateTime.UtcNow);
            if (node[pnVaultNodeFields.ModifyTime] == null)
                insert.AddValue("ModifyTime", DateTime.UtcNow);

            insert.Table = "Nodes";
            insert.Execute(fDb);
            node.NodeID = pnDatabase.LastInsert(fDb);
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
            pnSqlSelectStatement select = new pnSqlSelectStatement();
            select.AddColumn("NodeIdx");
            pnVaultNodeFields allFields = node.Fields;
            for (ulong bit = 1; bit != 0 && bit <= (ulong)allFields; bit <<= 1) {
                pnVaultNodeFields thisField = allFields & (pnVaultNodeFields)bit;
                if ((int)thisField == 0) continue;

                string colName = thisField.ToString();
                select.AddWhere(colName, node[thisField]);
            }

            select.Limit = 500; // Match Cyan's functionality
            select.Table = "Nodes";

            IDataReader r = select.Execute(fDb);
            List<uint> nodes = new List<uint>();
            while (r.Read())
                nodes.Add((uint)r["NodeIdx"]);
            r.Close();

            return nodes.ToArray();
        }

        private pnVaultNode IMakeNode(IDataReader r) {
            pnVaultNodeFields[] fields = (pnVaultNodeFields[])Enum.GetValues(typeof(pnVaultNodeFields));
            pnVaultNode node = new pnVaultNode();
            foreach (pnVaultNodeFields f in fields) {
                string name = f.ToString();
                try {
                    if (r[name] != null && r[name].ToString() != String.Empty)
                        node[f] = r[name];
                } catch { continue; }
            }

            return node;
        }
    }
}