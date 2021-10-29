using System.Collections.Generic;
using turismo_real_business.DTOs;
using System.Linq;
using turismo_real_services.REST.Departamento;
using System.Diagnostics;

namespace turismo_real_controller.Controllers.Departamento
{
    public class DepartamentoController
    {
        private DepartamentoService deptoService;
        private List<DepartamentoDTO> deptos;

        public List<DepartamentoDTO> ObtenerDepartamentos()
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

        public bool EliminarDepto(int id)
        {
            deptoService = new DepartamentoService();
            bool removed = deptoService.DeleteDepto(id);
            return removed;
        }

        public DepartamentoDTO ObtenerDepartamento(int id)
        {
            deptoService = new DepartamentoService();
            DepartamentoDTO depto = deptoService.GetDeptoById(id);
            return depto;  
        }
    }
}
