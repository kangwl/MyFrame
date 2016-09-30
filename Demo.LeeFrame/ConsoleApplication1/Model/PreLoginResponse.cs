using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model
{
    public class PreLoginResponse
    {
        public bool needAuthCode { get; set; }
        public Authcode authCode { get; set; }
    }
}
