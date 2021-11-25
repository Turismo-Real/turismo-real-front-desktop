using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Reserva;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Administrador;

namespace turismo_real_desktop.Views.Reservas
{

    public partial class ReservasWindow : MetroWindow
    {
        private ReservaController reservaController;

        public ReservasWindow()
        {
            InitializeComponent();
            FillDataGridReservas();
        }

        public void FillDataGridReservas()
        {
            reservaController = new ReservaController();
            List<ReservaDTO> reservas = reservaController.ObtenerReservas();
            reservasDataGrid.ItemsSource = ConvertToReservaGrid(reservas);
            reservasCounter.Text = $"Total reservas: {reservas.Count}";
        }

        public List<ReservaGrid> ConvertToReservaGrid(List<ReservaDTO> reservas)
        {
            List<ReservaGrid> reservasGrid = new List<ReservaGrid>();
            foreach (ReservaDTO reserva in reservas)
            {
                ReservaGrid reservaGrid = new ReservaGrid();
                reservaGrid.id = reserva.idReserva;
                reservaGrid.usuario = reserva.idUsuario;
                reservaGrid.departamento = reserva.idDepartamento;
                reservaGrid.desde = reserva.fecDesde.ToString("dd/MM/yyyy");
                reservaGrid.hasta = reserva.fecHasta.ToString("dd/MM/yyyy"); ;
                reservaGrid.estado = reserva.estadoReserva;
                reservaGrid.servicios = reserva.servicios.Count;
                reservaGrid.asistentes = reserva.asistentes.Count;
                reservasGrid.Add(reservaGrid);
            }
            return reservasGrid;
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
            NuevaReserva nuevaReservaWin = new NuevaReserva();
            nuevaReservaWin.Show();
        }

        private void OpenReserva(object sender, RoutedEventArgs e)
        {
            if (reservasDataGrid.SelectedItem == null) return;
            Reserva reservaWin = new Reserva();
            reservaWin.Show();
        }

        public void Volver()
        {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        private void EliminarReserva(object sender, RoutedEventArgs e)
        {
            if (reservasDataGrid.SelectedItem == null) return;
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
