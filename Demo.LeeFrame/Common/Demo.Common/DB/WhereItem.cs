namespace Demo.Common.DB
{
    public class WhereItem
    {
        public WhereItem()
        {
            
        }

        public WhereItem(string field, string signal)
        {
            Field = field;
            Signal = signal;
        } 

        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 符号
        /// =
        /// >
        /// >=
        /// like 等等
        /// </summary>
        public string Signal { get; set; }
    }
}
