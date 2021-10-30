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
using turismo_real_services.REST.Usuario;

namespace turismo_real_desktop.Views.Usuarios
{


    public partial class NuevoUsuario : MetroWindow
    {

        public NuevoUsuario()
        {
            InitializeComponent();
        }

        private void CancelarInsercion(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }

        private void RegionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GuardarUsuario(object sender, RoutedEventArgs e)
        {

        }
    }
}
