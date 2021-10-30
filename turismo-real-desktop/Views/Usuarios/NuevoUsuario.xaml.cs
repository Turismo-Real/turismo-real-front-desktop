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
using turismo_real_controller.Controllers.Util;
using turismo_real_services.REST.Usuario;

namespace turismo_real_desktop.Views.Usuarios
{


    public partial class NuevoUsuario : MetroWindow
    {
        private UsuariosWindow usuariosWin;
        private UtilController utilController;

        public NuevoUsuario(UsuariosWindow usuariosWin)
        {
            InitializeComponent();
            this.usuariosWin = usuariosWin;
            FillComboBoxes();
        }

        public void FillComboBoxes()
        {
            utilController = new UtilController();
            cboxTipo.ItemsSource = utilController.ObtenerTiposUsuarios();
            cboxTipo.SelectedIndex = 0;
            cboxGenero.ItemsSource = utilController.ObtenerGeneros();
            cboxGenero.SelectedIndex = 0;
            cboxPais.ItemsSource = utilController.ObtenerPaises();
            cboxPais.SelectedIndex = 0;
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

        private void TipoChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedType = cboxTipo.SelectedItem.ToString().ToUpper();
            if (selectedType.Equals("CLIENTE"))
            {
                EnabledTextBox(txtPasaporte);
                EnabledTextBox(txtRut);
                EnabledTextBox(txtDv);
                return;
            }
            DisabledTextBox(txtPasaporte);
            DisabledTextBox(txtRut);
            DisabledTextBox(txtDv);
        }

        public void EnabledTextBox(TextBox textbox)
        {
            textbox.IsEnabled = true;
            textbox.Background = Brushes.White;
        }

        public void DisabledTextBox(TextBox textbox)
        {
            textbox.IsEnabled = false;
            textbox.Background = Brushes.LightGray;
        }

        private void PaisChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedCountry = cboxPais.SelectedItem.ToString().ToUpper();
            if (selectedCountry.Equals("CHILE"))
            {
                EnabledComboBox(cboxRegion);
                return;
            }
            DisabledComboBox(cboxRegion);
        }

        public void EnabledComboBox(ComboBox combobox)
        {

        }

        public void DisabledComboBox(ComboBox combobox)
        {

        }
    }
}
