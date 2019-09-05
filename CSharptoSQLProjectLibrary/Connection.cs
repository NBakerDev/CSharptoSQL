using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharptoSQLProjectLibrary {

    public class Connection {

        public SqlConnection _Connection { get; set; } = null;

        public void Open() {
            this._Connection.Open();
            if (this._Connection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection Did Not Open!");
            }
        }

        public void Close() {
            if(this._Connection.State != System.Data.ConnectionState.Open) {
                return;
            }
            this._Connection.Close();
        }

        public Connection(string server, string database) {
            var connStr = $"server={server};database={database};trusted_connection=true;";
            this._Connection = new SqlConnection(connStr);
           
            }
        }
    }

