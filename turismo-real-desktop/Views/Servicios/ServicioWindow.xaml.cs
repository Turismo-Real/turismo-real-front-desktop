using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Servicio;
using turismo_real_controller.Controllers.Util;

namespace turismo_real_desktop.Views.Servicios
{
    public partial class ServicioWindow : MetroWindow
    {
        private ServicioController servicioController;
        private UtilController utilController;
        private ServiciosWindow activeServiciosWindow;
        private ServicioDTO selectedServicio;
        private int idServicio;

        public ServicioWindow()
        {
            InitializeComponent();
        }

        public ServicioWindow(ServiciosWindow activeServiciosWindow, int idServicio)
        {
            InitializeComponent();

            this.activeServiciosWindow = activeServiciosWindow;
            this.idServicio = idServicio;
            string titulo = $"SERVICIO {this.idServicio}";
            tituloServicio.Text = titulo;
            tituloServicioEditar.Text = titulo;
            FillComboTipo();

            servicioController = new ServicioController();
            selectedServicio = servicioController.ObtenerServicio(idServicio);
            SetDataForm(selectedServicio);

            gridVerServicio.Visibility = Visibility.Visible;
            gridEditarServicio.Visibility = Visibility.Hidden;
        }

        public void FillComboTipo()
        {
            utilController = new UtilController();
            List<string> tiposServicio = utilController.ObtenerTiposServicios();
            cboxTipoServicio.ItemsSource = tiposServicio;
            cboxTipoServicio.SelectedIndex = 0;
        }

        public void SetDataForm(ServicioDTO servicio)
        {
            // Fill inputs
            cboxTipoServicio.SelectedItem = servicio.tipo;
            txtNombreServicio.Text = servicio.nombre;
            txtValorServicio.Text = servicio.valor.ToString();
            txtDescripcion.Text = servicio.descripcion;
            // Fill labels
            tipoText.Content = servicio.tipo;
            nombreText.Content = servicio.nombre;
            valorText.Content = servicio.valor;
            descripcionText.Content = servicio.descripcion;
        }

        private void Volver(object sender, RoutedEventArgs e) => Close();

        private void OnHoverVolver(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveVolver(object sender, MouseEventArgs e)
        {

        }

        private void EditarServicio(object sender, RoutedEventArgs e)
        {
            gridVerServicio.Visibility = Visibility.Hidden;
            gridEditarServicio.Visibility = Visibility.Visible;
        }

        private void OnHoverEditar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveEditar(object sender, MouseEventArgs e)
        {

        }

        private void CancelarCambios(object sender, RoutedEventArgs e)
        {
            gridEditarServicio.Visibility = Visibility.Hidden;
            gridVerServicio.Visibility = Visibility.Visible;
        }

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {

        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }
    }
}
