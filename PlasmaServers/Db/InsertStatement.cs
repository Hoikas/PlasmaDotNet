using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public class pnSqlInsertStatement {

        Dictionary<string, string> fStuff = new Dictionary<string, string>();
        string fTable;

        /// <summary>
        /// The Database table that we should Insert into
        /// </summary>
        public string Table {
            get { return fTable; }
            set { fTable = value; }
        }

        public void AddValue(string column, object value) {
            fStuff.Add(column, value.ToString());
        }

        public void Execute(IDbConnection db) {
            IDbCommand cmd = db.CreateCommand();
            cmd.CommandText = ToString();

            try {
                cmd.ExecuteNonQuery();
            } catch (Exception e) {
                // Rethrow a more friendly exception
                throw new pnDbException("INSERT failed", e);
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
            string sql = String.Format("INSERT INTO {0} SET", fTable);

            bool prependComma = false;
            foreach (KeyValuePair<string, string> kvp in fStuff) {
                if (prependComma)
                    sql += ",";
                else
                    prependComma = true;

                sql += String.Format(" `{0}` = '{1}'", kvp.Key, pnDatabase.Escape(kvp.Value));
            }

            return sql;
        }
    }
}
