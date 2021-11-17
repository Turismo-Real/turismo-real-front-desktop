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
using turismo_real_desktop.UIElements;

namespace turismo_real_desktop.Views.Usuarios
{

    public partial class Usuario : MetroWindow
    {
        private UsuarioController usuarioController;
        private UtilController utilController;
        private UsuarioDTO selectedUsuario;
        private UsuariosWindow usuariosWin;

        public Usuario()
        {
            InitializeComponent();
            SetInitialStateGrids();
        }

        public Usuario(UsuariosWindow usuariosWin, int idUsuario)
        {
            InitializeComponent();
            this.usuariosWin = usuariosWin;
            usuarioController = new UsuarioController();
            selectedUsuario = usuarioController.ObtenerUsuario(idUsuario); ;
            SetInitialStateGrids();
            SetFormTitle(idUsuario);
            SetLabelForm(selectedUsuario);
            FillComboBoxes();
            RegionChanged();
            SetInputForm(selectedUsuario);
        }

        public string GetSource(FrameworkElement src) => src.Name;
        private void FillComboRegion() => cboxRegion.ItemsSource = utilController.ObtenerRegiones();
        private void Volver(object sender, RoutedEventArgs e) => Close();
        private void PaisChanged(object sender, SelectionChangedEventArgs e) => PaisChanged();
        private void RegionChanged(object sender, SelectionChangedEventArgs e) => RegionChanged();
        private void OnHoverCancelar(object sender, MouseEventArgs e) => ChangeHoverColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveGuardar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveCancelar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnCancelar, cancelarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverGuardar(object sender, MouseEventArgs e) => ChangeHoverColor(btnGuardar, guardarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverEditar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEditar, editarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveEditar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEditar, editarText, GetSource(e.OriginalSource as FrameworkElement));

        private void SetLabelForm(UsuarioDTO usuario)
        {
            string empty = "-";
            txtTipoEditar.Text = selectedUsuario.tipoUsuario;
            txtbTipo.Text = usuario.tipoUsuario;
            txtbPasaporte.Text = usuario.pasaporte.Equals(string.Empty) ? empty : usuario.pasaporte;
            txtbRut.Text = $"{usuario.rut}-{usuario.dv}";
            txtbPrimerNombre.Text = usuario.primerNombre;
            txtbSegundoNombre.Text = usuario.segundoNombre;
            txtbPrimerApellido.Text = usuario.apellidoPaterno;
            txtbSegundoApellido.Text = usuario.apellidoMaterno;
            txtbFecNac.Text = usuario.fechaNacimiento.ToString("dd/MM/yyyy");
            txtbCorreo.Text = usuario.correo;
            txtbTelMovil.Text = usuario.telefonoMovil.Equals(string.Empty) ? empty : usuario.telefonoMovil;
            txtbTelFijo.Text = usuario.telefonoFijo.Equals(string.Empty) ? empty : usuario.telefonoFijo;
            txtbGenero.Text = usuario.genero;
            txtbPais.Text = usuario.pais;
            txtbRegion.Text = usuario.direccion.region;
            txtbComuna.Text = usuario.direccion.comuna;
            txtbCalle.Text = usuario.direccion.calle;
            txtbNumero.Text = usuario.direccion.numero;
            txtbNroDepto.Text = usuario.direccion.depto.Equals(string.Empty) ? empty : usuario.direccion.depto;
            txtbNroCasa.Text = usuario.direccion.calle.Equals(string.Empty) ? empty : usuario.direccion.casa;
        }

        private void FillComboBoxes()
        {
            utilController = new UtilController();
            cboxGenero.ItemsSource = utilController.ObtenerGeneros();
            cboxPais.ItemsSource = utilController.ObtenerPaises();
            FillComboRegion();
        }

        private void SetInitialStateGrids()
        {
            gridVerUsuario.Visibility = Visibility.Visible;
            gridEditarUsuario.Visibility = Visibility.Hidden;
        }

        private void RegionChanged()
        {
            if(cboxRegion.SelectedIndex != 0 && cboxRegion.SelectedItem != null)
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

        private void SetFormTitle(int idUsuario)
        {
            string titulo = $"USUARIO {idUsuario}";
            editarUsuarioTitle.Text = titulo;
            verUsuarioTitle.Text = titulo;
        }

        public void SetInputForm(UsuarioDTO selectedUsuario)
        {
            txtPasaporte.Text = selectedUsuario.pasaporte;
            txtRut.Text = selectedUsuario.rut;
            txtDv.Text = selectedUsuario.dv;
            txtPrimerNombre.Text = selectedUsuario.primerNombre;
            txtSegundoNombre.Text = selectedUsuario.segundoNombre;
            txtPrimerApellido.Text = selectedUsuario.apellidoPaterno;
            txtSegundoApellido.Text = selectedUsuario.apellidoMaterno;
            dpFecNacimiento.SelectedDate = DateTime.Parse(selectedUsuario.fechaNacimiento.ToString("dd/MM/yyyy"));
            txtCorreo.Text = selectedUsuario.correo;
            txttMovil.Text = selectedUsuario.telefonoMovil;
            txtFijo.Text = selectedUsuario.telefonoFijo;
            cboxGenero.SelectedItem = selectedUsuario.genero;
            cboxPais.SelectedItem = selectedUsuario.pais;
            cboxRegion.SelectedItem = selectedUsuario.direccion.region;
            cboxComuna.SelectedItem = selectedUsuario.direccion.comuna;
            txtCalle.Text = selectedUsuario.direccion.calle;
            txtNumero.Text = selectedUsuario.direccion.numero;
            txtNroDepto.Text = selectedUsuario.direccion.depto;
            txtNroCasa.Text = selectedUsuario.direccion.casa;
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            usuarioController = new UsuarioController();
            UsuarioDTO usuario = ConvertFormToUsuario();
            usuario.idUsuario = selectedUsuario.idUsuario;
            UsuarioDTO updatedUsuario = usuarioController.ActualizarUsuario(usuario);

            string title;
            string message;
            if (updatedUsuario != null)
            {
                SetDataForm(updatedUsuario);
                usuariosWin.FillDataGridUsuarios();
                title = "Usuario Actualizado";
                message = "El usuario ha sido actualizado correctamente.";
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                this.Title = "Turismo Real - Usuario";
                return;
            }
            title = "Error al actualizar";
            message = "Ha ocurrido un error al intentar actualizar usuario.";
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void SetDataForm(UsuarioDTO usuario)
        {

        }

        public UsuarioDTO ConvertFormToUsuario()
        {
            usuarioController = new UsuarioController();
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
            usuario.genero = cboxGenero.SelectedItem.ToString();
            usuario.pais = cboxPais.SelectedItem.ToString();
            usuario.tipoUsuario = selectedUsuario.tipoUsuario;

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

        private void CancelarEdicion(object sender, RoutedEventArgs e)
        {
            gridEditarUsuario.Visibility = Visibility.Hidden;
            gridVerUsuario.Visibility = Visibility.Visible;
        }

        private void EditarUsuario(object sender, RoutedEventArgs e)
        {
            string tipo = selectedUsuario.tipoUsuario.ToUpper();
            if (!tipo.Equals("CLIENTE"))
            {
                DisableTextBox(txtRut);
                DisableTextBox(txtDv);
                DisableTextBox(txtPasaporte);
            }
            gridEditarUsuario.Visibility = Visibility.Visible;
            gridVerUsuario.Visibility = Visibility.Hidden;
        }

        public void DisableTextBox(TextBox textbox)
        {
            textbox.IsEnabled = false;
            textbox.Background = Brushes.LightGray;
        }


        public void PaisChanged()
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
