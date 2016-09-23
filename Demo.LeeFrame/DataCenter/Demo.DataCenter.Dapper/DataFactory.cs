using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Demo.DataCenter.Dapper
{
    public class DataFactory : IDisposable
    {

        public DataFactory()
        {
           // DbType = DBEnum.MSSQL;
            if (DbProviderStr == "mysql")
            {
                DbType = DBEnum.MySQL;
            }
            else if (DbProviderStr == "mssql")
            {
                DbType = DBEnum.MSSQL;
            }
        }

        private static readonly string Connstr =
            System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        public static readonly string DbProviderStr =
            System.Configuration.ConfigurationManager.AppSettings["DbProvider"];

        public DBEnum DbType { get; set; }

        private IDbConnection GetOpenConnection()
        {
            IDbConnection connection;

            switch (DbType)
            {
                case DBEnum.MSSQL:
                    connection = new SqlConnection(Connstr);
                    break;
                case DBEnum.MySQL:
                    connection = new MySqlConnection(Connstr);
                    break;
                default:
                    connection = new SqlConnection(Connstr);
                    break;
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
            DapperConn?.Dispose();
        }
    }

}
