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
using turismo_real_services.REST.Usuario;

namespace turismo_real_desktop.Views.Usuarios
{

    public partial class Usuario : MetroWindow
    {
        private UsuarioService usuarioService;
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
            usuarioService = new UsuarioService();
            selectedUsuario = usuarioService.GetUsuarioById(idUsuario);
            SetInitialStateGrids();
            SetUsuarioForm(selectedUsuario);
        }

        public void SetInitialStateGrids()
        {
            gridVerUsuario.Visibility = Visibility.Visible;
            gridEditarUsuario.Visibility = Visibility.Hidden;
        }

        public void SetUsuarioForm(UsuarioDTO selectedUsuario)
        {

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
    }
}
