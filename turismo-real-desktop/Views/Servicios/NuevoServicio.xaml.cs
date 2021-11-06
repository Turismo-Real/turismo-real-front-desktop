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
    public partial class NuevoServicio : MetroWindow
    {
        private ServicioController servicioController;
        private UtilController utilController;

        public NuevoServicio()
        {
            InitializeComponent();
            FillComboTipo();
        }

        public void FillComboTipo()
        {
            utilController = new UtilController();
            List<string> tiposServicio = utilController.ObtenerTiposServicios();
            cboxTipoServicio.ItemsSource = tiposServicio;
            cboxTipoServicio.SelectedIndex = 0;
        }

        //btn guardar servicio
        private void GuardarServicio(object sender, RoutedEventArgs e)
        {
            ServicioDTO nuevoServicio = ConvertFormToServicio();
            servicioController = new ServicioController();
            bool nuevoServicioInsertado = servicioController.AgregarServicio(nuevoServicio);

            if (nuevoServicioInsertado)
            {
                ClearForm();
                //activeDeptosWindow.FillDataGridDeptos();
                //ShowSuccessMessage();
                return;
            }
            //mensajeUsuario.Text = "Error al crear departamento.";
            //mensajeUsuario.Foreground = Brushes.Red;
        }

        public void ClearForm()
        {

        }

        public ServicioDTO ConvertFormToServicio()
        {
            ServicioDTO servicio = new ServicioDTO();
            return servicio;
        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }

        //btn cancelar
        private void CancelarInsercion(object sender, RoutedEventArgs e) => Close();

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }

        private void txtNombreServicio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtNombreServicio_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtValorServicio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtValorServicio_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtDescripcion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txtDescripcion_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Close(object sender, ContextMenuEventArgs e) => Close();
    }
}
