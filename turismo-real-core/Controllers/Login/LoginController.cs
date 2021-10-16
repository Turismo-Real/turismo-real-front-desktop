using turismo_real_business.Messages;
using turismo_real_controller.Hasher;
using turismo_real_controller.Validates;
using turismo_real_services.REST.Login;

namespace turismo_real_controller.Controllers.Login
{
    public class LoginController
    {
        public LoginResponse Login(string correo, string password)
        {
            LoginService loginService = new LoginService();
            LoginResponse response = loginService.CallService(correo, password);
            return response;
        }

        public bool ValidarCorreo(string correo)
        {
            return new Validator().EmailValidator(correo);
        }

        public string HashPassword(string password)
        {
            return new HashPassword().HashSHA256(password);
        }

        public bool validarCampoVacio(string content)
        {
            return new Validator().ValidateEmptyString(content);
        }
    }
}
