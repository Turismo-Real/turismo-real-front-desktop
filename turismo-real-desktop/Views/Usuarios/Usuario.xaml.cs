using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_controller.Controllers.Util;

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
            cboxRegion.SelectedItem = selectedUsuario.direccion.region;
            RegionChanged();
            cboxComuna.SelectedItem = selectedUsuario.direccion.comuna;
            SetInputForm(selectedUsuario);
        }

        private void SetLabelForm(UsuarioDTO usuario)
        {
            string vacio = "-";
            txtTipoEditar.Text = selectedUsuario.tipoUsuario;
            txtbTipo.Text = usuario.tipoUsuario;
            txtbPasaporte.Text = usuario.pasaporte.Equals(string.Empty) ? vacio : usuario.pasaporte;
            txtbRut.Text = $"{usuario.rut}-{usuario.dv}";
            txtbPrimerNombre.Text = usuario.primerNombre;
            txtbSegundoNombre.Text = usuario.segundoNombre;
            txtbPrimerApellido.Text = usuario.apellidoPaterno;
            txtbSegundoApellido.Text = usuario.apellidoMaterno;
            txtbFecNac.Text = usuario.fechaNacimiento.ToString("dd/MM/yyyy");
            txtbCorreo.Text = usuario.correo;
            txtbTelMovil.Text = usuario.telefonoMovil.Equals(string.Empty) ? vacio : usuario.telefonoMovil;
            txtbTelFijo.Text = usuario.telefonoFijo.Equals(string.Empty) ? vacio : usuario.telefonoFijo;
            txtbGenero.Text = usuario.genero;
            txtbPais.Text = usuario.pais;
            txtbRegion.Text = usuario.direccion.region;
            txtbComuna.Text = usuario.direccion.comuna;
            txtbCalle.Text = usuario.direccion.calle;
            txtbNumero.Text = usuario.direccion.numero;
            txtbNroDepto.Text = usuario.direccion.depto.Equals(string.Empty) ? vacio : usuario.direccion.depto;
            txtbNroCasa.Text = usuario.direccion.calle.Equals(string.Empty) ? vacio : usuario.direccion.casa;
        }

        public void FillComboBoxes()
        {
            utilController = new UtilController();
            cboxGenero.ItemsSource = utilController.ObtenerGeneros();
            cboxPais.ItemsSource = utilController.ObtenerPaises();
            List<string> regiones = utilController.ObtenerRegiones();
            
            cboxRegion.ItemsSource = regiones;
        }

        public void SetInitialStateGrids()
        {
            gridVerUsuario.Visibility = Visibility.Visible;
            gridEditarUsuario.Visibility = Visibility.Hidden;
        }

        public void RegionChanged()
        {
            if (cboxRegion.SelectedIndex != 0)
            {
                utilController = new UtilController();
                string region = cboxRegion.SelectedItem.ToString();
                List<string> comunas = utilController.ObtenerComunasPorRegion(region);
                cboxComuna.ItemsSource = comunas;
                cboxComuna.SelectedIndex = 0;
                return;
            }
            cboxComuna.ItemsSource = new List<string>();
        }

        public void SetFormTitle(int idUsuario)
        {
            string titulo = $"USUARIO {idUsuario}";
            editarUsuarioTitle.Text = titulo;
            verUsuarioTitle.Text = titulo;
        }

        public void SetInputForm(UsuarioDTO selectedUsuario)
        {
            //cboxTipo
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
            //cboxComuna
            txtCalle.Text = selectedUsuario.direccion.calle;
            txtNumero.Text = selectedUsuario.direccion.numero;
            txtNroDepto.Text = selectedUsuario.direccion.depto;
            txtNroCasa.Text = selectedUsuario.direccion.casa;
        }

        private void OnHoverCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveGuardar(object sender, MouseEventArgs e)
        {

        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {


            gridEditarUsuario.Visibility = Visibility.Visible;
            gridVerUsuario.Visibility = Visibility.Hidden;
        }

        private void OnLeaveCancelar(object sender, MouseEventArgs e)
        {

        }

        private void OnHoverGuardar(object sender, MouseEventArgs e)
        {

        }

        private void Volver(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelarEdicion(object sender, RoutedEventArgs e)
        {
            gridEditarUsuario.Visibility = Visibility.Hidden;
            gridVerUsuario.Visibility = Visibility.Visible;
        }

        private void RegionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RegionChanged();
        }
    }
}
