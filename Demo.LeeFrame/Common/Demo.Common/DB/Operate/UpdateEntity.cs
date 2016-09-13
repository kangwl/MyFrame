using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.DB.Operate
{
    public class UpdateEntity<T> where T : class, new()
    {
        public UpdateEntity(string tableName, List<string> updateFieldList, List<WhereItem> whereItems, T t)
        {
            UpdateFieldList = updateFieldList;
            WhereItems = whereItems;
            TEntity = t;
            TableName = tableName;
        }

        public string TableName { get; set; }
        public List<string> UpdateFieldList { get; set; }
        public List<WhereItem> WhereItems { get; set; }
        public T TEntity { get; set; }
    }
}
