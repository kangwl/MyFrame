using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model
{
    public class User 
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string QQ { get; set; } 
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
