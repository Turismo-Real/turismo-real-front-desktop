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
            FillInputs(loguedUser);
            FillLabels(loguedUser);
        }

        public void FillLabels(UsuarioDTO loguedUser)
        {
            if (loguedUser != null)
            {
                txtbPrimerNombre.Text = loguedUser.primerNombre;
                txtbSegundoNombre.Text = loguedUser.segundoNombre;
                txtbPrimerApellido.Text = loguedUser.apellidoPaterno;
                txtbSegundoApellido.Text = loguedUser.apellidoMaterno;
                txtbFecNac.Text = loguedUser.fechaNacimiento.ToString("dd/MM/yyyy");
                txtbCorreo.Text = loguedUser.correo;
                txtbTelMovil.Text = loguedUser.telefonoMovil;
                txtbTelFijo.Text = loguedUser.telefonoFijo;
                txtbGenero.Text = loguedUser.genero;
                txtbPais.Text = loguedUser.pais;
                txtbRegion.Text = loguedUser.direccion.region;
                txtbComuna.Text = loguedUser.direccion.comuna;
                txtbCalle.Text = loguedUser.direccion.calle;
                txtbNumero.Text = loguedUser.direccion.numero;
                txtbNroDepto.Text = loguedUser.direccion.depto;
                txtbNroCasa.Text = loguedUser.direccion.casa;
            }
        }

        public void FillInputs(UsuarioDTO loguedUser)
        {
            if (loguedUser != null)
            {
                txtPrimerNombre.Text = loguedUser.primerNombre;
                txtSegundoNombre.Text = loguedUser.segundoNombre;
                txtPrimerApellido.Text = loguedUser.apellidoPaterno;
                txtSegundoApellido.Text = loguedUser.apellidoMaterno;
                dpFecNacimiento.SelectedDate = DateTime.Parse(loguedUser.fechaNacimiento.ToString("dd/MM/yyyy"));
                txtCorreo.Text = loguedUser.correo;
                txtMovil.Text = loguedUser.telefonoMovil;
                txtFijo.Text = loguedUser.telefonoFijo;
                cboxGenero.SelectedItem = loguedUser.genero;
                cboxPais.SelectedItem = loguedUser.pais;
                cboxRegion.SelectedItem = loguedUser.direccion.region;
                cboxComuna.SelectedItem = loguedUser.direccion.comuna;
                txtCalle.Text = loguedUser.direccion.calle;
                txtNumero.Text = loguedUser.direccion.numero;
                txtDepto.Text = loguedUser.direccion.depto;
                txtCasa.Text = loguedUser.direccion.casa;
            }
        }

        public void FillComboBoxes()
        {
            utilController = new UtilController();
            cboxGenero.ItemsSource = utilController.ObtenerGeneros();
            cboxPais.ItemsSource = utilController.ObtenerPaises();
            FillComboRegion();
        }

        private void Volver(object sender, RoutedEventArgs e)
        {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        private void EditarUsuario(object sender, RoutedEventArgs e) => ChangeGridVisibility();
        private void CancelarEdicion(object sender, RoutedEventArgs e) => ChangeGridVisibility();
        public void FillComboRegion() => cboxRegion.ItemsSource = utilController.ObtenerRegiones();

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
            usuarioController = new UsuarioController();
            UsuarioDTO usuario = ConvertFormToUsuario();
            Trace.WriteLine(usuario.idUsuario);
            UsuarioDTO updatedUsuario = usuarioController.ActualizarUsuario(usuario);

            string title;
            string message;
            if (updatedUsuario != null)
            {
                LoguedUser.SetLoguedUser(updatedUsuario);
                FillDataForm();
                ChangeGridVisibility();
                title = "Datos actualizados";
                message = "Tus datos han sido actualizados correctamente.";
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            title = "Error al actualizar";
            message = "Ha ocurrido un error al intentar actualizar tu información.";
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public UsuarioDTO ConvertFormToUsuario()
        {
            UsuarioDTO usuario = new UsuarioDTO();
            usuario.idUsuario = LoguedUser.GetLoguedUser().idUsuario;
            usuario.primerNombre = txtPrimerNombre.Text;
            usuario.segundoNombre = txtSegundoNombre.Text;
            usuario.apellidoPaterno = txtPrimerApellido.Text;
            usuario.apellidoMaterno = txtSegundoApellido.Text;
            usuario.fechaNacimiento = Convert.ToDateTime(dpFecNacimiento.SelectedDate.Value.Date.ToShortDateString());
            usuario.correo = txtCorreo.Text;
            usuario.telefonoFijo = txtFijo.Text;
            usuario.telefonoMovil = txtMovil.Text;
            usuario.genero = cboxGenero.SelectedItem.ToString();
            usuario.pais = cboxPais.SelectedItem.ToString();
            usuario.tipoUsuario = LoguedUser.GetLoguedUser().tipoUsuario;
            DireccionDTO direccion = new DireccionDTO();
            direccion.region = cboxRegion.SelectedItem.ToString();
            direccion.comuna = cboxComuna.SelectedItem.ToString();
            direccion.numero = txtNumero.Text;
            direccion.calle = txtCalle.Text;
            direccion.depto = txtDepto.Text;
            direccion.casa = txtCasa.Text;
            usuario.direccion = direccion;
            return usuario;
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
            string selectedCountry = cboxPais.SelectedItem.ToString().ToUpper();
            if (selectedCountry.Equals("CHILE"))
            {
                EnabledComboBox(cboxRegion);
                FillComboRegion();
                cboxRegion.SelectedIndex = 0;
                return;
            }
            cboxComuna.SelectedIndex = 0;
            DisabledComboBox(cboxComuna);
            cboxComuna.ItemsSource = new List<string>();
            cboxRegion.SelectedIndex = 0;
            DisabledComboBox(cboxRegion);
            cboxRegion.ItemsSource = new List<string>();
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

        private void CambiarPassword(object sender, RoutedEventArgs e)
        {
            PasswordWindow passWin = new PasswordWindow(LoguedUser.GetLoguedUser().idUsuario);
            passWin.ShowDialog();
        }

        private void OnHoverCambioPass(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveCambioPass(object sender, MouseEventArgs e)
        {

        }
    }
}
