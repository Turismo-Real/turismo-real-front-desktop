using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using turismo_real_desktop.UIElements;

namespace turismo_real_desktop.Views.Extra
{
    public partial class RecoverPassword : MetroWindow
    {
        public RecoverPassword()
        {
            InitializeComponent();
        }

        private void OpenLoginWin(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            Close();
        }

        private void RequestPasswdChange(object sender, RoutedEventArgs e)
        {
            changePasswdFormGrid.Visibility = Visibility.Hidden;
            changePasswdSent.Visibility = Visibility.Visible;
        }

        private void OnHoverSolicitar(object sender, MouseEventArgs e) => ChangeHoverColor(btnSolicitar, solicitarText, "HOVERGREEN");
        private void OnLeaveSolicitar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnSolicitar, solicitarText, "NORMALGREEN");
        
        private void OnHoverVolver(object sender, MouseEventArgs e)
        {
            if (changePasswdFormGrid.IsVisible) ChangeHoverColor(btnVolver, volverText, "NORMALGREEN");
            else ChangeHoverColor(btnVolverLogin, volverTxt, "NORMALGREEN");
        }

        private void OnLeaveVolver(object sender, MouseEventArgs e)
        {
            if (changePasswdFormGrid.IsVisible) ChangeLeaveColor(btnVolver, volverText, "WHITE");
            else ChangeLeaveColor(btnVolverLogin, volverTxt, "WHITE");
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string color)
        {
            // NORMALGREEN - HOVERGREEN
            if (color.Equals("NORMALGREEN"))
            {
                tile.Background = UIColors.NormalGreen;
                tile.BorderBrush = UIColors.NormalGreen;
            }

            if (color.Equals("HOVERGREEN"))
            {
                tile.Background = UIColors.HoverGreen;
                tile.BorderBrush = UIColors.HoverGreen;
            }
            text.Foreground = UIColors.White;
        }

        public void ChangeLeaveColor(Tile tile, TextBlock text, string color)
        {
            // NORMALGREEN - WHITE
            if (color.Equals("NORMALGREEN"))
            {
                tile.Background = UIColors.NormalGreen;
                tile.BorderBrush = UIColors.NormalGreen;
                text.Foreground = UIColors.White;
            }

            if (color.Equals("WHITE"))
            {
                tile.Background = UIColors.White;
                tile.BorderBrush = UIColors.NormalGreen;
                text.Foreground = UIColors.NormalGreen;
            }
        }

    }
}
