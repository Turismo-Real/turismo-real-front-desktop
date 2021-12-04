using IronPdf;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_controller.Controllers.Usuario;

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

            string pdf_template = Properties.Resources.nuevaReservaPDF.ToString();
            usuarioController = new UsuarioController();
            departamentoController = new DepartamentoController();
            UsuarioDTO usuario = usuarioController.ObtenerUsuario(reserva.idUsuario);
            DepartamentoDTO depto = departamentoController.ObtenerDepartamento(reserva.idDepartamento);
            pdf_template = SetPDFdata(pdf_template, reserva, usuario, depto);

            if (saveFile.ShowDialog() == true)
            {
                HtmlToPdf htmltopdf = new HtmlToPdf();
                htmltopdf.PrintOptions.MarginLeft = 10;
                htmltopdf.PrintOptions.MarginTop = 10;
                htmltopdf.PrintOptions.MarginRight = 10;
                htmltopdf.PrintOptions.MarginBottom = 10;
                PdfDocument pdfdoc = await htmltopdf.RenderHtmlAsPdfAsync(pdf_template);

                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    stream.Write(pdfdoc.Stream.ToArray());
                    stream.Close();
                }
            }
        }

        private string SetPDFdata(string html, ReservaDTO reserva, UsuarioDTO cliente, DepartamentoDTO depto)
        {
            html = html.Replace("@IDRESERVA", reserva.idReserva.ToString());
            html = html.Replace("@FECHARESERVA", reserva.fecHoraReserva.ToString("dd-MM-yyyy | HH:mm:ss"));
            html = html.Replace("@FECHADESDE", reserva.fecDesde.ToString("dd-MM-yyyy"));
            html = html.Replace("@FECHAHASTA", reserva.fecHasta.ToString("dd-MM-yyyy"));
            html = html.Replace("@NUMRUT", $"{cliente.rut}-{cliente.dv}");
            html = html.Replace("@PASAPORTE", cliente.pasaporte);
            html = html.Replace("@CORREO", cliente.correo);
            html = html.Replace("@NOMBRESCLIENTE", $"{cliente.primerNombre} {cliente.apellidoPaterno}");
            html = html.Replace("@APELLIDOSCLIENTE", $"{cliente.apellidoPaterno} {cliente.apellidoMaterno}");
            html = html.Replace("@PAISCLIENTE", cliente.pais);
            html = html.Replace("@REGIONCLIENTE", cliente.direccion.region);
            html = html.Replace("@COMUNACLIENTE", cliente.direccion.comuna);
            html = html.Replace("@CALLECLIENTE", cliente.direccion.calle);
            html = html.Replace("@NUMEROCLIENTE", cliente.direccion.numero);
            html = html.Replace("@NUMDEPTOCLIENTE", cliente.direccion.depto);
            html = html.Replace("@NUMCASACLIENTE", cliente.direccion.calle);
            html = html.Replace("@IDDEPTO", depto.id_departamento.ToString());
            html = html.Replace("@TIPODEPTO", depto.tipo);
            html = html.Replace("@VALORDIARIODEPTO", depto.valorDiario.ToString("C", CultureInfo.CurrentCulture));
            html = html.Replace("@DORMITORIOS", depto.dormitorios.ToString());
            html = html.Replace("@BANIOS", depto.banios.ToString());
            html = html.Replace("@SUPERFICIE", depto.superficie.ToString());
            html = html.Replace("@REGIONDEPTO", depto.direccion.region);
            html = html.Replace("@COMUNADEPTO", depto.direccion.comuna);
            html = html.Replace("@CALLEDEPTO", depto.direccion.calle);
            html = html.Replace("@NUMERODEPTO", depto.direccion.numero);
            html = html.Replace("@NUMDEPTO", depto.direccion.depto);
            html = html.Replace("@DESCRIPCIONDEPTO", depto.descripcion);
            html = html.Replace("@INSTALACIONES", ConvertInstalaciones(depto.instalaciones));
            html = html.Replace("@CANTIDADSERVICIOS", reserva.servicios.Count.ToString());
            html = html.Replace("@CANTIDADASISTENTES", reserva.asistentes.Count.ToString());
            html = html.Replace("@SERVICIOS", SetServiciosTable(reserva.servicios, reserva.GetTotalServicios()));
            html = html.Replace("@ASISTENTES", SetAsistentesTable(reserva.asistentes));
            html = html.Replace("@TOTALARRIENDO", reserva.GetTotalArriendo(depto.valorDiario).ToString("C", CultureInfo.CurrentCulture));
            html = html.Replace("@TOTALSERVICIOS", reserva.GetTotalServicios().ToString("C", CultureInfo.CurrentCulture));
            html = html.Replace("@TOTALRESERVA", "999");
            return html;
        }

        private string SetServiciosTable(List<ServicioReservaDTO> servicios, double totalServicios)
        {
            if (servicios.Count == 0) return "Aun no se han agregado servicios.";

            string serviciosTable = "<table class=\"servicios-table\">" +
                "<tr><th class=\"table-header\">ID</th><th class=\"table-header\">SERVICIO</th>" +
                "<th class=\"table-header\">TIPO</th><th class=\"table-header\">VALOR</th></tr>";
            
            foreach(ServicioReservaDTO servicio in servicios)
            {
                serviciosTable += $"<tr><td class=\"table-data\">{servicio.idServicio}</td>" +
                    $"<td class=\"table-data\">{servicio.servicio}</td><td class=\"table-data\">{servicio.tipo}</td>" +
                    $"<td class=\"table-data\">{servicio.valor.ToString("C", CultureInfo.CurrentCulture)}</td></tr>";
            }
            serviciosTable += "<tr><td colspan=\"3\" class=\"total-servicios\">TOTAL</td>" +
                $"<td class=\"total table-data\">{totalServicios.ToString("C", CultureInfo.CurrentCulture)}</td></tr></table>";
            return serviciosTable;
        }

        private string SetAsistentesTable(List<AsistenteDTO> asistentes)
        {
            if (asistentes.Count == 0) return "No se han agregado asistentes.";

            string asistentesTable = "<table class=\"servicios-table\"><tr><th>RUT</th>" +
                "<th>PASAPORTE</th><th>NOMBRE</th><th>CORREO</th></tr>";

            foreach (AsistenteDTO asistente in asistentes)
            {
                asistentesTable += $"<tr><td class=\"table-data\">{asistente.numRut}-{asistente.dvRut}</td>" +
                    $"<td class=\"table-data\">{asistente.pasaporte}</td><td class=\"table-data\">{ShortenName(asistente)}</td>" +
                    $"<td class=\"table-data\">enzo.norese @gmail.com</td></tr>";
            }
            asistentesTable += "</table>";
            return asistentesTable;
        }

        public string ShortenName(AsistenteDTO asistente)
        {
            string name = $"{asistente.primerNombre} {asistente.segundoNombre.Substring(0,1)}. {asistente.primerApellido} {asistente.segundoApellido}";
            return name;
        }

        private string ConvertInstalaciones(List<string> instalaciones)
        {
            string contactInstalaciones = string.Empty;
            bool first = true;
            foreach (string instalacion in instalaciones)
            {
                if (first) contactInstalaciones = instalacion; first = false;
                contactInstalaciones += $", {instalacion}";
            }
            contactInstalaciones += ".";
            return contactInstalaciones;
        }
    }
}
