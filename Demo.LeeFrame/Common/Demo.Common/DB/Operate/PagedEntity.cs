using System.Collections.Generic;

namespace Demo.Common.DB.Operate
{
    public class PagedEntity<T> where T : class, new()
    {
        public PagedEntity(string tableName, OrderByItem orderByItem, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SelectFieldList = new List<string>();
            WhereItems = new List<WhereItem>();
            OrderByItem = orderByItem;
            OrderByItems = new List<OrderByItem>();
            TEntity = new T();
            TableName = tableName;
        }

        public PagedEntity(string tableName, OrderByItem orderByItem, List<WhereItem> whereItems, T t, int pageIndex,
            int pageSize) : this(tableName, orderByItem, pageIndex, pageSize)
        {
            WhereItems = whereItems;
            TEntity = t;
        }


        public string TableName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<string> SelectFieldList { get; set; }
        public List<WhereItem> WhereItems { get; set; }
        public OrderByItem OrderByItem { get; set; }
        public List<OrderByItem> OrderByItems { get; set; }
        public T TEntity { get; set; }
    }
}
