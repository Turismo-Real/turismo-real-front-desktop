using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.Utils;

namespace turismo_real_services.REST.Departamento
{
    public class DepartamentoService
    {
        public List<DepartamentoDTO> GetAllDeptos()
        {
            try
            {
                WebRequest request = WebRequest.Create(URLService.URL_DEPTOS);
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.ContentType = "Application/json; Charset=UTF-8";
                request.Timeout = 5000;

                string result = "";
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                dynamic response = JsonConvert.DeserializeObject(result);
                List<DepartamentoDTO> deptoResponse = ParseDeptosResponse(response);
                return deptoResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public List<DepartamentoDTO> ParseDeptosResponse(dynamic obj)
        {
            List<DepartamentoDTO> deptos = new List<DepartamentoDTO>();
            foreach(dynamic deptoJSON in obj)
            {
                DepartamentoDTO depto = new DepartamentoDTO();
                depto.id_departamento = deptoJSON["id_departamento"];
                depto.rol = deptoJSON["rol"];
                depto.dormitorios = deptoJSON["dormitorios"];
                depto.banios = deptoJSON["banios"];
                depto.descripcion = deptoJSON["descripcion"];
                depto.superficie = deptoJSON["superficie"];
                depto.valorDiario = deptoJSON["valorDiario"];
                depto.tipo = deptoJSON["tipo"];
                depto.estado = deptoJSON["estado"];
                
                DireccionDTO direccion = new DireccionDTO();
                direccion.region = deptoJSON["direccion"]["region"];
                direccion.comuna = deptoJSON["direccion"]["comuna"];
                direccion.calle = deptoJSON["direccion"]["calle"];
                direccion.numero = deptoJSON["direccion"]["numero"];
                direccion.depto = deptoJSON["direccion"]["depto"];
                depto.direccion = direccion;

                List<string> instalaciones = new List<string>();
                foreach(string instalacion in deptoJSON["instalaciones"])
                {
                    instalaciones.Add(instalacion);
                }
                depto.instalaciones = instalaciones;
                deptos.Add(depto);
            }

            return deptos;
        }

        public bool CreateNewDepto(DepartamentoDTO newDepto)
        {
            try
            {
                string json = JsonConvert.SerializeObject(newDepto);

                WebRequest request = WebRequest.Create(URLService.URL_DEPTOS);
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
    }
}
