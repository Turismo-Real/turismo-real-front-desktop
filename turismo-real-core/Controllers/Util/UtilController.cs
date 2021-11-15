using System;
using System.Collections.Generic;
using turismo_real_services.REST.Util;

namespace turismo_real_controller.Controllers.Util
{
    public class UtilController
    {
        private UtilService utilService;

        public List<string> ObtenerRegiones()
        {
            string defaultValue = "-- Selecciona una Región --";
            utilService = new UtilService();
            List<string> regiones = utilService.GetRegionesREST();

            if (regiones != null)
            {
                regiones.Insert(0, defaultValue);
                regiones.RemoveAt(regiones.Count - 1);
            }
            else
            {
                regiones = new List<string>() { defaultValue };
            }
            return regiones;     
        }

        public List<string> ObtenerComunasPorRegion(string region)
        {
            utilService = new UtilService();
            List<string> comunas = utilService.GetComunasREST(region);
            comunas.Insert(0, "-- Selecciona una Comuna --");

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
            string defaultValue = "-- Tipo Departamento --";
            utilService = new UtilService();
            List<string> tiposDepto = utilService.GetTiposDeptoREST();

            if (tiposDepto != null)
            {
                tiposDepto.Insert(0, defaultValue);
                return tiposDepto;
            }
            tiposDepto = new List<string>() { defaultValue };
            return tiposDepto;
        }

        public List<string> ObtenerEstadosDepto()
        {
            string defaultValue = "-- Estado --";
            try
            {
                utilService = new UtilService();
                List<string> estadosDepto = utilService.GetEstadosDeptoREST();
                estadosDepto.Insert(0, defaultValue);
                return estadosDepto;
            } catch(NullReferenceException ex)
            {
                List<string> defaultCombo = new List<string>();
                defaultCombo.Insert(0, defaultValue);
                return defaultCombo;
            }
        }

        public List<string> ObtenerGeneros()
        {
            List<string> generos = new List<string> { "-- Genero --", "Femenino", "Masculino" };
            return generos;
        }

        public List<string> ObtenerPaises()
        {
            string defaultValue = "-- Seleccionar País --";
            utilService = new UtilService();
            List<string> paises = utilService.GetPaisesREST();

            if (paises != null)
                paises.Insert(0, defaultValue);
            else
                paises = new List<string>() { defaultValue };
            return paises;
        }

        public List<string> ObtenerTiposUsuarios()
        {
            List<string> tiposUsuarios = new List<string> { "-- Tipo Usuario --", "Administrador", "Funcionario", "Cliente" };
            return tiposUsuarios;
        }

        public List<string> ObtenerTiposServicios()
        {
            string defaultValue = "-- Tipo Servicio --";
            utilService = new UtilService();
            List<string> tiposServicios = utilService.GetTiposServicioREST();

            if (tiposServicios != null)
            {
                tiposServicios.Insert(0, defaultValue);
                return tiposServicios;
            }
            tiposServicios = new List<string>() { defaultValue };
            return tiposServicios;
        }
    }
}
