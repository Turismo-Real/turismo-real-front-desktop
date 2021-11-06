using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
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

        private void EditarServicio(object sender, RoutedEventArgs e) => ChangeGridVisibility();

        private void OnHoverEditar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveEditar(object sender, MouseEventArgs e)
        {

        }

        private void CancelarCambios(object sender, RoutedEventArgs e) => ChangeGridVisibility();

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            servicioController = new ServicioController();
            ServicioDTO servicio = ConvertFormToServicio();
            servicio.idServicio = idServicio;
            ServicioDTO updatedServicio = servicioController.ActualizarServicio(servicio);

            string title;
            string message;
            if (updatedServicio != null)
            {
                SetDataForm(updatedServicio);
                activeServiciosWindow.FillDataGridServicios();
                title = "Servicio Actualizado";
                message = "El servicio ha sido actualizado correctamente.";
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                this.Title = "Turismo Real - Servicio";
                ChangeGridVisibility();
                return;
            }
            title = "Error al actualizar";
            message = "Ha ocurrido un error al intentar actualizar el servicio.";
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ChangeGridVisibility()
        {
            if (gridVerServicio.Visibility.Equals(Visibility.Hidden))
            {
                gridEditarServicio.Visibility = Visibility.Hidden;
                gridVerServicio.Visibility = Visibility.Visible;
                return;
            }
            gridEditarServicio.Visibility = Visibility.Visible;
            gridVerServicio.Visibility = Visibility.Hidden;
        }

        public ServicioDTO ConvertFormToServicio()
        {
            ServicioDTO servicio = new ServicioDTO();
            servicio.tipo = cboxTipoServicio.SelectedItem.ToString();
            servicio.nombre = txtNombreServicio.Text;
            servicio.valor = Convert.ToDouble(txtValorServicio.Text);
            servicio.descripcion = txtDescripcion.Text;
            return servicio;
        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }
    }
}
