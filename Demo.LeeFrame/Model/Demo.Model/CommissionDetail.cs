using System;

namespace Demo.Model
{
    public class CommissionDetail 
    {
        public Guid ID { get; set; }
        public decimal Amt { get; set; } 
        public Guid UserID { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
