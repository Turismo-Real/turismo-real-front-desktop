using System;
using System.Collections.Generic;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.REST.Servicio;

namespace turismo_real_controller.Controllers.Servicio
{
    public class ServicioController
    {
        public ServicioService servicioService;

        public List<ServicioDTO> ObtenerServicios()
        {
            servicioService = new ServicioService();
            List<ServicioDTO> servicios = servicioService.GetAllServicios();
            return servicios;
        }

        public bool AgregarServicio(ServicioDTO servicio)
        {
            servicioService = new ServicioService();
            bool saved = servicioService.CreateServicio(servicio);
            return saved;
        }



    }
}
