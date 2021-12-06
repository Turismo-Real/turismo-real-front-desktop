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
        private ReservasWindow _targetWindow;
        private UsuarioController usuarioController;
        private NuevaReservaDepto _nextWindow;
        private UsuarioDTO _cliente;

        public NuevaReservaCliente(ReservasWindow targetWindow)
        {
            _targetWindow = targetWindow;
            InitializeComponent();
        }

        private void Salir(object sender, RoutedEventArgs e) => Close();

        private void StepDepto(object sender, RoutedEventArgs e) => BuscarUsuario(txtRutPasaporte.Text);

        public void BuscarUsuario(string rutPasaporte)
        {
            usuarioController = new UsuarioController();
            _cliente = usuarioController.ExisteUsuario(rutPasaporte); ;

            if (_cliente != null)
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
            if (_nextWindow != null)
            {
                _nextWindow.SetPreviousWindow(this, _cliente);
                _nextWindow.Show();
            }

            if (_nextWindow == null)
            {
                NuevaReservaDepto reservaDeptoWin = new NuevaReservaDepto(this, _cliente);
                reservaDeptoWin.SetTargetWindow(_targetWindow);
                reservaDeptoWin.Show();
            }
            Hide();
        }

        public void SetNextWindow(NuevaReservaDepto nextWindow)
        {
            this._nextWindow = nextWindow;
        }

        private void ActiveLabel(object sender, MouseEventArgs e) => lblNuevoCliente.Foreground = UIColors.NormalGreen;


        private void DeactiveLabel(object sender, MouseEventArgs e) => lblNuevoCliente.Foreground = UIColors.NormalBlack;

        private void OpenNuevoCliente(object sender, MouseButtonEventArgs e)
        {
            NuevoUsuario nuevoUsuarioWin = new NuevoUsuario(this);
            nuevoUsuarioWin.ShowDialog();
        }

        public void SetReservationOwner(UsuarioDTO owner) => _cliente = owner;
    }
}
