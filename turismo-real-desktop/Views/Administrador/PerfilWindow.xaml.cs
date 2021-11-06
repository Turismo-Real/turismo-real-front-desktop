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
using turismo_real_business.Singleton;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_controller.Controllers.Util;

namespace turismo_real_desktop.Views.Administrador
{

    public partial class PerfilWindow : MetroWindow
    {
        private UtilController utilController;
        private UsuarioController usuarioController;

        public PerfilWindow()
        {
            InitializeComponent();
            FillComboBoxes();
            FillDataForm();
            gridVerUsuario.Visibility = Visibility.Visible;
            gridEditarUsuario.Visibility = Visibility.Hidden;
        }

        public void FillDataForm()
        {
            UsuarioDTO loguedUser = LoguedUser.GetLoguedUser();
            // Fill inputs
            txtPrimerNombre.Text = loguedUser.primerNombre;
            txtSegundoNombre.Text = loguedUser.segundoNombre;
            txtPrimerApellido.Text = loguedUser.apellidoPaterno;
            txtSegundoApellido.Text = loguedUser.apellidoMaterno;
            // Fill labels
        }

        public void FillComboBoxes()
        {
            utilController = new UtilController();
            cboxGenero.ItemsSource = utilController.ObtenerGeneros();
            cboxPais.ItemsSource = utilController.ObtenerPaises();
            cboxRegion.ItemsSource = utilController.ObtenerRegiones();
        }

        private void Volver(object sender, RoutedEventArgs e)
        {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        private void EditarUsuario(object sender, RoutedEventArgs e) => ChangeGridVisibility();
        private void CancelarEdicion(object sender, RoutedEventArgs e) => ChangeGridVisibility();

        public void ChangeGridVisibility()
        {
            if (gridVerUsuario.Visibility.Equals(Visibility.Hidden))
            {
                gridEditarUsuario.Visibility = Visibility.Hidden;
                gridVerUsuario.Visibility = Visibility.Visible;
                return;
            }
            gridEditarUsuario.Visibility = Visibility.Visible;
            gridVerUsuario.Visibility = Visibility.Hidden;
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

        private void OnHoverVolver(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveVolver(object sender, MouseEventArgs e)
        {

        }

        private void OnHoverEditar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveEditar(object sender, MouseEventArgs e)
        {

        }

        private void PaisChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RegionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
