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
using turismo_real_business.DTOs;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class NuevaReservaDepto : MetroWindow
    {
        private NuevaReservaCliente previousWindow;
        private NuevaReservaServicios nextWindow;
        private readonly UsuarioDTO reservationOwner;

        public NuevaReservaDepto(NuevaReservaCliente previousWindow, UsuarioDTO reservationOwner)
        {
            this.previousWindow = previousWindow;
            this.reservationOwner = reservationOwner;
            InitializeComponent();
        }

        private void BackToCliente(object sender, RoutedEventArgs e)
        {
            previousWindow.SetNextWindow(this);
            previousWindow.Show();
            Hide();
        }

        private void StepServicios(object sender, RoutedEventArgs e)
        {
            if (nextWindow != null) nextWindow.Show();

            if (nextWindow == null)
            {
                NuevaReservaServicios reservaServicioWin = new NuevaReservaServicios(this);
                reservaServicioWin.Show();

            }
            Hide();
        }

        public void SetNextWindow(NuevaReservaServicios nextWindow) => this.nextWindow = nextWindow;

    }
}
