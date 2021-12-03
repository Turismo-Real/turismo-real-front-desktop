using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Servicio;

namespace turismo_real_desktop.Views.Reservas
{
    public partial class NuevaReservaServicios : MetroWindow
    {
        private readonly NuevaReservaDepto previousWindow;
        private NuevaReservaAsistente nextWindow;
        private ReservaDTO _reserva;
        private ServicioController servicioController;
        private List<ServicioDTO> _servicios;
        protected int seleccionados;

        public NuevaReservaServicios(NuevaReservaDepto previousWindow, ReservaDTO reserva)
        {
            this.previousWindow = previousWindow;
            _reserva = reserva;
            seleccionados = 0;
            InitializeComponent();
            CargarServicios();
            SetDefaultValues(_reserva);
        }

        private void SetDefaultValues(ReservaDTO reserva)
        {
            SetCantidadServicios(0);
            SetValorArriendo(reserva.valorArriendo);
            SetTotalServicios(0);
            SetTotalReserva(reserva.valorArriendo);
        }

        private void SetTotalReserva(double totalArriendo) => tbTotalReserva.Text = $"Total: {totalArriendo.ToString("C", CultureInfo.CurrentCulture)}";
        private void SetTotalServicios(double total) => tbTotalServicios.Text = $"Servicios: {total.ToString("C", CultureInfo.CurrentCulture)}";
        private void SetValorArriendo(double valor) => tbTotalArriendo.Text = $"Arriendo: {valor.ToString("C", CultureInfo.CurrentCulture)}";
        private void SetCantidadServicios(int cantidad) => tbCantidadServicios.Text = $"Servicios Seleccionados: {cantidad}";

        public void CargarServicios()
        {
            servicioController = new ServicioController();
            _servicios = servicioController.ObtenerServicios();

            int mainContainerHeight = 180;
            int startMarginTop = 20;
            foreach (ServicioDTO servicio in _servicios)
            {
                Grid servicioContainer = CreateServicioContainer(servicio.idServicio, startMarginTop);
                startMarginTop += 280;
                SetServicioInfo(servicio, servicioContainer);
                SetAgregarButton(servicio, servicioContainer);

                serviciosGrid.Height += mainContainerHeight;
                serviciosGrid.Children.Add(servicioContainer);
            }
        }

        private void SetAgregarButton(ServicioDTO servicio, Grid parent)
        {
            Button btnAgregarQuitar = new Button();
            btnAgregarQuitar.Name = $"btnAgregarQuitar{servicio.idServicio}";
            btnAgregarQuitar.Content = "AGREGAR";
            btnAgregarQuitar.HorizontalAlignment = HorizontalAlignment.Left;
            btnAgregarQuitar.Margin = new Thickness(434,200,0,0);
            btnAgregarQuitar.VerticalAlignment = VerticalAlignment.Top;
            btnAgregarQuitar.Width = 128;
            btnAgregarQuitar.Height = 28;
            btnAgregarQuitar.Click += new RoutedEventHandler(AgregarQuitarServicio);
            parent.Children.Add(btnAgregarQuitar);
        }

        private void SetServicioInfo(ServicioDTO servicio, Grid parent)
        {
            string name;
            string text;
            Thickness margin;

            name = $"tbTipo{servicio.idServicio}";
            text = servicio.tipo;
            margin = new Thickness(60, 17, 0, 0);
            TextBlock tipoServicio = CreateTextBlock(name, text, 18, FontWeights.Bold, margin, VerticalAlignment.Top);

            name = $"tbNombre{servicio.idServicio}";
            text = servicio.nombre;
            margin = new Thickness(60, 50, 0, 0);
            TextBlock nombreServicio = CreateTextBlock(name, text, 14, FontWeights.Normal, margin, VerticalAlignment.Top);

            name = $"tbDescripion{servicio.idServicio}";
            margin = new Thickness(60, 0, 0, 55);
            TextBlock descripcionServicio = CreateTextBlock(name, text, 13, FontWeights.Normal, margin, VerticalAlignment.Bottom);
            descripcionServicio.Width = 502;
            descripcionServicio.Height = 119;

            name = $"tbValor{servicio.idServicio}";
            text = $"Valor: {servicio.valor.ToString("C", CultureInfo.CurrentCulture)}";
            margin = new Thickness(60, 206, 0, 0);
            TextBlock valorServicio = CreateTextBlock(name, text, 18, FontWeights.Bold, margin, VerticalAlignment.Top);

            parent.Children.Add(tipoServicio);
            parent.Children.Add(nombreServicio);
            parent.Children.Add(descripcionServicio);
            parent.Children.Add(valorServicio);
        }

        private TextBlock CreateTextBlock(string name, string text, double fontSize, FontWeight fontWeight, Thickness margin, VerticalAlignment verticalAlignment)
        {
            TextBlock textblock = new TextBlock();
            textblock.Name = name;
            textblock.FontSize = fontSize;
            textblock.HorizontalAlignment = HorizontalAlignment.Left;
            textblock.TextWrapping = TextWrapping.Wrap;
            textblock.FontWeight = fontWeight;
            textblock.Text = text;
            textblock.Margin = margin;
            textblock.VerticalAlignment = verticalAlignment;
            return textblock;
        }

        private Grid CreateServicioContainer(int id, double marginTop)
        {
            Grid container = new Grid();
            container.Name = $"gridServicio{id}";
            container.VerticalAlignment = VerticalAlignment.Top;
            container.Background = Brushes.Beige;
            container.HorizontalAlignment = HorizontalAlignment.Left;
            container.Height = 250;
            container.Width = 650;
            container.Margin = new Thickness(20, marginTop, 20,0);
            return container;
        }

        private void BackToDepto(object sender, RoutedEventArgs e)
        {
            previousWindow.SetNextWindow(this);
            previousWindow.Show();
            Hide();
        }

        private void StepAsistentes(object sender, RoutedEventArgs e)
        {
            if (nextWindow != null) nextWindow.Show();

            if (nextWindow == null)
            {
                NuevaReservaAsistente reservaAsistentesWin = new NuevaReservaAsistente(_reserva, this);
                reservaAsistentesWin.Show();
            }
            Hide();
        }

        public void SetNextWindow(NuevaReservaAsistente nextWindow) => this.nextWindow = nextWindow;
        private void AgregarQuitarServicio(object sender, RoutedEventArgs e) => AgregarQuitar(e.Source as Button);

        private void AgregarQuitar(Button source)
        {
            int servicio_id = Convert.ToInt32(source.Name.Substring(source.Name.Length - 1, 1));

            if (source.Content.Equals("AGREGAR"))
            {
                ServicioDTO selectedServicio = _servicios.Find(x => x.idServicio == servicio_id);
                ServicioReservaDTO servicioReserva = new ServicioReservaDTO(selectedServicio);
                if (_reserva.servicios == null) _reserva.servicios = new List<ServicioReservaDTO>();
                servicioReserva.id = 0;
                servicioReserva.conductor = 0;
                _reserva.servicios.Add(servicioReserva);
                source.Content = "QUITAR";
                _reserva.valorArriendo += selectedServicio.valor;
                ChangeParentBg(source.Parent as Grid, Brushes.Gray);
                seleccionados++;
            }
            else if (source.Content.Equals("QUITAR"))
            {
                ServicioReservaDTO selectedServicio = _reserva.servicios.Find(x => x.idServicio == servicio_id);
                _reserva.servicios.Remove(selectedServicio);
                source.Content = "AGREGAR";
                _reserva.valorArriendo -= selectedServicio.valor;
                ChangeParentBg(source.Parent as Grid, Brushes.Beige);
                seleccionados--;
            }
            SetTotalServicios(_reserva.GetTotalServicios());
            SetTotalReserva(_reserva.valorArriendo);
            SetCantidadServicios(seleccionados);
        }

        private void ChangeParentBg(Grid parent, Brush color) => parent.Background = color;
    }
}
