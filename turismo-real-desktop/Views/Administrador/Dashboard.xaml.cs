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
using turismo_real_business.Messages;
using turismo_real_controller.Controllers.Usuario;

namespace turismo_real_desktop.Views.Administrador
{
    public partial class Dashboard : MetroWindow
    {
        public Dashboard(LoginResponse login)
        {
            InitializeComponent();

            // Obtener datos del usuario
            UsuarioController usuarioController = new UsuarioController();
            UsuarioDTO usuario = usuarioController.GetUsuario(login.id);

            // Crear singleton de usuario logueado
            lblWelcome.Content = $"Bienvenido {usuario.primerNombre} {usuario.primerApellido} {usuario.segundoApellido}";


        }

    }
}
