using System.Threading.Tasks;
using turismo_real_business.DTOs;
using turismo_real_business.Messages;
using turismo_real_controller.Hasher;
using turismo_real_controller.Validates;
using turismo_real_services.REST.Login;

namespace turismo_real_controller.Controllers.Login
{
    public class LoginController
    {
        public LoginResponse Login(LoginDTO login)
        {
            LoginResponse response = new LoginService().CallService(login);
            return response;
        }

        public bool ValidarCorreo(LoginDTO login)
        {
            return new Validator().EmailValidator(login.email);
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
