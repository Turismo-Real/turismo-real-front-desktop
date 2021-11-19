namespace turismo_real_business.DTOs
{
    public class ServicioReservaDTO
    { 
        public int id { get; set; }
        public int idServicio { get; set; }
        public string servicio { get; set; }
        public string tipo { get; set; }
        public double valor { get; set; }
        public int conductor { get; set; }
    }
}
