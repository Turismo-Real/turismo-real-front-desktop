using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using turismo_real_desktop.UIElements;

namespace turismo_real_desktop.Views.Usuarios
{

    public partial class UsuarioMessage : MetroWindow
    {
        public UsuarioMessage(string defaultPassword)
        {
            InitializeComponent();
            lblPass.Text = defaultPassword;
        }

        private void OnHoverAceptar(object sender, MouseEventArgs e)
        {
            btnAceptar.Background = UIColors.NormalGreen;
            aceptarText.Foreground = UIColors.White;
        }

        private void OnLeaveAceptar(object sender, MouseEventArgs e)
        {
            btnAceptar.Background = UIColors.White;
            aceptarText.Foreground = UIColors.NormalGreen;
        }

        private void ClickAceptar(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
