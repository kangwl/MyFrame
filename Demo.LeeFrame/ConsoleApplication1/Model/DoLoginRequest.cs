using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model
{
    public class DoLoginRequest
    {
        public string password { get; set; }
        public string imageCode { get; set; }

        public string imageSsid { get; set; }
    }
}

