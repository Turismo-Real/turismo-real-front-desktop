using System;
using System.Collections.Generic;

namespace turismo_real_business.DTOs
{
    public class ReservaDTO
    {
        public int idReserva { get; set; }
        public DateTime fecHoraReserva { get; set; }
        public DateTime fecDesde { get; set; }
        public DateTime fecHasta { get; set; }
        public double valorArriendo { get; set; }
        public DateTime? fecHoraCheckIn { get; set; }
        public DateTime? fecHoraCheckOut { get; set; }
        public string estadoCheckIn { get; set; }
        public string estadoCheckOut { get; set; }
        public string estadoReserva { get; set; }
        public int idUsuario { get; set; }
        public int idDepartamento { get; set; }
        public List<ServicioReservaDTO> servicios { get; set; }
        public List<AsistenteDTO> asistentes { get; set; }

        public double GetDiasArriendo() => (fecHasta - fecDesde).TotalDays;

        public double GetTotalServicios()
        {
            double totalServicios = 0;
            foreach (ServicioReservaDTO servicio in servicios) totalServicios += servicio.valor;
            return totalServicios;
        }

        public double GetTotalArriendo(double valorArriendoDiario)
        {
            valorArriendo = (valorArriendoDiario * GetDiasArriendo()) + GetTotalServicios();
            return valorArriendo;
        }
    }
}
