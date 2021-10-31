using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_controller.Controllers.Util;
using turismo_real_controller.Hasher;

namespace turismo_real_desktop.Views.Usuarios
{


    public partial class NuevoUsuario : MetroWindow
    {
        private UsuariosWindow usuariosWin;
        private UsuarioController usuarioController;
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
            if (cboxRegion.SelectedIndex != 0 && cboxRegion.SelectedItem != null)
            {
                utilController = new UtilController();
                string region = cboxRegion.SelectedItem.ToString();
                List<string> comunas = utilController.ObtenerComunasPorRegion(region);
                EnabledComboBox(cboxComuna);
                cboxComuna.ItemsSource = comunas;
                cboxComuna.SelectedIndex = 0;
                return;
            }
            DisabledComboBox(cboxComuna);
            cboxComuna.ItemsSource = new List<string>();
        }

        private void GuardarUsuario(object sender, RoutedEventArgs e)
        {
            UsuarioDTO nuevoUsuario = ConvertFormToUsuario();
            usuarioController = new UsuarioController();
            bool nuevoUsuarioInsertado = usuarioController.AgregarNuevoUsuario(nuevoUsuario);

            if (nuevoUsuarioInsertado)
            {
                ClearForm();
                usuariosWin.FillDataGridUsuarios();
                ShowSuccessMessage(nuevoUsuario.tipoUsuario);
                return;
            }
            mensajeUsuario.Text = "Error al crear departamento.";
            mensajeUsuario.Foreground = Brushes.Red;
        }

        public void ClearForm()
        {
            cboxTipo.SelectedIndex = 0;
            txtPasaporte.Text = string.Empty;
            txtRut.Text = string.Empty;
            txtDv.Text = string.Empty;
            txtPrimerNombre.Text = string.Empty;
            txtSegundoNombre.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            dpFecNacimiento.SelectedDate = new DateTime();
            txtCorreo.Text = string.Empty;
            txttMovil.Text = string.Empty;
            txtFijo.Text = string.Empty;
            cboxGenero.SelectedIndex = 0;
            cboxPais.SelectedIndex = 0;
            txtCalle.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtNroDepto.Text = string.Empty;
            txtNroCasa.Text = string.Empty;
        }

        public void ShowSuccessMessage(string tipoUsuario)
        {
            string defaultPassword = GetDefaultPasswordByUserType(tipoUsuario);
            UsuarioMessage successWin = new UsuarioMessage(defaultPassword);
            successWin.ShowDialog();
        }

        public UsuarioDTO ConvertFormToUsuario()
        {
            string userType = cboxTipo.SelectedItem.ToString();
            string defaultPassword = GetDefaultPasswordByUserType(userType);
            UsuarioDTO usuario = new UsuarioDTO();
            usuario.pasaporte = txtPasaporte.Text.Equals(string.Empty) ? null : txtPasaporte.Text;
            usuario.rut = txtRut.Text.Equals(string.Empty) ? null : txtRut.Text;
            usuario.dv = txtDv.Text.Equals(string.Empty) ? null : txtDv.Text;
            usuario.primerNombre = txtPrimerNombre.Text;
            usuario.segundoNombre = txtSegundoNombre.Text;
            usuario.apellidoPaterno = txtPrimerApellido.Text;
            usuario.apellidoMaterno = txtSegundoApellido.Text;
            usuario.fechaNacimiento = Convert.ToDateTime(dpFecNacimiento.SelectedDate.Value.Date.ToShortDateString());
            usuario.correo = txtCorreo.Text;
            usuario.telefonoMovil = txttMovil.Text.Equals(string.Empty) ? null : txttMovil.Text;
            usuario.telefonoFijo = txtFijo.Text.Equals(string.Empty) ? null : txtFijo.Text;
            usuario.password = new HashPassword().HashSHA256(defaultPassword);
            usuario.genero = cboxGenero.SelectedItem.ToString();
            usuario.pais = cboxPais.SelectedItem.ToString();
            usuario.tipoUsuario = userType;

            DireccionDTO direccion = new DireccionDTO();
            direccion.region = cboxRegion.SelectedItem == null ? "Sin Región" : cboxRegion.SelectedItem.ToString();
            direccion.comuna = cboxComuna.SelectedItem == null ? "Sin comuna" : cboxComuna.SelectedItem.ToString();
            direccion.calle = txtCalle.Text;
            direccion.numero = txtNumero.Text;
            direccion.depto = txtNroDepto.Text;
            direccion.casa = txtNroCasa.Text;
            usuario.direccion = direccion;
            return usuario;
        }

        public string GetDefaultPasswordByUserType(string userType)
        {
            string defaultPassword = string.Empty;
            userType = userType.ToUpper();
            if (userType.Equals("ADMINISTRADOR")) defaultPassword = "administrar";
            if (userType.Equals("FUNCIONARIO")) defaultPassword = "checkin";
            if (userType.Equals("CLIENTE")) defaultPassword = "turismo";
            return defaultPassword;
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
                FillComboRegion();
                return;
            }
            cboxComuna.SelectedIndex = 0;
            DisabledComboBox(cboxComuna);
            cboxComuna.ItemsSource = new List<string>();
            cboxRegion.SelectedIndex = 0;
            DisabledComboBox(cboxRegion);
            cboxRegion.ItemsSource = new List<string>();
        }

        public void EnabledComboBox(ComboBox combobox)
        {
            combobox.IsEnabled = true;
            combobox.Background = Brushes.White;
        }

        public void DisabledComboBox(ComboBox combobox)
        {
            combobox.IsEnabled = false;
            combobox.Background = Brushes.LightGray;
        }

        public void FillComboRegion()
        {
            utilController = new UtilController();
            List<string> regiones = utilController.ObtenerRegiones();
            cboxRegion.ItemsSource = regiones;
            cboxRegion.SelectedIndex = 0;
            cboxComuna.ItemsSource = new List<string>();
        }


    }
}
