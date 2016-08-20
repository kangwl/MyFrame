using System;
using Demo.Model.Extend;

namespace Demo.Model
{
    public class CommissionDetail : EntityBase
    {
        public decimal Amt { get; set; } 
        public Guid UserID { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
