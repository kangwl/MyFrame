using System.Collections.Generic;

namespace Demo.Common.DB.Operate
{
    public class DeleteEntity<T> where T : class, new()
    {
        public DeleteEntity(string tableName, List<WhereItem> whereItems, T t)
        {
            WhereItems = whereItems;
            TEntity = t;
            TableName = tableName;
        }

        public string TableName { get; set; }
        public List<WhereItem> WhereItems { get; set; }
        public T TEntity { get; set; }
    }

}
