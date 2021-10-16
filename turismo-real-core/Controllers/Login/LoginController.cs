using System;
using System.Collections.Generic;
using System.Text;
using turismo_real_business.Messages;
using turismo_real_controller.Hasher;

namespace turismo_real_controller.Controllers.Login
{
    public class LoginController
    {
        public LoginResponse Login(string correo, string password)
        {
            LoginResponse response = new LoginResponse();
            response.login = true;
            response.perfil = "administrador";
            return response;
        }

        public bool ValidarCorreo(string correo)
        {
            return true;
        }

        public string HashPassword(string password)
        {
            return new HashPassword().HashSHA256(password);
        }
    }
}
