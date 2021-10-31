using System;
using System.Collections.Generic;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.REST.Usuario;

namespace turismo_real_controller.Controllers.Usuario
{
    public class UsuarioController
    {
        private UsuarioService usuarioService;

        public UsuarioDTO ObtenerUsuario(int id)
        {
            usuarioService = new UsuarioService();
            return usuarioService.GetUsuarioById(id);
        }

        public List<UsuarioDTO> ObtenerUsuarios()
        {
            usuarioService = new UsuarioService();
            List<UsuarioDTO> usuarios = usuarioService.GetUsuarios();
            return usuarios;
        }

        public bool EliminarUsuario(int id)
        {
            usuarioService = new UsuarioService();
            bool removed = usuarioService.DeleteUsuario(id);
            return removed;
        }

        public bool AgregarNuevoUsuario(UsuarioDTO nuevoUsuario)
        {
            usuarioService = new UsuarioService();
            bool saved = usuarioService.CreateUser(nuevoUsuario);
            return saved;
        }

        public string ObtenerDefaultPasswordPorTipo(string userType)
        {
            string defaultPassword = string.Empty;
            userType = userType.ToUpper();
            if (userType.Equals("ADMINISTRADOR")) defaultPassword = "administrar";
            if (userType.Equals("FUNCIONARIO")) defaultPassword = "checkin";
            if (userType.Equals("CLIENTE")) defaultPassword = "turismo";
            return defaultPassword;
        }
    }
}
