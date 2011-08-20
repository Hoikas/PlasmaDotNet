using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Plasma {
    public enum pnColumnOption {
        None,
        AutoIncrement,
        Nullable,
        VariableSize,
    }

    public class pnSqlCreateTable {

        struct ColumnDetail {
            public string fName;
            public Type fType;
            public pnColumnOption fOption;
        }

        List<ColumnDetail> fColumns = new List<ColumnDetail>();

        private string fName;

        /// <summary>
        /// Gets or sets the name of the table we should create
        /// </summary>
        public string Name {
            get { return fName; }
            set { fName = value; }
        }

        private string fPrimaryKey;

        /// <summary>
        /// Gets or sets the name of the primary key for this table
        /// </summary>
        public string PrimaryKey {
            get { return fPrimaryKey; }
            set { fPrimaryKey = value; }
        }

        public void AddColumn(string name, Type type) { AddColumn(name, type, pnColumnOption.None); }
        public void AddColumn(string name, Type type, pnColumnOption opt) {
            ColumnDetail cd = new ColumnDetail();
            cd.fName = name;
            cd.fOption = opt;
            cd.fType = type;
            fColumns.Add(cd);
        }

        /// <summary>
        /// Executes the prepared SQL Statement
        /// </summary>
        /// <param name="db">Database to send the query to</param>
        public void Execute(IDbConnection db) {
            IDbCommand cmd = db.CreateCommand();
            cmd.CommandText = ToString();
            
            try {
                cmd.ExecuteNonQuery();
            } catch (Exception e) {
                // Rethrow a more friendly exception
                throw new pnDbException("Failed to create Db Table", e);
            }

            cmd.Dispose();
        }

        /// <summary>
        /// Prepares an SQL Statement for the Database specified in PlasmaServers.ini
        /// </summary>
        /// <returns>SQL Statement</returns>
        public override string ToString() {
            if (fName == null)
                throw new pnDbException("Failed to generate SQL", new ArgumentNullException("Table Name"));

            string type = pngIni.Ini["Database.Type"];
            if (type.ToLower() == "mysql")
                try {
                    return IBuildForMySQL();
                } catch (Exception e) {
                    throw new pnDbException("Failed to generate SQL", e);
                }
            else
                throw new pnDbException("Unknown Database.Type: " + type);
        }

        private string IBuildForMySQL() {
            string cmd = String.Format("CREATE TABLE IF NOT EXISTS `{0}` (\n", fName);
            for (int i = 0; i < fColumns.Count; i++) {
                ColumnDetail cd = fColumns[i];
                string options = "";
                string def = "";

                // The types we support. Better not use anything else.
                if (cd.fType == typeof(uint)) {
                    options += "INT unsigned ";
                    def = "0";
                } else if (cd.fType == typeof(int)) {
                    options += "INT ";
                    def = "0";
                } else if (cd.fType == typeof(sbyte)) {
                    options += "TINYINT ";
                    def = "0";
                } else if (cd.fType == typeof(string)) {
                    if (cd.fOption == pnColumnOption.VariableSize)
                        options += "VARCHAR(64) ";
                    else
                        options += "TEXT ";
                } else if (cd.fType == typeof(OpenSSL.Hash)) {
                    options += "CHAR(40) ";
                } else if (cd.fType == typeof(Guid)) {
                    options += "CHAR(36) ";
                    def = Guid.Empty.ToString();
                } else
                    throw new ArgumentException("Invalid table data type: " + cd.fType.ToString());

                if (cd.fOption == pnColumnOption.AutoIncrement)
                    options += "NOT NULL AUTO_INCREMENT";
                else if (cd.fOption == pnColumnOption.Nullable)
                    options += "DEFAULT NULL";
                else
                    options += String.Format("NOT NULL DEFAULT '{0}'", def);

                cmd += String.Format("  `{0}` {1}", cd.fName, options);
                
                // Decide if we need a comma
                if (fColumns.Count == (i+1) && fPrimaryKey == null)
                    cmd += "\n";
                else
                    cmd += ",\n";
            }

            if (fPrimaryKey != null)
                cmd += String.Format("  PRIMARY KEY (`{0}`)\n", fPrimaryKey);

            // TODO: Storage Engines are pretty important in MySQL
            //       The Nodes and NodeRefs tables should *probably* be InnoDB (fast node saves/adds)
            //       Accounts and Players should probably be MyISAM because they're inserted into rarely
            cmd += ") DEFAULT CHARSET=utf8 ;";
            return cmd;
        }
    }
}
