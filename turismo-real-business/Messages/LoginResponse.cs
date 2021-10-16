using System;

namespace turismo_real_business.Messages
{
    public class LoginResponse
    {
        public LoginResponse() { }

        public LoginResponse(string mensaje, bool login, string tipo)
        {
            this.mensaje = mensaje;
            this.login = login;
            this.tipo = tipo;
        }

        public string mensaje { get; set; }
        public bool login { get; set; }
        public string tipo { get; set; }
    }
}
