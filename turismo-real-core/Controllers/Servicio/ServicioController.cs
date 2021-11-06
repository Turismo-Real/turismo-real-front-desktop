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

        public ServicioDTO ObtenerServicio(int idServicio)
        {
            servicioService = new ServicioService();
            ServicioDTO servicio = servicioService.GetServicioById(idServicio);
            return servicio;
        }

        public ServicioDTO ActualizarServicio(ServicioDTO servicio)
        {
            servicioService = new ServicioService();
            ServicioDTO updatedServicio = servicioService.UpdateServicio(servicio);
            return updatedServicio;
        }

        public bool EliminarServicio(int idServicio)
        {
            servicioService = new ServicioService();
            bool removed = servicioService.DeleteServicio(idServicio);
            return removed;
        }

    }
}
