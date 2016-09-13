using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.DB
{
    public class WhereItem
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 符号
        /// =
        /// >
        /// >=
        /// </summary>
        public string Signal { get; set; }
    }
}
