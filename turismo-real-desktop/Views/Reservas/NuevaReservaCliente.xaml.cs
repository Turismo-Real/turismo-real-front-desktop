using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using turismo_real_controller.Controllers.Reserva;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Usuarios;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class NuevaReservaCliente : MetroWindow
    {
        private ReservaController reservaController;
        private UsuarioController usuarioController;
        private NuevaReservaDepto nextWindow;
        private UsuarioDTO reservationOwner;

        public NuevaReservaCliente()
        {
            InitializeComponent();
        }

        private void Salir(object sender, RoutedEventArgs e) => Close();

        private void StepDepto(object sender, RoutedEventArgs e) => BuscarUsuario(txtRutPasaporte.Text);

        public void BuscarUsuario(string rutPasaporte)
        {
            usuarioController = new UsuarioController();
            reservationOwner = usuarioController.ExisteUsuario(rutPasaporte); ;

            if (reservationOwner != null)
            {
                ShowReservaDeptoWindow();
                ChangeVisibilityNotFoundUserAlert(Visibility.Hidden);
            }
            else
            {
                ChangeVisibilityNotFoundUserAlert(Visibility.Visible);
            }
        }

        public void ChangeVisibilityNotFoundUserAlert(Visibility visibility)
        {
            tbTituloAlerta.Visibility = visibility;
            tbMensajeAlerta.Visibility = visibility;
            AlertGrid.Visibility = visibility;
        }

        private void ShowReservaDeptoWindow()
        {
            if (nextWindow != null) nextWindow.Show();

            if (nextWindow == null)
            {
                NuevaReservaDepto reservaDeptoWin = new NuevaReservaDepto(this);
                reservaDeptoWin.Show();
            }
            Hide();
        }

        public void SetNextWindow(NuevaReservaDepto nextWindow)
        {
            this.nextWindow = nextWindow;
        }

        private void ActiveLabel(object sender, MouseEventArgs e) => lblNuevoCliente.Foreground = UIColors.NormalGreen;


        private void DeactiveLabel(object sender, MouseEventArgs e) => lblNuevoCliente.Foreground = UIColors.NormalBlack;

        private void OpenNuevoCliente(object sender, MouseButtonEventArgs e)
        {
            NuevoUsuario nuevoUsuarioWin = new NuevoUsuario(this);
            nuevoUsuarioWin.ShowDialog();
        }

        public void SetReservationOwner(UsuarioDTO owner) => reservationOwner = owner;

        public void PrintNuevoUsuario()
        {
            Trace.WriteLine(reservationOwner.idUsuario);
            Trace.WriteLine(reservationOwner.primerNombre);
        }
    }
}
