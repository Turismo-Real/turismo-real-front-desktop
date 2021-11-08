using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using turismo_real_controller.Controllers.Login;
using turismo_real_controller.Controllers.Usuario;

namespace turismo_real_desktop.Views.Administrador
{
    public partial class PasswordWindow : MetroWindow
    {
        private LoginController loginController;
        private UsuarioController usuarioController;
        private int usuarioId;

        public PasswordWindow(int usuarioId)
        {
            InitializeComponent();
            this.usuarioId = usuarioId;
        }

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }

        private void CancelarEdicion(object sender, RoutedEventArgs e) => Close();

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            if (newPassword.Password.Equals(repeatPassword.Password))
            {
                loginController = new LoginController();
                usuarioController = new UsuarioController();
                string currentPass = loginController.HashPassword(currentPassword.Password.ToString());
                string newPass = loginController.HashPassword(newPassword.Password.ToString());
            }
            mensajeUsuario.Text = "Las nuevas contraseñas no coinciden";
        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }
    }
}
