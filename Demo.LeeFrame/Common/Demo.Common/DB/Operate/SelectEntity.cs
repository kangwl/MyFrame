using System.Collections.Generic;

namespace Demo.Common.DB.Operate
{
    public class SelectEntity<T> where T : class, new()
    {
        public SelectEntity(string tableName, List<string> selectFieldList, List<WhereItem> whereItems, T t)
        {
            SelectFieldList = selectFieldList;
            WhereItems = whereItems;
            TEntity = t;
            TableName = tableName;
        }

        public string TableName { get; set; }
        public List<string> SelectFieldList { get; set; }
        public List<WhereItem> WhereItems { get; set; }
        public T TEntity { get; set; }
    }
}
