using System.Collections.Generic;
using turismo_real_business.DTOs;
using turismo_real_services.REST.Departamento;

namespace turismo_real_controller.Controllers.Departamento
{
    public class DepartamentoController
    {
        private DepartamentoService deptoService;
        private List<DepartamentoDTO> deptos;

        public List<DepartamentoDTO> GetDepartamentos()
        {
            deptos = new DepartamentoService().GetAllDeptos();
            return deptos;
        }

        public bool AgregarNuevoDepto(DepartamentoDTO nuevoDepto)
        {
            deptoService = new DepartamentoService();
            bool saved = deptoService.CreateNewDepto(nuevoDepto);
            return saved;
        }
    }
}
