using System;
using System.Collections.Generic;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.REST.Departamento;

namespace turismo_real_controller.Controllers.Departamento
{
    public class DepartamentoController
    {
        private List<DepartamentoDTO> deptos;

        public List<DepartamentoDTO> GetDepartamentos()
        {
            deptos = new DepartamentoService().GetAllDeptos();
            return deptos;
        }
    }
}
