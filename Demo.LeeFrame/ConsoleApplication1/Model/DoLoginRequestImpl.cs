using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model
{
    public class DoLoginRequestImpl
    {
        public DoLoginRequestImpl()
        {
            
        }
        public string username { get; set; }
        public string token { get; set; }

        public string functionName { get; set; }
        public string uuid { get; set; }
        public DoLoginRequest request { get; set; }
    }
}
