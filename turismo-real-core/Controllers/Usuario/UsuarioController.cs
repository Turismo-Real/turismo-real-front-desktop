using System;
using System.Collections.Generic;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.REST.Usuario;

namespace turismo_real_controller.Controllers.Usuario
{
    public class UsuarioController
    {
        public UsuarioDTO GetUsuario(int id)
        {
            return new UsuarioService().GetUsuarioById(id);
        }
    }
}
