using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using turismo_real_services.REST.Util;

namespace turismo_real_controller.Controllers.Util
{
    public class UtilController
    {
        private UtilService utilService;

        public List<string> ObtenerRegiones()
        {
            utilService = new UtilService();
            List<string> regiones = utilService.GetRegionesREST();
            regiones.Insert(0, "-- Selecciona una Región --");
            regiones.RemoveAt(regiones.Count-1);

            return regiones;
        }

        public List<string> ObtenerComunasPorRegion(string region)
        {
            utilService = new UtilService();
            List<string> comunas = utilService.GetComunasREST(region);
            Trace.WriteLine("REGION: "+region);
            Trace.WriteLine("------");
            Trace.WriteLine("Cant comunas: "+comunas.Count);
            comunas.Insert(0, "-- Selecciona una Región --");

            return comunas;
        }

        public List<string> ObtenerInstalaciones()
        {
            utilService = new UtilService();
            List<string> instalaciones = utilService.GetInstalacionesREST();
            return instalaciones;
        }
    }
}
