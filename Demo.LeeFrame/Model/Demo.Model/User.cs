using System;
using Demo.Model.Extend;

namespace Demo.Model
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string QQ { get; set; } 
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
