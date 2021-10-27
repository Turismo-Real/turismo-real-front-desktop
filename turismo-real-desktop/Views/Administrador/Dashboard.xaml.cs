using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using turismo_real_business.DTOs;
using turismo_real_business.Messages;
using turismo_real_business.Singleton;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.UIElements;
using System.Windows;
using turismo_real_desktop.Views.Extra;
using turismo_real_desktop.Views.Administrador.Departamentos;

namespace turismo_real_desktop.Views.Administrador
{
    public partial class Dashboard : MetroWindow
    {
        public Dashboard()
        {
            InitializeComponent();
            WelcomeMessage();
        }

        public Dashboard(LoginResponse login)
        {
            InitializeComponent();

            if(login.id != 0) // Para ingresar en modo offline
            {
                // Obtener datos del usuario
                UsuarioController usuarioController = new UsuarioController();
                UsuarioDTO usuario = usuarioController.GetUsuario(login.id);

                // Setear usuario logueado
                LoguedUser.SetLoguedUser(usuario);
                WelcomeMessage();
            }
        }

        // BUILDINGS EVENTS BUTTON
        private void OnHoverBuilding(object sender, MouseEventArgs e)
        {
            ChangeHoverColor(buildingTile, buildingIcon, buildingText);
        }

        private void OnLeaveBuilding(object sender, MouseEventArgs e)
        {
            ChangeLeaveColor(buildingTile, buildingIcon, buildingText);
        }

        // USERS EVENTS BUTTON
        private void OnHoverUsers(object sender, MouseEventArgs e)
        {
            ChangeHoverColor(usersTile, usersIcon, usersText);
        }

        private void OnLeaveUsers(object sender, MouseEventArgs e)
        {
            ChangeLeaveColor(usersTile, usersIcon, usersText);
        }

        // PROFILE EVENTS BUTTON
        private void OnHoverProfile(object sender, MouseEventArgs e)
        {
            ChangeHoverColor(profileTile, profileIcon, profileText);
        }

        private void OnLeaveProfile(object sender, MouseEventArgs e)
        {
            ChangeLeaveColor(profileTile, profileIcon, profileText);
        }

        // EXIT EVENTS BUTTON
        private void OnHoverExit(object sender, MouseEventArgs e)
        {
            exitTile.Background = (Brush)new BrushConverter().ConvertFrom("#FFDC1F1F");
            exitIcon.Foreground = UIColors.White;
            exitText.Foreground = UIColors.White;
        }

        private void OnLeaveExit(object sender, MouseEventArgs e)
        {
            exitTile.Background = UIColors.White;
            exitIcon.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC1F1F");
            exitText.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFDC1F1F");
        }


        // SERVICES EVENTS BUTTON
        private void OnHoverServices(object sender, MouseEventArgs e)
        {
            ChangeHoverColor(servicesTile, servicesIcon, servicesText);
        }

        private void OnLeaveServices(object sender, MouseEventArgs e)
        {
            ChangeLeaveColor(servicesTile, servicesIcon, servicesText);
        }

        // RESERVATION EVENTS BUTTON
        private void OnHoverReservation(object sender, MouseEventArgs e)
        {
            ChangeHoverColor(reservationTile, reservationIcon, reservationText);
        }

        private void OnLeaveReservation(object sender, MouseEventArgs e)
        {
            ChangeLeaveColor(reservationTile, reservationIcon, reservationText);
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, PackIconFontAwesome icon, TextBlock text)
        {
            tile.Background = UIColors.NormalGreen;
            icon.Foreground = UIColors.White;
            text.Foreground = UIColors.White;
        }

        public void ChangeLeaveColor(Tile tile, PackIconFontAwesome icon, TextBlock text)
        {
            tile.Background = UIColors.White;
            icon.Foreground = UIColors.NormalGreen;
            text.Foreground = UIColors.NormalGreen;
        }

        // CLICK EVENTS
        private void Logout(object sender, RoutedEventArgs e)
        {
            LoguedUser.SetLoguedUser(null);
            Login login = new Login();
            login.Show();
            Close();
        }

        private void OpenWinDeptos(object sender, RoutedEventArgs e)
        {
            Deptos deptosWindow = new Deptos();
            deptosWindow.Show();
            Close();
        }

        public void WelcomeMessage()
        {
            string nombreUsuario = $"{LoguedUser.GetLoguedUser().primerNombre} " +
                    $"{LoguedUser.GetLoguedUser().primerApellido} " +
                    $"{LoguedUser.GetLoguedUser().segundoApellido}";

            // Bienvenida a usuario
            lblWelcome.Content = $"Bienvenido {nombreUsuario}";
        }
    }
}
