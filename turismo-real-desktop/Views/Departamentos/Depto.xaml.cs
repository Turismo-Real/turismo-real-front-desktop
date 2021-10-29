using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using turismo_real_controller.Controllers.Departamento;

namespace turismo_real_desktop.Views.Departamentos
{

    public partial class Depto : MetroWindow
    {

        public Depto()
        {
            InitializeComponent();
        }

        public Depto(int deptoId)
        {
            InitializeComponent();
            tituloDepto.Content = $"DEPARTAMENTO {deptoId}";
            DepartamentoDTO selectedDepto = new DepartamentoController().ObtenerDepartamento(deptoId);
            SetDeptoData(selectedDepto);
            Trace.WriteLine(selectedDepto.rol);
        }

        public void SetDeptoData(DepartamentoDTO depto)
        {

        }

        private void AgregarInstalacion(object sender, RoutedEventArgs e)
        {

        }

        private void RegionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void QuitarInstalacion(object sender, RoutedEventArgs e)
        {

        }

        private void InvertirInstalaciones(object sender, RoutedEventArgs e)
        {

        }

        private void NuevaInstalacion(object sender, RoutedEventArgs e)
        {

        }

        private void NuevaInstalacionFocus(object sender, RoutedEventArgs e)
        {

        }

        private void NuevaInstalacionLostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }

        private void CancelarInsercion(object sender, RoutedEventArgs e)
        {

        }

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }
    }
}
