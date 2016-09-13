using System.Collections.Generic;

namespace Demo.Common.DB.Operate
{
    public class PagedEntity<T> where T : class, new()
    {
        public PagedEntity(string tableName, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SelectFieldList = new List<string>();
            WhereItems = new List<WhereItem>();
            OrderByItem = new OrderByItem();
            OrderByItems = new List<OrderByItem>();
            TEntity = new T();
            TableName = tableName;
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
