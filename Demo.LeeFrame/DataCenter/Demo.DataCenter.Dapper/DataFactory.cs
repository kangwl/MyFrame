using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Demo.DataCenter.Dapper
{
    public class DataFactory : IDisposable
    {

        private static readonly string Connstr =
            System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        public static readonly string DbProviderStr =
            System.Configuration.ConfigurationManager.AppSettings["DbProvider"];

        public DBEnum DbType { get; set; }

        private IDbConnection GetOpenConnection()
        {
            DbType = DBEnum.MSSQL;
            IDbConnection connection;
            if (DbProviderStr == "mysql")
            {
                DbType = DBEnum.MySQL;
                connection = new MySqlConnection(Connstr);
            }
            else if (DbProviderStr == "mssql")
            {
                DbType = DBEnum.MSSQL;
                connection = new SqlConnection(Connstr);
            }
            else
            {
                connection = new SqlConnection(Connstr);
            }

            connection.Open();
            return connection;
        }


        private IDbConnection _connection;

        public IDbConnection DapperConn => _connection ?? (_connection = GetOpenConnection());




        public void Dispose()
        {

            if (DapperConn.State == ConnectionState.Open)
            {
                DapperConn.Close();
            }
            DapperConn.Dispose();
        }
    }

}
