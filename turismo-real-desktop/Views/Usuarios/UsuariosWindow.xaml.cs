using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.UIElements;
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

        public string GetSource(FrameworkElement src) => src.Name;
        private void OnHoverNuevoUsuario(object sender, MouseEventArgs e) => ChangeHoverColor(btnNuevoUsuario, nuevoUsuarioText, GetSource(e.OriginalSource as FrameworkElement), nuevoUsuarioIcon);
        private void OnLeaveNuevoUsuario(object sender, MouseEventArgs e) => ChangeLeaveColor(btnNuevoUsuario, nuevoUsuarioText, GetSource(e.OriginalSource as FrameworkElement), nuevoUsuarioIcon);
        private void OnHoverVolver(object sender, MouseEventArgs e) => ChangeHoverColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement), volverIcon);
        private void OnLeaveVolver(object sender, MouseEventArgs e) => ChangeLeaveColor(btnVolver, volverText, GetSource(e.OriginalSource as FrameworkElement), volverIcon);
        private void OnHoverEliminar(object sender, MouseEventArgs e) => ChangeHoverColor(btnEliminar, eliminarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaverEliminar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnEliminar, eliminarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnHoverSeleccionar(object sender, MouseEventArgs e) => ChangeHoverColor(btnSeleccionar, seleccionarText, GetSource(e.OriginalSource as FrameworkElement));
        private void OnLeaveSeleccionar(object sender, MouseEventArgs e) => ChangeLeaveColor(btnSeleccionar, seleccionarText, GetSource(e.OriginalSource as FrameworkElement));

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

            if (usuarios != null)
            {
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
        }

        public List<UsuarioGrid> ConvertToUsuarioGrid(List<UsuarioDTO> usuarios)
        {
            List<UsuarioGrid> usuariosGrid = new List<UsuarioGrid>();
            if (usuarios != null)
            {
                foreach (UsuarioDTO usuario in usuarios)
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
            else
            {
                UsuarioGrid defaultGrid = new UsuarioGrid();
                usuariosGrid.Add(defaultGrid.GetDefaultGrid());
                return usuariosGrid;
            }
        }

        private void EliminarUsuario(object sender, RoutedEventArgs e)
        {
            if (dataGridUsuarios.SelectedItem == null) return;
            if (MessageBox.Show("¿Esta seguro que desea eliminar a este usuario?", "¡Atención!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                usuarioController = new UsuarioController();
                UsuarioGrid selectedUser = dataGridUsuarios.SelectedItem as UsuarioGrid;
                int id = Convert.ToInt32(selectedUser.id);
                bool removed = usuarioController.EliminarUsuario(id);
                if (removed) FillDataGridUsuarios();
            }
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

        private void OpenNuevoUsuario(object sender, RoutedEventArgs e)
        {
            NuevoUsuario nuevoUsuarioWin = new NuevoUsuario(this);
            nuevoUsuarioWin.Show();
        }

        // CHANGE COLORS METHODS
        public void ChangeHoverColor(Tile tile, TextBlock text, string source, PackIconFontAwesome icon = null)
        {
            if (source.Equals("btnSeleccionar"))
            {
                tile.Background = UIColors.Blue;
                text.Foreground = UIColors.White;
            }
            else if (source.Equals("btnNuevoUsuario") || source.Equals("btnVolver"))
            {
                tile.Background = UIColors.NormalGreen;
                text.Foreground = UIColors.White;
                if (icon != null) icon.Foreground = UIColors.White;
            }
            else if (source.Equals("btnEliminar"))
            {
                tile.Background = UIColors.Red;
                text.Foreground = UIColors.White;
            }
        }

        public void ChangeLeaveColor(Tile tile, TextBlock text, string source, PackIconFontAwesome icon = null)
        {
            tile.Background = UIColors.White;
            if (source.Equals("btnSeleccionar"))
                text.Foreground = UIColors.Blue;
            else if (source.Equals("btnNuevoUsuario") || source.Equals("btnVolver"))
                text.Foreground = UIColors.NormalGreen;
                if (icon != null) icon.Foreground = UIColors.NormalGreen;
            else if (source.Equals("btnEliminar"))
                text.Foreground = UIColors.Red;
        }

    }
}
