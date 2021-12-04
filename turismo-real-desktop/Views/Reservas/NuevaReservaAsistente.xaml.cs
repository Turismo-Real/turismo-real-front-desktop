using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Reserva;
using turismo_real_desktop.GridEntities;
using IronPdf;

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
                GenerarPDF(saved);
                ReservaFinalizada finalizadaWin = new ReservaFinalizada(saved);
                finalizadaWin.Show();
                Close();
                return;
            }
            string title = "";
            string message = "";
            MessageBox.Show(title, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public async void GenerarPDF(ReservaDTO reserva)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = $"turismoreal_reserva_{reserva.idReserva}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.pdf";

            string pdf_template = Properties.Resources.nuevaReservaPDF.ToString();
            pdf_template = SetVariables(pdf_template);
            Trace.WriteLine(pdf_template);

            if (saveFile.ShowDialog() == true)
            {
                HtmlToPdf htmltopdf = new HtmlToPdf();
                PdfDocument pdfdoc = await htmltopdf.RenderHtmlAsPdfAsync(pdf_template);
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    stream.Write(pdfdoc.Stream.ToArray());
                    stream.Close();
                }
            }
        }

        private string SetVariables(string html)
        {
            html = html.Replace("@IDRESERVA", "20");
            html = html.Replace("@PASAPORTE", "11111111111");
            html = html.Replace("@NUMRUT", "19406080-7");
            return html;
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
