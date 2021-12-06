using IronPdf;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.PDFbuilder;
using turismo_real_desktop.UIElements;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class ReservaFinalizada : MetroWindow
    {
        private readonly ReservaDTO _reserva;
        private readonly UsuarioDTO _cliente;
        private readonly DepartamentoDTO _depto;
        private UsuarioController usuarioController;
        private DepartamentoController departamentoController;

        public ReservaFinalizada()
        {
            InitializeComponent();
        }

        public ReservaFinalizada(ReservaDTO reserva, UsuarioDTO cliente, DepartamentoDTO depto)
        {
            _reserva = reserva;
            _cliente = cliente;
            _depto = depto;
            InitializeComponent();
            CargarDatos();
        }

        public void CargarDatos()
        {
            reservaTitle.Text = $"RESERVA {_reserva.idReserva}";
            CargarDatosReserva();
            CargarDatosCliente();
            CargarDatosDepto();
            CargarAsistentes();
            CargarServicios();
        }

        private void CargarDatosReserva()
        {
            fechaReserva.Text = _reserva.fecHoraReserva.ToString("dd-MM-yyyy | HH:mm:ss");
            desde.Text = _reserva.fecDesde.ToString("dd-MM-yyyy");
            hasta.Text = _reserva.fecHasta.ToString("dd-MM-yyyy");
            totalArriendo.Text = _reserva.GetTotalArriendo(_depto.valorDiario).ToString("C", CultureInfo.CurrentCulture);
            totalServicios.Text = _reserva.GetTotalServicios().ToString("C", CultureInfo.CurrentCulture);
            totalReserva.Text = (_reserva.GetTotalArriendo(_depto.valorDiario) + _reserva.GetTotalServicios()).ToString("C", CultureInfo.CurrentCulture);
        }

        private void CargarDatosCliente()
        {
            rut.Text = $"{_cliente.rut}-{_cliente.dv}";
            pasaporte.Text = _cliente.pasaporte;
            nombres.Text = $"{_cliente.primerNombre} {_cliente.segundoNombre}";
            apellidos.Text = $"{_cliente.apellidoPaterno} {_cliente.apellidoPaterno}";
            correo.Text = _cliente.correo;
            pais.Text = _cliente.pais;
            regionCliente.Text = _cliente.direccion.region;
            comunaCliente.Text = _cliente.direccion.comuna;
            calleCliente.Text = _cliente.direccion.calle;
            numDireccionCliente.Text = _cliente.direccion.numero;
            numDeptoCliente.Text = _cliente.direccion.depto;
            numCasaCliente.Text = _cliente.direccion.casa;
        }

        private void CargarDatosDepto()
        {
            idDepto.Text = _depto.id_departamento.ToString();
            dormitorios.Text = _depto.dormitorios.ToString();
            regionDepto.Text = _depto.direccion.region;
            numeroDireccionDepto.Text = _depto.direccion.numero;
            instalaciones.Text = new BuilderPDF().ConvertInstalaciones(_depto.instalaciones);
            descripcion.Text = _depto.descripcion;
            tipoDepto.Text = _depto.tipo;
            banios.Text = _depto.banios.ToString();
            comunaDepto.Text = _depto.direccion.comuna;
            numDepto.Text = _depto.direccion.depto;
            valorDiario.Text = _depto.valorDiario.ToString("C", CultureInfo.CurrentCulture);
            superficie.Text = $"{_depto.superficie}m2";
            calleDepto.Text = _depto.direccion.calle;
        }

        private void CargarAsistentes()
        {
            asistentesCounter.Text = $"ASISTENTES ({_reserva.asistentes.Count})";
            if (_reserva.asistentes.Count == 0)
            {
                asistentesGrid.Visibility = Visibility.Hidden;
                asistentesMsg.Visibility = Visibility.Visible;
            }
            else
            {
                asistentesGrid.ItemsSource = ConvertToAsistenteGrid(_reserva.asistentes);
                asistentesGrid.Visibility = Visibility.Visible;
                asistentesMsg.Visibility = Visibility.Hidden;
            }
        }

        private List<AsistenteGrid> ConvertToAsistenteGrid(List<AsistenteDTO> asistentes)
        {
            List<AsistenteGrid> asistentesGrid = new List<AsistenteGrid>();
            foreach (AsistenteDTO asistente in asistentes)
            {
                AsistenteGrid asistenteGrid = new AsistenteGrid();
                asistenteGrid.rut = $"{asistente.numRut}-{asistente.dvRut}";
                asistenteGrid.pasaporte = asistente.pasaporte;
                asistenteGrid.primerNombre = asistente.primerNombre;
                asistenteGrid.segundoNombre = asistente.segundoApellido;
                asistenteGrid.primerApellido = asistente.primerApellido;
                asistenteGrid.segundoApellido = asistente.segundoApellido;
                asistenteGrid.correo = asistente.correo;
                asistentesGrid.Add(asistenteGrid);
            }
            return asistentesGrid;
        }

        private void CargarServicios()
        {
            serviciosCounter.Text = $"SERVICIOS ({_reserva.servicios.Count})";
            if (_reserva.servicios.Count == 0)
            {
                serviciosGrid.Visibility = Visibility.Hidden;
                serviciosMsg.Visibility = Visibility.Visible;
            }
            else
            {
                serviciosGrid.ItemsSource = ConvertToServiciosGrid(_reserva.servicios);
                serviciosGrid.Visibility = Visibility.Visible;
                serviciosMsg.Visibility = Visibility.Hidden;
            }
        }

        public List<ServicioGrid> ConvertToServiciosGrid(List<ServicioReservaDTO> servicios)
        {
            List<ServicioGrid> serviciosGrid = new List<ServicioGrid>();
            foreach (ServicioReservaDTO servicio in servicios)
            {
                ServicioGrid servicioGrid = new ServicioGrid();
                servicioGrid.idServicio = servicio.idServicio;
                servicioGrid.tipo = servicio.tipo;
                servicioGrid.nombre = servicio.servicio;
                servicioGrid.valor = servicio.valor.ToString("C", CultureInfo.CurrentCulture);
                serviciosGrid.Add(servicioGrid);
            }
            return serviciosGrid;
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

        private void Salir(object sender, RoutedEventArgs e) => Close();

        private void OnHoverSalir(object sender, MouseEventArgs e)
        {
            btnSalir.Background = UIColors.NormalGreen;
            salirText.Foreground = UIColors.White;
        }

        private void OnLeaveSalir(object sender, MouseEventArgs e)
        {
            btnSalir.Background = UIColors.White;
            salirText.Foreground = UIColors.NormalGreen;
        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {
            btnGuardarPDF.Background = UIColors.Blue;
            guardarText.Foreground = UIColors.White;
        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {
            btnGuardarPDF.Background = UIColors.White;
            guardarText.Foreground = UIColors.Blue;
        }
    }
}
