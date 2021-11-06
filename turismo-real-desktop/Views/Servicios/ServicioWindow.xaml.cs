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

namespace turismo_real_desktop.Views.Servicios
{
    public partial class ServicioWindow : MetroWindow
    {
        private ServiciosWindow activeServiciosWindow;

        public ServicioWindow()
        {
            InitializeComponent();
        }

        public ServicioWindow(ServiciosWindow activeServiciosWindow, int idServicio)
        {
            InitializeComponent();

            this.activeServiciosWindow = activeServiciosWindow;

            gridVerServicio.Visibility = Visibility.Visible;
            gridEditarServicio.Visibility = Visibility.Hidden;

            //this.deptoId = deptoId;
            //this.deptosWindow = deptosWindow;
            //string titulo = $"DEPARTAMENTO {deptoId}";
            //tituloDepto.Content = titulo;
            //tituloDeptoEditar.Content = titulo;

            //selectedDepto = new DepartamentoController().ObtenerDepartamento(deptoId);
            //SetDataForm(selectedDepto);

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
