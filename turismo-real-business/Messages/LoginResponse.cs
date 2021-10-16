using System;
using System.Collections.Generic;
using System.Text;

namespace turismo_real_business.Messages
{
    public class LoginResponse
    {
        public string message { get; set; }
        public bool login { get; set; }
        public string perfil { get; set; }
    }
}
