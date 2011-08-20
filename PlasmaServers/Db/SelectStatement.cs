using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pnSqlSelectStatement {

        uint fLimit;
        List<string> fSelects = new List<string>();
        string fTable;
        Dictionary<string, string> fWhere = new Dictionary<string, string>();

        /// <summary>
        /// Maximum number of rows to return.
        /// </summary>
        public uint Limit {
            get { return fLimit; }
            set { fLimit = value; }
        }

        /// <summary>
        /// The Database table that we should SELECT FROM
        /// </summary>
        public string Table {
            get { return fTable; }
            set { fTable = value; }
        }

        /// <summary>
        /// Adds a column to select.
        /// </summary>
        /// <param name="column">Column name</param>
        /// <remarks>If no columns have been added to the list, we will select all columns.</remarks>
        public void AddColumn(string column) {
            fSelects.Add(column);
        }

        /// <summary>
        /// Adds a where clause to the statement.
        /// </summary>
        /// <param name="column">Column Name</param>
        /// <param name="value">Desired Value</param>
        /// <remarks>All provided where clauses will be combined with AND. OR is not supported.</remarks>
        public void AddWhere(string column, object value) {
            fWhere.Add(column, value.ToString());
        }

        public IDataReader Execute(IDbConnection db) {
            IDbCommand cmd = db.CreateCommand();
            cmd.CommandText = ToString();

            try {
                return cmd.ExecuteReader();
            } catch (Exception e) {
                // Rethrow a more friendly exception
                throw new pnDbException("SELECT failed", e);
            }
        }

        public override string ToString() {
            if (fTable == null)
                throw new pnDbException("Failed to generate SQL", new ArgumentNullException("Table Name"));

            string type = pngIni.Ini["Database.Type"];
            if (type.ToLower() == "mysql")
                try {
                    return IBuildForMySQL();
                } catch (Exception e) {
                    throw new pnDbException("Failed to generate SQL", e);
                } else
                throw new pnDbException("Unknown Database.Type: " + type);
        }

        private string IBuildForMySQL() {
            string cmd = "SELECT ";

            if (fSelects.Count == 0)
                cmd += "* ";
            else
                for (int i = 0; i < fSelects.Count; i++) {
                    cmd += fSelects[i];
                    if (fSelects.Count == (i + 1))
                        cmd += " ";
                    else
                        cmd += ", ";
                }

            cmd += String.Format("FROM {0} ", fTable);

            if (fWhere.Count != 0) {
                cmd += "WHERE ";

                bool needAnd = false;
                foreach (KeyValuePair<string, string> kvp in fWhere) {
                    if (needAnd)
                        cmd += "AND ";
                    else
                        needAnd = true;
                    cmd += String.Format("`{0}` = '{1}' ", kvp.Key, pnDatabase.Escape(kvp.Value));
                }
            }

            if (fLimit != 0)
                cmd += "LIMIT " + fLimit.ToString();
            return cmd;
        }
    }
}
