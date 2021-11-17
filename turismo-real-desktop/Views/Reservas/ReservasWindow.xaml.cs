using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
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
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Administrador;

namespace turismo_real_desktop.Views.Reservas
{

    public partial class ReservasWindow : MetroWindow
    {
        public ReservasWindow()
        {
            InitializeComponent();
        }

        public string GetSource(FrameworkElement src) => src.Name;
        private void Volver(object sender, RoutedEventArgs e) => Volver();
        private void OnHoverNuevaReserva(object sender, MouseEventArgs e) => ChangeHoverColor(btnNuevaReserva, nuevaReservaText, GetSource(e.OriginalSource as FrameworkElement), nuevaReservaIcon);
        private void OnLeaveNuevaReserva(object sender, MouseEventArgs e) => ChangeLeaveColor(btnNuevaReserva, nuevaReservaText, GetSource(e.OriginalSource as FrameworkElement), nuevaReservaIcon);
        private void OnHoverSeleccionar(object sender, MouseEventArgs e) => ChangeHoverColor(btnSeleccionar, seleccionarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveSeleccionar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnSeleccionar, seleccionarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement), volverIcon);
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement), volverIcon);
        private void OnHoverEliminar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEliminar, eliminarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveEliminar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEliminar, eliminarText, GetSource(e.OriginalSource as FrameworkElement));

        private void OpenNuevaReserva(object sender, RoutedEventArgs e)
        {

        }

        private void OpenReserva(object sender, RoutedEventArgs e)
        {

        }

        public void Volver()
        {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        private void EliminarReserva(object sender, RoutedEventArgs e)
        {

        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string source, PackIconFontAwesome icon = null)
        {
            if (source.Equals("btnSeleccionar"))
            {
                tile.Background = UIColors.Blue;
                text.Foreground = UIColors.White;
            }
            else if (source.Equals("btnNuevaReserva") || source.Equals("btnVolver"))
            {
                tile.Background = UIColors.NormalGreen;
                text.Foreground = UIColors.White;
                if (icon != null) icon.Foreground = UIColors.White;
            }
            else if (source.Equals("btnEliminar"))
            {
                tile.Background = UIColors.Red;
                text.Foreground = UIColors.White;
            }
        }

        public void ChangeLeaveColor(Tile tile, TextBlock text, string source, PackIconFontAwesome icon = null)
        {
            tile.Background = UIColors.White;
            if (source.Equals("btnSeleccionar"))
                text.Foreground = UIColors.Blue;
            else if (source.Equals("btnNuevaReserva") || source.Equals("btnVolver"))
                text.Foreground = UIColors.NormalGreen;
            if (icon != null) icon.Foreground = UIColors.NormalGreen;
            else if (source.Equals("btnEliminar"))
                text.Foreground = UIColors.Red;
        }

    }
}
