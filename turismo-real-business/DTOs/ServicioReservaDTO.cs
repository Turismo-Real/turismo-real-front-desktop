namespace turismo_real_business.DTOs
{
    public class ServicioReservaDTO
    { 
        public ServicioReservaDTO() { }
        public ServicioReservaDTO(ServicioDTO servicio)
        {
            id = null;
            idServicio = servicio.idServicio;
            this.servicio = servicio.nombre;
            tipo = servicio.tipo;
            valor = servicio.valor;
            conductor = null;
        }

        public int? id { get; set; }
        public int idServicio { get; set; }
        public string servicio { get; set; }
        public string tipo { get; set; }
        public double valor { get; set; }
        public int? conductor { get; set; }
    }
}
