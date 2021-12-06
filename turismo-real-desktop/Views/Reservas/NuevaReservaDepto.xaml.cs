using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_controller.Controllers.Imagen;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class NuevaReservaDepto : MetroWindow
    {
        private NuevaReservaCliente _prevWindow;
        private NuevaReservaServicios _nextWindow;
        private UsuarioDTO _cliente;
        private DepartamentoDTO _depto;
        private ReservaDTO _reserva;
        private List<DepartamentoDTO> _deptos;
        private ReservasWindow _targetWindow;
        private DepartamentoController deptoController;
        private ImagenController imagenController;

        public NuevaReservaDepto(NuevaReservaCliente previousWindow, UsuarioDTO cliente)
        {
            SetPreviousWindow(previousWindow, cliente);
            InitializeComponent();

            deptoController = new DepartamentoController();
            _deptos = deptoController.ObtenerDepartamentos();
            CargarDepartamentos(_deptos);
        }

        public void SetTargetWindow(ReservasWindow targetWindow) => _targetWindow = targetWindow;

        public void SetPreviousWindow(NuevaReservaCliente previousWindow, UsuarioDTO cliente)
        {
            _prevWindow = previousWindow;
            _cliente = cliente;
        }

        public void SetDeptoImage(DepartamentoDTO depto, string imageData, Grid parentContainer)
        {
            Image imgDepto = new Image();
            imgDepto.Name = $"imgDepto{depto.id_departamento}";
            imgDepto.Width = 350;
            imgDepto.Height = 250;
            imgDepto.Margin = new Thickness(19, 0, 0, 0);
            imgDepto.VerticalAlignment = VerticalAlignment.Center;
            imgDepto.HorizontalAlignment = HorizontalAlignment.Left;
            imgDepto.Stretch = Stretch.Fill;
            imgDepto.Source = SetBase64Image(imageData);
            parentContainer.Children.Add(imgDepto);
        }

        public BitmapImage SetBase64Image(string base64)
        {
            BitmapImage bitmap = new BitmapImage();
            try
            {
                byte[] binaryImage = Convert.FromBase64String(base64);
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(binaryImage);
                bitmap.EndInit();
                return bitmap;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                string errorImagePath = @"/Assets/errorImage.jpg";
                bitmap = new BitmapImage(new Uri(errorImagePath, UriKind.Relative));
                return bitmap;
            }
        }

        public void CargarDepartamentos(List<DepartamentoDTO> deptos)
        {
            if (deptos == null) return;
            double marginTopDeptoContainer = 20;
            double deptosGridHeight = 323;
            foreach (DepartamentoDTO depto in deptos)
            {
                Grid deptoContainer = SetDeptoContainer(depto.id_departamento, marginTopDeptoContainer);
                marginTopDeptoContainer += 300;

                deptosGrid.Height = deptosGridHeight;
                deptosGridHeight += 300;
                deptosGrid.Children.Add(deptoContainer);

                imagenController = new ImagenController();
                ImagenesDeptoDTO imagenesDepto = imagenController.ObtenerImagenes(depto.id_departamento);
                SetDeptoImage(depto, imagenesDepto.imagenes[0].imagen, deptoContainer);
                SetDeptoData(depto, deptoContainer);
                SetListBox(depto, deptoContainer);
                SetDatePicker(depto, deptoContainer);
                SetTxtBlockValorArriendo(depto, deptoContainer);
                SetSeleccionarButton(depto, deptoContainer);
            }
        }

        public void SetSeleccionarButton(DepartamentoDTO depto, Grid parentCointainer)
        {
            Button btnSeleccionar = new Button();
            btnSeleccionar.Name = $"btnSeleccionar{depto.id_departamento}";
            btnSeleccionar.HorizontalAlignment = HorizontalAlignment.Left;
            btnSeleccionar.Margin = new Thickness(1132, 220, 0, 0);
            btnSeleccionar.VerticalAlignment = VerticalAlignment.Top;
            btnSeleccionar.Height = 37;
            btnSeleccionar.Width = 141;
            btnSeleccionar.Content = "SELECCIONAR";
            btnSeleccionar.FontWeight = FontWeights.Medium;
            btnSeleccionar.Click += new RoutedEventHandler(SeleccionarDepto);
            parentCointainer.Children.Add(btnSeleccionar);
        }

        public void SetTxtBlockValorArriendo(DepartamentoDTO depto, Grid parentContainer)
        {
            string name = $"totalArriendo{depto.id_departamento}";
            string defaultMessage = "TOTAL ARRIENDO: $0";
            TextBlock txtTotal = GenerateTextBlock(name, 0, defaultMessage, 12);
            txtTotal.Margin = new Thickness(1080, 176, 0, 0);
            txtTotal.FontWeight = FontWeights.Bold;
            parentContainer.Children.Add(txtTotal);
        }

        public void SetDatePicker(DepartamentoDTO depto, Grid parentGrid)
        {
            string name = $"desdeText{depto.id_departamento}";
            TextBlock txtDesde = GenerateTextBlock(name, 0, "Desde", 12);
            txtDesde.Margin = new Thickness(1077, 80, 0, 0);

            name = $"hastaText{depto.id_departamento}";
            TextBlock txtHasta = GenerateTextBlock(name, 0, "Hasta", 12);
            txtHasta.Margin = new Thickness(1080, 126, 0, 0);

            DatePicker datePickerDesde = new DatePicker();
            datePickerDesde.Name = $"dpDesde{depto.id_departamento}";
            datePickerDesde.HorizontalAlignment = HorizontalAlignment.Left;
            datePickerDesde.VerticalAlignment = VerticalAlignment.Top;
            datePickerDesde.Margin = new Thickness(1122, 77, 0, 0);
            datePickerDesde.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(ChangeDesde);

            DatePicker datePickerHasta = new DatePicker();
            datePickerHasta.Name = $"dpHasta{depto.id_departamento}";
            datePickerHasta.HorizontalAlignment = HorizontalAlignment.Left;
            datePickerHasta.VerticalAlignment = VerticalAlignment.Top;
            datePickerHasta.Margin = new Thickness(1122, 126, 0, 0);
            datePickerHasta.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(ChangeHasta);

            DisableDates(datePickerDesde, depto.fechasReservadas);
            DisableDates(datePickerHasta, depto.fechasReservadas);

            parentGrid.Children.Add(txtDesde);
            parentGrid.Children.Add(txtHasta);
            parentGrid.Children.Add(datePickerDesde);
            parentGrid.Children.Add(datePickerHasta);
        }

        private void ChangeDesde(object sender, SelectionChangedEventArgs e) => ChangeValorArriendo(e.Source as DatePicker);
        private void ChangeHasta(object sender, SelectionChangedEventArgs e) => ChangeValorArriendo(e.Source as DatePicker);

        private void ChangeValorArriendo(DatePicker source)
        {
            // OBTENTER PARENT TARGET OBJECT
            Grid parentTarget = source.Parent as Grid;
            // OBTENER DATEPICKER HASTA
            DatePicker datepickerHasta = parentTarget.Children[11] as DatePicker;
            // OBTENER ID DEPARTAMENTO
            int depto_id = Convert.ToInt32(source.Name.Substring(source.Name.Length - 1, 1));

            bool isDesde = source.Name.ToUpper().Contains("DESDE");
            DatePicker siblingDatePicker = isDesde ? siblingDatePicker = parentTarget.Children[11] as DatePicker : parentTarget.Children[10] as DatePicker;

            if (source.SelectedDate != null && siblingDatePicker.SelectedDate != null)
            {
                // OBTENER DEPARTAMENTO
                DepartamentoDTO selectedDepto = _deptos.Find(x => x.id_departamento == depto_id);
                // OBTENER FECHAS DE ARRIENDO
                DateTime fechaDesde = Convert.ToDateTime(source.SelectedDate);
                DateTime fechaHasta = Convert.ToDateTime(siblingDatePicker.SelectedDate);
                // OBTENER DIAS DE ARRIENDO
                int diasArriendo = Convert.ToInt32(Math.Abs((fechaHasta - fechaDesde).TotalDays));
                // OBTENER TEXTBLOCK VALOR ARRIENDO
                TextBlock valorArriendo = parentTarget.Children[12] as TextBlock;
                valorArriendo.Text = $"TOTAL ARRIENDO: {(selectedDepto.valorDiario * diasArriendo).ToString("C", CultureInfo.CurrentCulture)}";
            }
        }

        private void DisableDates(DatePicker datepicker, List<FechaReservadaDTO> fechasReservadas)
        {
            foreach (FechaReservadaDTO fechaReservada in fechasReservadas)
            {
                CalendarDateRange calendarRange = new CalendarDateRange(fechaReservada.desde, fechaReservada.hasta);
                datepicker.BlackoutDates.Add(calendarRange);
            }
            CalendarDateRange disablePastDays = new CalendarDateRange(DateTime.MinValue, DateTime.Now.AddDays(-1));
            datepicker.BlackoutDates.Add(disablePastDays);
        }

        public void SetListBox(DepartamentoDTO depto, Grid parentGrid)
        {
            string name = $"instalacionesText{depto.id_departamento}";
            TextBlock instalacionesTitle = GenerateTextBlock(name, 43, "Instalaciones", 12);
            instalacionesTitle.Margin = new Thickness(933, 43, 0, 0);

            ListBox instalacionesList = new ListBox();
            instalacionesList.Name = $"listbox{depto.id_departamento}";
            instalacionesList.Margin = new Thickness(879, 64, 244, 28);
            instalacionesList.Background = Brushes.Beige;
            instalacionesList.ItemsSource = depto.instalaciones;

            parentGrid.Children.Add(instalacionesTitle);
            parentGrid.Children.Add(instalacionesList);
        }

        public void SetDeptoData(DepartamentoDTO depto, Grid parentContainer)
        {
            string name = $"tipoText{depto.id_departamento}";
            double marginTop = 20;
            string message = depto.tipo.ToUpper();
            TextBlock tipo = GenerateTextBlock(name, marginTop, message, 16);

            name = $"datosText{depto.id_departamento}";
            marginTop = 51;
            message = $"{depto.superficie}m2 | {depto.dormitorios} dormitorios | {depto.banios} baños";
            TextBlock datos = GenerateTextBlock(name, marginTop, message, 12); ;

            name = $"comunaRegionText{depto.id_departamento}";
            message = $"{depto.direccion.comuna}, Región {depto.direccion.region}";
            TextBlock comunaRegion = GenerateTextBlock(name, 77, message, 12);
            comunaRegion.Width = 638;

            name = $"descripcionText{depto.id_departamento}";
            message = depto.descripcion;
            TextBlock descripcion = GenerateTextBlock(name, 103, message, 12);
            descripcion.Width = 474;

            name = $"valorText{depto.id_departamento}";
            message = $"Valor Diario: {depto.valorDiario.ToString("C", CultureInfo.CurrentCulture)}";
            TextBlock valor = GenerateTextBlock(name, 247, message, 14);
            valor.FontWeight = FontWeights.Bold;

            parentContainer.Children.Add(tipo);
            parentContainer.Children.Add(datos);
            parentContainer.Children.Add(comunaRegion);
            parentContainer.Children.Add(descripcion);
            parentContainer.Children.Add(valor);
        }

        public TextBlock GenerateTextBlock(string name, double mTop, string message, double fontSize)
        {
            TextBlock textblock = new TextBlock();
            textblock.Name = name;
            textblock.HorizontalAlignment = HorizontalAlignment.Left;
            textblock.Margin = new Thickness(397, mTop, 0, 0);
            textblock.Text = message;
            textblock.TextWrapping = TextWrapping.Wrap;
            textblock.VerticalAlignment = VerticalAlignment.Top;
            textblock.FontSize = fontSize;
            return textblock;
        }

        public Grid SetDeptoContainer(int id, double marginTop)
        {
            Grid deptoContainer = new Grid();
            deptoContainer.Name = $"departamento{id}";
            deptoContainer.Background = Brushes.Beige;
            deptoContainer.Margin = new Thickness(20, marginTop, 20, 0);
            deptoContainer.Height = 283;
            deptoContainer.VerticalAlignment = VerticalAlignment.Top;
            return deptoContainer;
        }

        private void BackToCliente(object sender, RoutedEventArgs e)
        {
            _prevWindow.SetNextWindow(this);
            _prevWindow.Show();
            Hide();
        }

        public void SetNextWindow(NuevaReservaServicios nextWindow) => this._nextWindow = nextWindow;

        private void SeleccionarDepto(object sender, RoutedEventArgs e)
        {
            Button source = e.Source as Button;
            int depto_id = Convert.ToInt32(source.Name.Substring(source.Name.Length - 1, 1));
            _depto = _deptos.Find(x => x.id_departamento == depto_id);
            _reserva = new ReservaDTO();
            _reserva.idUsuario = _cliente.idUsuario;
            _reserva.idDepartamento = _depto.id_departamento;

            Grid parent = source.Parent as Grid;
            DatePicker desde = parent.Children[10] as DatePicker;
            DatePicker hasta = parent.Children[11] as DatePicker;
            _reserva.fecDesde = Convert.ToDateTime(desde.SelectedDate);
            _reserva.fecHasta = Convert.ToDateTime(hasta.SelectedDate);
            _reserva.valorArriendo = _depto.valorDiario * _reserva.GetDiasArriendo();

            if (_nextWindow != null)
            {
                _nextWindow.SetPreviousWindow(this, _reserva, _cliente, _depto);
                _nextWindow.Show();
            }

            if (_nextWindow == null)
            {
                NuevaReservaServicios reservaServiciosWin = new NuevaReservaServicios(this, _reserva, _cliente, _depto);
                _nextWindow = reservaServiciosWin;
                reservaServiciosWin.SetTargetWindow(_targetWindow);
                reservaServiciosWin.Show();
            }
            Hide();
        }
    }
}
