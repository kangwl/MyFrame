using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Demo.Common;
using Demo.Common.Redis;

namespace WindowsService.Demo
{
    public partial class ServiceDemo : ServiceBase
    {
        public ServiceDemo()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            RedisHelper.Subscribe<Order>("order", SuscribebBack);
        }

        private void SuscribebBack(Order order)
        {
            NLogHelper.Debug("SuscribebBack:" + order.ID.ToString());
        }


        public class Order
        {
            public Guid ID { get; set; }
            public decimal Money { get; set; } 
        }

        protected override void OnStop()
        {
            RedisHelper.UnSubscribeAll();
        }
    }
}
