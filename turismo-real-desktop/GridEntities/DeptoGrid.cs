using System.ComponentModel.DataAnnotations.Schema;

namespace turismo_real_desktop.GridEntities
{
    public class DeptoGrid
    {
        public int id { get; set; }
        public string rol { get; set; }
        public string tipo { get; set; }
        public string superficie { get; set; }

        [Column("Valor Diario")]
        public string valorDiario { get; set; }
        public string comuna { get; set; }
        public string region { get; set; }
        public string estado { get; set; }
    }
}
