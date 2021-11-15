namespace turismo_real_desktop.GridEntities
{
    public class UsuarioGrid
    {
        public int id { get; set; }
        public string rut { get; set; }
        public string pasaporte { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string pais { get; set; }
        public string tipo { get; set; }

        public UsuarioGrid GetDefaultGrid()
        {
            UsuarioGrid defaultGrid = new UsuarioGrid();
            defaultGrid.id = 0;
            defaultGrid.rut = "12345678-9";
            defaultGrid.pasaporte= "00000000";
            defaultGrid.nombre = "AAAA BBBB CCCC ";
            defaultGrid.email = "default@correo.cl";
            defaultGrid.pasaporte = "Chile";
            defaultGrid.tipo = "Default";
            return defaultGrid;
        }
    }
}
