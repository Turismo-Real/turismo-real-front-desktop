using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using turismo_real_controller.Controllers.Login;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.UIElements;

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

                Trace.WriteLine(currentPass);
                Trace.WriteLine(newPass);
                bool updated = usuarioController.ActualizarPassword(usuarioId, currentPass, newPass);

                if (updated)
                {
                    mensajeUsuario.Text = "Contraseña actualizada.";
                    mensajeUsuario.Foreground = UIColors.NormalGreen;
                    ClearForm();
                }
                return;
            }
            mensajeUsuario.Text = "Las nuevas contraseñas no coinciden";
            mensajeUsuario.Foreground = UIColors.DangerRed;
        }

        public void ClearForm()
        {
            currentPassword.Password = string.Empty;
            newPassword.Password = string.Empty;
            repeatPassword.Password = string.Empty;
        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }
    }
}
