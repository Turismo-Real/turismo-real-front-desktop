using System.Collections.Generic;
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
            comunas.Insert(0, "-- Selecciona una Región --");

            return comunas;
        }

        public List<string> ObtenerInstalaciones()
        {
            utilService = new UtilService();
            List<string> instalaciones = utilService.GetInstalacionesREST();
            return instalaciones;
        }

        public List<string> ObtenerTiposDepto()
        {
            utilService = new UtilService();
            List<string> tiposDepto = utilService.GetTiposDeptoREST();
            tiposDepto.Insert(0, " -- Tipo Departamento --");
            return tiposDepto;
        }

        public List<string> ObtenerEstadosDepto()
        {
            utilService = new UtilService();
            List<string> estadosDepto = utilService.GetEstadosDeptoREST();
            estadosDepto.Insert(0, "-- Estado --");
            return estadosDepto;
        }

        public List<string> ObtenerGeneros()
        {
            List<string> generos = new List<string> { "-- Genero --", "Femenino", "Masculino" };
            return generos;
        }

        public List<string> ObtenerPaises()
        {
            utilService = new UtilService();
            List<string> paises = utilService.GetPaisesREST();
            paises.Insert(0, "-- Seleccionar País --");
            return paises;
        }

        public List<string> ObtenerTiposUsuarios()
        {
            List<string> tiposUsuarios = new List<string> { "-- Tipo Usuario --", "Administrador", "Funcionario", "Cliente" };
            return tiposUsuarios;
        }
    }
}
