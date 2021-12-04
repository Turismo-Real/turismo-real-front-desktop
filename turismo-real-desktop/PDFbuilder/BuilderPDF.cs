using IronPdf;
using System.Collections.Generic;
using System.Globalization;
using turismo_real_business.DTOs;

namespace turismo_real_desktop.PDFbuilder
{
    public class BuilderPDF
    {
        public string pdfTemplate = string.Empty;

        public BuilderPDF(string pdfTemplate) => this.pdfTemplate = pdfTemplate;

        public HtmlToPdf GetInstanceHTMLtoPDF(double margins)
        {
            HtmlToPdf htmlToPDF = new HtmlToPdf();
            htmlToPDF.PrintOptions.MarginLeft = margins;
            htmlToPDF.PrintOptions.MarginTop = margins;
            htmlToPDF.PrintOptions.MarginRight = margins;
            htmlToPDF.PrintOptions.MarginBottom = margins;
            return htmlToPDF;
        }

        public void SetPDFdata(ReservaDTO reserva, UsuarioDTO cliente, DepartamentoDTO depto)
        {
            pdfTemplate = pdfTemplate.Replace("@IDRESERVA", reserva.idReserva.ToString());
            pdfTemplate = pdfTemplate.Replace("@FECHARESERVA", reserva.fecHoraReserva.ToString("dd-MM-yyyy | HH:mm:ss"));
            pdfTemplate = pdfTemplate.Replace("@FECHADESDE", reserva.fecDesde.ToString("dd-MM-yyyy"));
            pdfTemplate = pdfTemplate.Replace("@FECHAHASTA", reserva.fecHasta.ToString("dd-MM-yyyy"));
            pdfTemplate = pdfTemplate.Replace("@NUMRUT", $"{cliente.rut}-{cliente.dv}");
            pdfTemplate = pdfTemplate.Replace("@PASAPORTE", cliente.pasaporte);
            pdfTemplate = pdfTemplate.Replace("@CORREO", cliente.correo);
            pdfTemplate = pdfTemplate.Replace("@NOMBRESCLIENTE", $"{cliente.primerNombre} {cliente.segundoNombre}");
            pdfTemplate = pdfTemplate.Replace("@APELLIDOSCLIENTE", $"{cliente.apellidoPaterno} {cliente.apellidoMaterno}");
            pdfTemplate = pdfTemplate.Replace("@PAISCLIENTE", cliente.pais);
            pdfTemplate = pdfTemplate.Replace("@REGIONCLIENTE", cliente.direccion.region);
            pdfTemplate = pdfTemplate.Replace("@COMUNACLIENTE", cliente.direccion.comuna);
            pdfTemplate = pdfTemplate.Replace("@CALLECLIENTE", cliente.direccion.calle);
            pdfTemplate = pdfTemplate.Replace("@NUMEROCLIENTE", cliente.direccion.numero);
            pdfTemplate = pdfTemplate.Replace("@NUMDEPTOCLIENTE", cliente.direccion.depto);
            pdfTemplate = pdfTemplate.Replace("@NUMCASACLIENTE", cliente.direccion.casa);
            pdfTemplate = pdfTemplate.Replace("@IDDEPTO", depto.id_departamento.ToString());
            pdfTemplate = pdfTemplate.Replace("@TIPODEPTO", depto.tipo);
            pdfTemplate = pdfTemplate.Replace("@VALORDIARIODEPTO", depto.valorDiario.ToString("C", CultureInfo.CurrentCulture));
            pdfTemplate = pdfTemplate.Replace("@DORMITORIOS", depto.dormitorios.ToString());
            pdfTemplate = pdfTemplate.Replace("@BANIOS", depto.banios.ToString());
            pdfTemplate = pdfTemplate.Replace("@SUPERFICIE", depto.superficie.ToString());
            pdfTemplate = pdfTemplate.Replace("@REGIONDEPTO", depto.direccion.region);
            pdfTemplate = pdfTemplate.Replace("@COMUNADEPTO", depto.direccion.comuna);
            pdfTemplate = pdfTemplate.Replace("@CALLEDEPTO", depto.direccion.calle);
            pdfTemplate = pdfTemplate.Replace("@NUMERODEPTO", depto.direccion.numero);
            pdfTemplate = pdfTemplate.Replace("@NUMDEPTO", depto.direccion.depto);
            pdfTemplate = pdfTemplate.Replace("@DESCRIPCIONDEPTO", depto.descripcion);
            pdfTemplate = pdfTemplate.Replace("@INSTALACIONES", ConvertInstalaciones(depto.instalaciones));
            pdfTemplate = pdfTemplate.Replace("@CANTIDADSERVICIOS", reserva.servicios.Count.ToString());
            pdfTemplate = pdfTemplate.Replace("@CANTIDADASISTENTES", reserva.asistentes.Count.ToString());
            pdfTemplate = pdfTemplate.Replace("@SERVICIOS", SetServiciosTable(reserva.servicios, reserva.GetTotalServicios()));
            pdfTemplate = pdfTemplate.Replace("@ASISTENTES", SetAsistentesTable(reserva.asistentes));
            pdfTemplate = pdfTemplate.Replace("@TOTALARRIENDO", reserva.GetTotalArriendo(depto.valorDiario).ToString("C", CultureInfo.CurrentCulture));
            pdfTemplate = pdfTemplate.Replace("@TOTALSERVICIOS", reserva.GetTotalServicios().ToString("C", CultureInfo.CurrentCulture));
            pdfTemplate = pdfTemplate.Replace("@TOTALRESERVA", (reserva.GetTotalArriendo(depto.valorDiario) + reserva.GetTotalServicios()).ToString("C", CultureInfo.CurrentCulture));
        }

        private string SetServiciosTable(List<ServicioReservaDTO> servicios, double totalServicios)
        {
            if (servicios.Count == 0) return "Aun no se han agregado servicios.";

            string serviciosTable = "<table class=\"servicios-table\">" +
                "<tr><th class=\"table-header\">ID</th><th class=\"table-header\">SERVICIO</th>" +
                "<th class=\"table-header\">TIPO</th><th class=\"table-header\">VALOR</th></tr>";

            foreach (ServicioReservaDTO servicio in servicios)
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
                    $"<td class=\"table-data\">{asistente.correo}</td></tr>";
            }
            asistentesTable += "</table>";
            return asistentesTable;
        }

        public string ShortenName(AsistenteDTO asistente)
        {
            string name = $"{asistente.primerNombre} {asistente.segundoNombre.Substring(0, 1)}. {asistente.primerApellido} {asistente.segundoApellido}";
            return name;
        }

        private string ConvertInstalaciones(List<string> instalaciones)
        {
            string contactInstalaciones = string.Empty;
            bool first = true;
            foreach (string instalacion in instalaciones)
            {
                if (first) contactInstalaciones = instalacion;
                else contactInstalaciones += $", {instalacion}";
                first = false;
            }
            contactInstalaciones += ".";
            return contactInstalaciones;
        }
    }
}
