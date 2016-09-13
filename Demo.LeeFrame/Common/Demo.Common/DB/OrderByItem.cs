using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.DB
{
    public class OrderByItem
    {
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
