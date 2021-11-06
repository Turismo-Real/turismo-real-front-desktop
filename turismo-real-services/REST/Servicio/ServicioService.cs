using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using turismo_real_business.DTOs;
using turismo_real_services.Utils;

namespace turismo_real_services.REST.Servicio
{
    public class ServicioService
    {
        public List<ServicioDTO> GetAllServicios()
        {
            try
            {
                WebRequest request = WebRequest.Create(URLService.URL_SERVICIOS);
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.ContentType = "Application/json; Charset=UTF-8";
                request.Timeout = 8000;

                string result = "";
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                dynamic response = JsonConvert.DeserializeObject(result);
                List<ServicioDTO> servicioResponse = ParseServiciosResponse(response);
                return servicioResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ServicioDTO GetServicioById(int id)
        {
            try
            {
                WebRequest request = WebRequest.Create($"{URLService.URL_SERVICIOS}/{id}");
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.ContentType = "Application/json; Charset=UTF-8";
                request.Timeout = 8000;

                string result = "";
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                dynamic response = JsonConvert.DeserializeObject(result);
                ServicioDTO servicioResponse = DynamicToServicio(response);
                return servicioResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool CreateServicio(ServicioDTO nuevoServicio)
        {
            try
            {
                string json = JsonConvert.SerializeObject(nuevoServicio);

                WebRequest request = WebRequest.Create(URLService.URL_SERVICIOS);
                request.Method = "POST";
                request.PreAuthenticate = true;
                request.ContentType = "Application/json; Charset=UTF-8";
                request.Timeout = 8000;

                using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                string result = "";
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                dynamic response = JsonConvert.DeserializeObject(result);
                bool createResponse = response["saved"];
                return createResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<ServicioDTO> ParseServiciosResponse(dynamic obj)
        {
            List<ServicioDTO> servicios = new List<ServicioDTO>();
            foreach (dynamic servicioJSON in obj)
            {
                ServicioDTO servicio = DynamicToServicio(servicioJSON);
                servicios.Add(servicio);
            }
            return servicios;
        }

        public ServicioDTO DynamicToServicio(dynamic servicioJSON)
        {
            ServicioDTO servicio = new ServicioDTO();
            servicio.idServicio = servicioJSON["idServicio"];
            servicio.nombre = servicioJSON["nombre"];
            servicio.descripcion = servicioJSON["descripcion"];
            servicio.valor = servicioJSON["valor"];
            servicio.tipo = servicioJSON["tipo"];
            return servicio;
        }



    }
}
