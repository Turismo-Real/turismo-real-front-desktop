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
using turismo_real_desktop.UIElements;
using turismo_real_desktop.Views.Reservas;

namespace turismo_real_desktop.Views.Usuarios
{
    public partial class NuevoUsuario : MetroWindow
    {
        private UsuariosWindow usuariosWin;
        private NuevaReservaCliente reservaWin;
        private UsuarioController usuarioController;
        private UtilController utilController;

        public NuevoUsuario(UsuariosWindow usuariosWin)
        {
            InitializeComponent();
            this.usuariosWin = usuariosWin;
            FillComboBoxes();
        }

        public NuevoUsuario(NuevaReservaCliente reservaWin)
        {
            InitializeComponent();
            this.reservaWin = reservaWin;
            FillComboBoxes();
            cboxTipo.SelectedIndex = 3; // Tipo cliente
            cboxTipo.IsEnabled = false;
        }

        public string GetSource(FrameworkElement src) => src.Name;
        private void CancelarInsercion(object sender, RoutedEventArgs e) => Close();
        private void OnHoverCancelar(object sender, MouseEventArgs e) => ChangeHoverColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveCancelar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverGuardar(object sender, MouseEventArgs e) => ChangeHoverColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveGuardar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));

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
                ShowSuccessMessage(nuevoUsuario.tipoUsuario);

                if (reservaWin != null)
                {
                    reservaWin.BuscarUsuario(GetRutOrPasaporte(nuevoUsuario));
                    reservaWin.PrintNuevoUsuario();
                    Close();
                    return;
                }
                usuariosWin.FillDataGridUsuarios();
                return;
            }
            mensajeUsuario.Text = "Error al crear departamento.";
            mensajeUsuario.Foreground = Brushes.Red;
        }

        public string GetRutOrPasaporte(UsuarioDTO usuario)
        {
            string rutPasaporte = !usuario.rut.Equals(string.Empty) && !usuario.dv.Equals(string.Empty)
                ? $"{usuario.rut}-{usuario.dv}"
                : !usuario.pasaporte.Equals(string.Empty)
                    ? usuario.pasaporte
                    : "0";
            return rutPasaporte;
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
            dpFecNacimiento.SelectedDate = null;
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
            usuarioController = new UsuarioController();
            string defaultPassword = usuarioController.ObtenerDefaultPasswordPorTipo(tipoUsuario);
            UsuarioMessage successWin = new UsuarioMessage(defaultPassword);
            successWin.ShowDialog();
        }

        public UsuarioDTO ConvertFormToUsuario()
        {
            usuarioController = new UsuarioController();
            string userType = cboxTipo.SelectedItem.ToString();
            string defaultPassword = usuarioController.ObtenerDefaultPasswordPorTipo(userType);
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
        //--//

        //Sentencia para que textbox lea solamente letras 
        private void txtPrimerNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtPrimerNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrimerNombre.Text))
            {
                MessageBox.Show("El campo Primer Nombre no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente letras
        private void txtSegundoNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtSegundoNombre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSegundoNombre.Text))
            {
                MessageBox.Show("El campo Segundo Nombre no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente letras
        private void txtPrimerApellido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtPrimerApellido_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrimerApellido.Text))
            {
                MessageBox.Show("El campo Primer Apellido no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente letras
        private void txtSegundoApellido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtSegundoApellido_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSegundoApellido.Text))
            {
                MessageBox.Show("El campo Segundo Apellido no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Validador de textbox Vacio
        private void txtCorreo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                MessageBox.Show("El campo Correo no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros
        private void txttMovil_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }

        }
        //Validador de textbox Vacio
        private void txttMovil_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txttMovil.Text))
            {
                MessageBox.Show("El campo Telefono Movil no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros
        private void txtFijo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtFijo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFijo.Text))
            {
                MessageBox.Show("El campo Telefono Fijo no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente letras
        private void txtCalle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }

        }
        //Validador de textbox Vacio
        private void txtCalle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCalle.Text))
            {
                MessageBox.Show("El campo Calle no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros
        private void txtNumero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtNumero_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show("El campo Numero no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros
        private void txtNroDepto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtNroDepto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNroDepto.Text))
            {
                MessageBox.Show("El campo Nro Departamento no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        //--//

        //Sentencia para que textbox lea solamente numeros
        private void txtNroCasa_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]"))
            {
                e.Handled = true;
            }
        }
        //Validador de textbox Vacio
        private void txtNroCasa_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNroCasa.Text))
            {
                MessageBox.Show("El campo Nro Casa no puede estar vacío", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                return;
            }
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string source)
        {
            // btnCancelar - btnGuardar -btnVolver - btnEditar
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
            if (source.Equals("btnGuardar") || source.Equals("btnEditar"))
                text.Foreground = UIColors.NormalGreen;
            else if (source.Equals("btnCancelar") || source.Equals("btnVolver"))
                text.Foreground = UIColors.Red;
        }
    }
}
