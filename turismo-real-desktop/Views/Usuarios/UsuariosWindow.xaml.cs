using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using turismo_real_controller.Controllers.Usuario;
using turismo_real_desktop.GridEntities;
using turismo_real_desktop.Views.Administrador;

namespace turismo_real_desktop.Views.Usuarios
{
    public partial class UsuariosWindow : MetroWindow
    {
        private UsuarioController usuarioController;
        private List<string> usuarios;

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
            contadorUsuarios.Text = $"Total departamentos: {usuarios.Count}";
        }

        public List<UsuarioGrid> ConvertToUsuarioGrid(List<string> usuarios)
        {
            return new List<UsuarioGrid>();
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

        }

        private void OnLeaveSeleccionar(object sender, MouseEventArgs e)
        {

        }

        private void OnHoverSeleccionar(object sender, MouseEventArgs e)
        {

        }

        private void SeleccionarUsuario(object sender, RoutedEventArgs e)
        {
            Usuario selectedUsuarioWin = new Usuario();
            selectedUsuarioWin.Show();
        }

        private void OnHoverNuevoDepto(object sender, MouseEventArgs e)
        {

        }

        private void OnLeaveNuevoDepto(object sender, MouseEventArgs e)
        {

        }

        private void OpenNuevoUsuario(object sender, RoutedEventArgs e)
        {
            NuevoUsuario nuevoUsuarioWin = new NuevoUsuario();
            nuevoUsuarioWin.Show();
        }
    }
}
