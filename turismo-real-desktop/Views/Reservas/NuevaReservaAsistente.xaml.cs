using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Reserva;
using turismo_real_desktop.GridEntities;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class NuevaReservaAsistente : MetroWindow
    {
        private NuevaReservaServicios _prevWindow;
        private ReservaDTO _reserva;
        private UsuarioDTO _cliente;
        private DepartamentoDTO _depto;
        private ReservasWindow _targetWindow;
        private ReservaController reservaController;

        public NuevaReservaAsistente(NuevaReservaServicios previousWindow, ReservaDTO reserva, UsuarioDTO cliente, DepartamentoDTO depto)
        {
            SetPreviousWindow(previousWindow, reserva, cliente, depto);
            InitializeComponent();
            SetContadorAsistentes(0);
        }

        public void SetTargetWindow(ReservasWindow targetWindow) => _targetWindow = targetWindow;

        public void SetPreviousWindow(NuevaReservaServicios previousWindow, ReservaDTO reserva, UsuarioDTO cliente, DepartamentoDTO depto)
        {
            _prevWindow = previousWindow;
            _reserva = reserva;
            _cliente = cliente;
            _depto = depto;
        }

        public void SetContadorAsistentes(int asistentes) => contadorAsistentes.Text = $"Total Asistentes: {asistentes}";

        private void BackToServicios(object sender, RoutedEventArgs e)
        {
            _prevWindow.SetNextWindow(this);
            _prevWindow.Show();
            Hide();
        }

        private void FinalizarReserva(object sender, RoutedEventArgs e)
        {
            if (_reserva.asistentes == null) _reserva.asistentes = new List<AsistenteDTO>();
            reservaController = new ReservaController();
            ReservaDTO savedReserva = reservaController.NuevaReserva(_reserva);

            if (savedReserva != null)
            {
                ReservaFinalizada finalizadaWin = new ReservaFinalizada(savedReserva, _cliente, _depto);
                finalizadaWin.Show();
                _targetWindow.FillDataGridReservas();
                Close();
                return;
            }
            string title = "Error al agregar reserva.";
            string message = "Lo sentimos, ha ocurrido un error al intentar ingresar la reserva.";
            MessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OpenAgregarAsistente(object sender, RoutedEventArgs e)
        {
            AgregarAsistenteReserva agregarAsistenteWin = new AgregarAsistenteReserva(this);
            agregarAsistenteWin.ShowDialog();
        }

        public void AgregarAsistente(AsistenteDTO asistente)
        {
            if (_reserva.asistentes == null) _reserva.asistentes = new List<AsistenteDTO>();
            _reserva.asistentes.Add(asistente);
            asistentesGrid.ItemsSource = ConvertToAsistenteGrid(_reserva.asistentes);
            SetContadorAsistentes(_reserva.asistentes.Count);
        }

        public List<AsistenteGrid> ConvertToAsistenteGrid(List<AsistenteDTO> asistentes)
        {
            List<AsistenteGrid> asistentesGrid = new List<AsistenteGrid>();
            foreach (AsistenteDTO asistente in asistentes)
            {
                AsistenteGrid asistenteGrid = new AsistenteGrid();
                asistenteGrid.pasaporte = asistente.pasaporte;
                asistenteGrid.rut = $"{asistente.numRut}-{asistente.dvRut}";
                asistenteGrid.primerNombre = asistente.primerNombre;
                asistenteGrid.segundoNombre = asistente.segundoNombre;
                asistenteGrid.primerApellido = asistente.primerApellido;
                asistenteGrid.segundoApellido = asistente.segundoApellido;
                asistenteGrid.correo = asistente.correo;
                asistentesGrid.Add(asistenteGrid);
            }
            return asistentesGrid;
        }

        private void QuitarAsistente(object sender, RoutedEventArgs e)
        {
            if (asistentesGrid.SelectedItem == null) return;
            AsistenteGrid selectedAsistente = asistentesGrid.SelectedItem as AsistenteGrid;
            AsistenteDTO asistente = _reserva.asistentes.Find(x => x.pasaporte.Equals(selectedAsistente.pasaporte) || $"{x.numRut}-{x.dvRut}".Equals(selectedAsistente.rut));
            if (asistente != null) _reserva.asistentes.Remove(asistente);
            asistentesGrid.ItemsSource = ConvertToAsistenteGrid(_reserva.asistentes);
            SetContadorAsistentes(_reserva.asistentes.Count);
        }
    }
}
