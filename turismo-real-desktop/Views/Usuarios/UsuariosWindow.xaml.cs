using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.Views.Administrador;

namespace turismo_real_desktop.Views.Usuarios
{
    public partial class UsuariosWindow : MetroWindow
    {
        private UsuarioController usuarioController;
        private List<UsuarioDTO> usuarios;

        public UsuariosWindow()
        {
            InitializeComponent();
            FillDataGridUsuarios();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Dashboard dashWindow = new Dashboard();
            dashWindow.Show();
            Close();
        }

        public void FillDataGridUsuarios()
        {
            usuarioController = new UsuarioController();
            usuarios = usuarioController.ObtenerUsuarios(); // obtener usuarios

            ObservableCollection<UsuarioGrid> usuariosGrid = new ObservableCollection<UsuarioGrid>();
            List<UsuarioGrid> usuariosGridList = ConvertToUsuarioGrid(usuarios);

            foreach (UsuarioGrid usuario in usuariosGridList)
            {
                usuariosGrid.Add(usuario);
            }
            dataGridUsuarios.ItemsSource = usuariosGrid;
            contadorUsuarios.Text = $"Total usuarios: {usuarios.Count}";
            dataGridUsuarios.IsReadOnly = true;
        }

        public List<UsuarioGrid> ConvertToUsuarioGrid(List<UsuarioDTO> usuarios)
        {
            List<UsuarioGrid> usuariosGrid = new List<UsuarioGrid>();
            foreach(UsuarioDTO usuario in usuarios)
            {
                UsuarioGrid usuarioGrid = new UsuarioGrid();
                usuarioGrid.id = usuario.idUsuario;
                usuarioGrid.rut = usuario.rut.Equals(string.Empty) ? "0" : $"{usuario.rut}-{usuario.dv}";
                usuarioGrid.pasaporte = usuario.pasaporte.Equals(string.Empty) ? "0" : usuario.pasaporte;
                usuarioGrid.nombre = $"{usuario.primerNombre} {usuario.apellidoPaterno} {usuario.apellidoMaterno}";
                usuarioGrid.email = usuario.correo;
                usuarioGrid.pais = usuario.pais;
                usuarioGrid.tipo = usuario.tipoUsuario;
                usuariosGrid.Add(usuarioGrid);
            }
            return usuariosGrid;
        }

        private void OnHoverVolver(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveVolver(object sender, MouseEventArgs e)
        {

        }

        private void OnHoverEliminar(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaverEliminar(object sender, MouseEventArgs e)
        {

        }

        private void EliminarUsuario(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro que desea eliminar a este usuario?", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                usuarioController = new UsuarioController();
                UsuarioGrid selectedUser = dataGridUsuarios.SelectedItem as UsuarioGrid;
                int id = Convert.ToInt32(selectedUser.id);
                bool removed = usuarioController.EliminarUsuario(id);
                if (removed) FillDataGridUsuarios();
            }
            else
            {
                return;
            }
        }

        private void OnLeaveSeleccionar(object sender, MouseEventArgs e)
        {

        }

        private void OnHoverSeleccionar(object sender, MouseEventArgs e)
        {

        }

        private void SeleccionarUsuario(object sender, RoutedEventArgs e)
        {
            if (dataGridUsuarios.SelectedItem != null)
            {
                UsuarioGrid selectedUsuario = dataGridUsuarios.SelectedItem as UsuarioGrid;
                Usuario selectedUsuarioWin = new Usuario(this, selectedUsuario.id);
                selectedUsuarioWin.Show();
            }
        }

        private void OnHoverNuevoDepto(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveNuevoDepto(object sender, MouseEventArgs e)
        {

        }

        private void OpenNuevoUsuario(object sender, RoutedEventArgs e)
        {
            NuevoUsuario nuevoUsuarioWin = new NuevoUsuario(this);
            nuevoUsuarioWin.Show();
        }
    }
}
