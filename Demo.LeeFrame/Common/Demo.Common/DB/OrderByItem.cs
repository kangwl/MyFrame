namespace Demo.Common.DB
{
    public class OrderByItem
    {
        public OrderByItem()
        {

        }

        public OrderByItem(string orderByField, bool asc)
        {
            OrderByFiled = orderByField;
            Asc = asc;
        }

        /// <summary>
        /// order by 的字段
        /// </summary>
        public string OrderByFiled { get; set; }

        /// <summary>
        /// 是否升序
        /// </summary>
        public bool Asc { get; set; }
    }
}
