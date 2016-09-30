using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model
{
    public class PreLoginRequest
    {
        public PreLoginRequest()
        {
            osVersion = "Windows";
            clientVersion = "1.0";
            deviceType = "cellphone";
        }
        public string osVersion { get; set; }
        public string deviceType { get; set; }
        public string clientVersion { get; set; }
    }
}
