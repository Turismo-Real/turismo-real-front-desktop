using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Reserva;
using turismo_real_desktop.GridEntities;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class NuevaReservaAsistente : MetroWindow
    {
        private readonly NuevaReservaServicios _previousWindow;
        private ReservaDTO _reserva;
        private ReservaController reservaController;

        public NuevaReservaAsistente(ReservaDTO reserva, NuevaReservaServicios previousWindow)
        {
            _reserva = reserva;
            _previousWindow = previousWindow;
            InitializeComponent();
            SetContadorAsistentes(0);
        }

        public void SetContadorAsistentes(int asistentes) => contadorAsistentes.Text = $"Total Asistentes: {asistentes}";

        private void BackToServicios(object sender, RoutedEventArgs e)
        {
            _previousWindow.SetNextWindow(this);
            _previousWindow.Show();
            Hide();
        }

        private void FinalizarReserva(object sender, RoutedEventArgs e)
        {
            if (_reserva.asistentes == null) _reserva.asistentes = new List<AsistenteDTO>();
            reservaController = new ReservaController();
            ReservaDTO saved = reservaController.NuevaReserva(_reserva);

            if (saved != null)
            {
                ReservaFinalizada finalizadaWin = new ReservaFinalizada(saved);
                finalizadaWin.Show();
                Close();
                return;
            }
            string title = "";
            string message = "";
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
