using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Plasma {
    public class pnDatabase {

        /// <summary>
        /// Connects to the database specified in PlasmaServers.ini
        /// </summary>
        /// <returns></returns>
        public static IDbConnection Connect() {
            string type = pngIni.Ini["Database.Type"];
            if (type.ToLower() == "mysql")
                return IConnectToMySQL();
            else
                throw new pnDbException("Unknown Database.Type: " + type);
        }

        private static IDbConnection IConnectToMySQL() {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Database = pngIni.Ini["Database.Name"];
            sb.Password = pngIni.Ini["Database.Password"];
            sb.Server = pngIni.Ini["Database.Host"];
            sb.UserID = pngIni.Ini["Database.User"];

            MySqlConnection conn = new MySqlConnection(sb.GetConnectionString(true));
            try {
                conn.Open();
            } catch (Exception e) {
                throw new pnDbException("Failed to connect to MySQL", e);
            }

            return conn;
        }

        /// <summary>
        /// Escapes unsafe characters in an SQL String.
        /// </summary>
        /// <param name="sql">Unsafe SQL String</param>
        /// <returns>Safe SQL String</returns>
        /// <remarks>This is technically not the right way to do this, but I hate the way ADO .NET parameters 
        /// have different prefixes. Makes supporting different DB systems in one codebase difficult.</remarks>
        public static string Escape(string sql) {
            string type = pngIni.Ini["Database.Type"];
            if (type.ToLower() == "mysql")
                return sql.Replace("'", "''");
            else
                throw new pnDbException("Unknown Database.Type: " + type);
        }

        public static uint LastInsert(IDbConnection db) {
            string type = pngIni.Ini["Database.Type"];
            IDbCommand cmd = db.CreateCommand();
            if (type.ToLower() == "mysql")
                cmd.CommandText = "SELECT LAST_INSERT_ID();";
            else
                throw new pnDbException("Unknown Database.Type: " + type);

            IDataReader r = cmd.ExecuteReader();
            if (r.Read()) {
                uint id = Convert.ToUInt32(r[0]);
                r.Close();
                return id;
            } else {
                r.Close();
                unchecked { return (uint)-1; }
            }
        }
    }

    public class pnDbException : plNetException {
        public pnDbException() { }
        public pnDbException(string message) : base(message) { }
        public pnDbException(string message, Exception inner) : base(message, inner) { }
    }
}
