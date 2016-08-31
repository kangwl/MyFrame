using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace Demo.DataCenter.Dapper
{
    public class Connection
    {
        private static readonly string Connstr =
            System.Configuration.ConfigurationManager.ConnectionStrings["mysqlConn"].ConnectionString;

        private static readonly string DbProviderStr =
            System.Configuration.ConfigurationManager.AppSettings["DbProvider"];


        public static IDbConnection GetOpenConnection()
        {
            var cs = Connstr;

            DbConnectionStringBuilder scsb = new SqlConnectionStringBuilder(cs)
            {
                MultipleActiveResultSets = true
            };
            IDbConnection connection = new SqlConnection(scsb.ConnectionString);
            if (DbProviderStr == "mysql")
            {
                scsb = new MySqlConnectionStringBuilder(cs)
                {
                    AllowBatch = true
                };
                connection = new MySqlConnection(scsb.ConnectionString);
            }
            connection.Open();
            return connection;
        }

        public static MySqlConnection GetClosedConnection()
        {
            var conn = new MySqlConnection(Connstr);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }



    }
}
