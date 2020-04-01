using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.Models
{
    public class ServiceLoginResult
    {
        public int MessageCode { get; set; }
        public string Message { get; set; }
        public TokenWrapper Result { get; set; }
    }

    public class TokenWrapper
    {
        public string Token { get; set; }
    }
}
