using IronPdf;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.PDFbuilder;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class ReservaFinalizada : MetroWindow
    {
        private readonly ReservaDTO _reserva;
        private UsuarioController usuarioController;
        private DepartamentoController departamentoController;

        public ReservaFinalizada()
        {
            InitializeComponent();
        }

        public ReservaFinalizada(ReservaDTO reserva)
        {
            _reserva = reserva;
            InitializeComponent();
        }

        private void GuardarPDF(object sender, RoutedEventArgs e) => GenerarPDF(_reserva);
        
        public async void GenerarPDF(ReservaDTO reserva)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = $"comprobante_reserva_tr_{reserva.idReserva}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.pdf";

            BuilderPDF builderPDF = new BuilderPDF(Properties.Resources.nuevaReservaPDF.ToString());
            usuarioController = new UsuarioController();
            departamentoController = new DepartamentoController();
            UsuarioDTO usuario = usuarioController.ObtenerUsuario(reserva.idUsuario);
            DepartamentoDTO depto = departamentoController.ObtenerDepartamento(reserva.idDepartamento);
            builderPDF.SetPDFdata(reserva, usuario, depto);

            if (saveFile.ShowDialog() == true)
            {
                HtmlToPdf htmlToPDF = builderPDF.GetInstanceHTMLtoPDF(10);
                PdfDocument pdfDoc = await htmlToPDF.RenderHtmlAsPdfAsync(builderPDF.pdfTemplate);

                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    stream.Write(pdfDoc.Stream.ToArray());
                    stream.Close();
                }
            }
        }

        
    }
}
