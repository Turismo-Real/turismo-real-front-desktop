using System.Collections.Generic;
using turismo_real_business.DTOs;
using turismo_real_services.REST.Reserva;

namespace turismo_real_controller.Controllers.Reserva
{
    public class ReservaController
    {
        private ReservaService reservaService;

        public List<ReservaDTO> ObtenerReservas()
        {
            reservaService = new ReservaService();
            List<ReservaDTO> reservas = reservaService.GetReservas();
            return reservas;
        }

        public ReservaDTO NuevaReserva(ReservaDTO reserva)
        {
            reservaService = new ReservaService();
            ReservaDTO saved = reservaService.CreateReserva(reserva);
            return saved;
        }
    }
}
 