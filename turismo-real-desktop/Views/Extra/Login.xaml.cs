using System;
using System.Windows;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_business.Messages;
using turismo_real_controller.Controllers.Login;
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Administrador;

namespace turismo_real_desktop.Views.Extra
{
    public partial class Login : Window
    {
        protected LoginController loginController;
        protected string perfilAutorizado = "Administrador";

        public Login()
        {
            InitializeComponent();
            loginController = new LoginController();
        }

        private void OpenRecoverPasswdWin(object sender, MouseButtonEventArgs e)
        {
            string message = "Para poder registrarte en el sistema ponte en contacto con un administrador que ingrese tus datos.";
            MessageBox.Show(message, "Contacta con un administrador" ,MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ActiveLabel(object sender, MouseEventArgs e)
        {
            lblForgotPasswd.Foreground = UIColors.NormalGreen;
        }

        private void DeactiveLabel(object sender, MouseEventArgs e)
        {
            lblForgotPasswd.Foreground = UIColors.NormalBlack;
        }

        private void OnHoverLogin(object sender, MouseEventArgs e)
        {
            btnLogin.Background = UIColors.HoverGreen;
        }

        private void OpenRecoverPassWin(object sender, RoutedEventArgs e)
        {
            RecoverPassword recoverPasswdWin = new RecoverPassword();
            recoverPasswdWin.Show();
            Close();
        }

        private void LoginUser(object sender, RoutedEventArgs e)
        {
            // Validar campos vacios
            if(!loginController.validarCampoVacio(txtEmail.Text) || !loginController.validarCampoVacio(txtPasswd.Password.ToString()))
            {
                string message = "Se deben llenar todos los campos.";
                lblAlert = UIMessages.InfoMessage(lblAlert, message, UIColors.DangerRed);
                return;
            }

            LoginDTO loginObject = new LoginDTO();
            // validar formato de correo
            loginObject.email = txtEmail.Text;
            if (!loginController.ValidarCorreo(loginObject))
            {
                string message = "Formato de correo incorrecto.";
                lblAlert = UIMessages.InfoMessage(lblAlert, message, UIColors.DangerRed);
                return;
            }
            
            loginObject.password = loginController.HashPassword(txtPasswd.Password.ToString());
            LoginResponse loginResponse = loginController.Login(loginObject);

            // validar credenciales
            if (!loginResponse.login)
            {
                string message = "Credenciales Incorrectas.";
                lblAlert = UIMessages.InfoMessage(lblAlert, message, UIColors.DangerRed);
                return;
            }

            // validar perfil
            if (loginResponse.tipo.ToUpper().CompareTo(perfilAutorizado.ToUpper()) != 0)
            {
                string message = "Usuario No Autorizado.";
                lblAlert = UIMessages.InfoMessage(lblAlert, message, UIColors.DangerRed);
                return;
            }

            Dashboard dashboard = new Dashboard(loginObject);
            dashboard.Show();
            Close();
        }

    }
}
