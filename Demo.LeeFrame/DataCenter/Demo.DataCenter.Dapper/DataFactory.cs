using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Demo.DataCenter.Dapper
{
    public class DataFactory : IDisposable
    {
        public string TableName { get; set; }
        public DataFactory(string tableName)
        {
            TableName = tableName;
        }

        private IDbConnection _connection;

        public IDbConnection connection => _connection ?? (_connection = Connection.GetOpenConnection());

        public T GetOne<T>(Guid id)
        {
            T t = connection.Query<T>($"select * from {TableName} where ID=@id", new {id = id}).First();
            return t;
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

}
