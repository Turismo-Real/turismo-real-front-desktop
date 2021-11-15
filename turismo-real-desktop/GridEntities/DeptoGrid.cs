using System.ComponentModel.DataAnnotations.Schema;

namespace turismo_real_desktop.GridEntities
{
    public class DeptoGrid
    {
        public int id { get; set; }
        public string rol { get; set; }
        public string tipo { get; set; }
        public string superficie { get; set; }
        public string valorDiario { get; set; }
        public string comuna { get; set; }
        public string region { get; set; }
        public string estado { get; set; }

        public DeptoGrid GetDefaultGrid()
        {
            DeptoGrid defaultGrid = new DeptoGrid();
            defaultGrid.id = 0;
            defaultGrid.rol = "000000";
            defaultGrid.tipo = "Default";
            defaultGrid.superficie = "0";
            defaultGrid.valorDiario = "0";
            defaultGrid.comuna = "Default";
            defaultGrid.region = "Default";
            defaultGrid.estado = "Default";
            return defaultGrid;
        }
    }
}
