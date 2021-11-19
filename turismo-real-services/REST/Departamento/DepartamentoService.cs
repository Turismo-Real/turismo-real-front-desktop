using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
                DepartamentoDTO depto = DynamicToDepto(deptoJSON);
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

        public bool DeleteDepto(int id)
        {
            try
            {
                WebRequest request = WebRequest.Create($"{URLService.URL_DEPTOS}/{id}");
                request.Method = "DELETE";
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
                bool deleteResponse = response["removed"];
                return deleteResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public DepartamentoDTO GetDeptoById(int id)
        {
            try
            {
                WebRequest request = WebRequest.Create($"{URLService.URL_DEPTOS}/{id}");
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
                DepartamentoDTO deptoResponse = DynamicToDepto(response);
                return deptoResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public DepartamentoDTO UpdateDepto(DepartamentoDTO depto)
        {
            string json = JsonConvert.SerializeObject(depto);
            WebRequest request = WebRequest.Create($"{URLService.URL_DEPTOS}/{depto.id_departamento}");
            request.Method = "PUT";
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
            bool updated = response["updated"];

            if (updated && response["departamento"]["id_departamento"] != 0)
            {
                DepartamentoDTO deptoResponse = DynamicToDepto(response["departamento"]);
                return deptoResponse;
            }
            return null;
        }

        public DepartamentoDTO DynamicToDepto(dynamic deptoJSON)
        {
            DepartamentoDTO depto = new DepartamentoDTO();
            depto.id_departamento = deptoJSON["id_departamento"];
            depto.rol = deptoJSON["rol"];
            depto.estado = deptoJSON["estado"];
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
            depto.instalaciones = ParseStrObjectsToList(deptoJSON["instalaciones"]);
            return depto;
        }

        public List<string> ParseStrObjectsToList(dynamic objResponse)
        {
            List<string> strObjects = new List<string>();
            foreach (string strObject in objResponse)
            {
                strObjects.Add(strObject);
            }
            return strObjects;
        }
    }
}
