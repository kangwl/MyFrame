using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.DB.Operate
{
    public class InsertEntity<T> where T : class, new()
    {
        public InsertEntity(string tableName, T t, List<string> insertFieldList)
        {
            TableName = tableName;
            TEntity = t;
            InsertFieldList = insertFieldList;
        }

        public string TableName { get; set; }
        public List<string> InsertFieldList { get; set; } 
        public T TEntity { get; set; }
    }
}
