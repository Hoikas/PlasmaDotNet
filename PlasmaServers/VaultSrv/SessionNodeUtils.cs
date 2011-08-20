using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public partial class pnVaultSession {

        private uint ICreateNode(pnVaultNode node) {
            Dictionary<string, object> dict = node.ToDictionary();

            pnSqlInsertStatement insert = new pnSqlInsertStatement();
            foreach (KeyValuePair<string, object> kvp in dict)
                insert.AddValue(kvp.Key, kvp.Value.ToString());
            insert.Table = "Nodes";
            insert.Execute(fDb);

            return pnDatabase.LastInsert(fDb);
        }

        private pnVaultNode[] IFindNode(pnVaultNode node) {
            Dictionary<string, object> dict = node.ToDictionary();

            pnSqlSelectStatement select = new pnSqlSelectStatement();
            select.AddColumn("Idx"); // Hack to prevent us from selecting everything
            foreach (KeyValuePair<string, object> kvp in dict)
                select.AddWhere(kvp.Key, kvp.Value.ToString());
            select.Limit = 500; // Match Cyan's functionality
            select.Table = "Nodes";

            IDataReader r = select.Execute(fDb);
            List<pnVaultNode> nodes = new List<pnVaultNode>();
            while (r.Read())
                nodes.Add(IMakeNode(r));
            r.Close();

            return nodes.ToArray();
        }

        private pnVaultNode IMakeNode(IDataReader r) {
            pnVaultNode node = new pnVaultNode((ENodeType)((uint)r["NodeType"]), 
                pnVaultNode.ToDateTime(Convert.ToUInt32(r["CreateTime"])),
                pnVaultNode.ToDateTime(Convert.ToUInt32(r["ModifyTime"])));

            if (r["CreateAgeName"] != null)
                node.CreateAgeName = r["CreateAgeName"].ToString();

            // TODO: ...

            return node;
        }
    }
}