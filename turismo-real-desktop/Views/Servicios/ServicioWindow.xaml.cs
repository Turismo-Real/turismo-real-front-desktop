using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Servicio;
using turismo_real_controller.Controllers.Util;
using turismo_real_desktop.UIElements;

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

        public string GetSource(FrameworkElement src) => src.Name;
        private void Volver(object sender, RoutedEventArgs e) => Close();
        private void EditarServicio(object sender, RoutedEventArgs e) => ChangeGridVisibility();
        private void CancelarCambios(object sender, RoutedEventArgs e) => ChangeGridVisibility();
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverEditar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEditar, editarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveEditar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEditar, editarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverCancelar(object sender, MouseEventArgs e) => ChangeHoverColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveCancelar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverGuardar(object sender, MouseEventArgs e) => ChangeHoverColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveGuardar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));

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

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string source)
        {
            if (source.Equals("btnGuardar") || source.Equals("btnEditar"))
            {
                tile.Background = UIColors.NormalGreen;
                text.Foreground = UIColors.White;
            }
            else if (source.Equals("btnCancelar") || source.Equals("btnVolver"))
            {
                tile.Background = UIColors.Red;
                text.Foreground = UIColors.White;
            }
        }

        public void ChangeLeaveColor(Tile tile, TextBlock text, string source)
        {
            tile.Background = UIColors.White;
            if (source.Equals("btnGuardar") || source.Equals("btnEditar")) text.Foreground = UIColors.NormalGreen;
            else if (source.Equals("btnCancelar") || source.Equals("btnVolver")) text.Foreground = UIColors.Red;
        }
    }
}
