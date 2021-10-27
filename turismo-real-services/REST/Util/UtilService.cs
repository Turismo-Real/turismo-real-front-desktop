using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using turismo_real_services.Utils;

namespace turismo_real_services.REST.Util
{
    public class UtilService
    {
        public List<string> GetRegionesREST()
        {
            try
            {
                WebRequest request = WebRequest.Create($"{URLService.URL_UTILS}/region");
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
                List<string> regionesResponse = ParseStrObjectsToList(response);
                return regionesResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }



            List<string> regiones = new List<string>();
            regiones.Add("Region Metropolitana");
            regiones.Add("Coquimbo");
            regiones.Add("Atacama");
            return regiones;

        }

        public List<string> ParseStrObjectsToList(dynamic objResponse)
        {
            List<string> strObjects = new List<string>();
            foreach(string strObject in objResponse)
            {
                strObjects.Add(strObject);
            }
            return strObjects;
        }

        public List<string> GetComunasREST(string region)
        {
            try
            {
                Trace.WriteLine("Region en service: " + region);
                object payload = new { region = region };
                string json = JsonConvert.SerializeObject(payload);

                Trace.WriteLine("Payload: " + payload);

                string url = $"{URLService.URL_UTILS}/comuna";
                Trace.WriteLine(url);

                WebRequest request = WebRequest.Create(url);
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
                Trace.WriteLine("Result: "+result);
                dynamic response = JsonConvert.DeserializeObject(result);
                List<string> regionesResponse = ParseStrObjectsToList(response);
                Trace.WriteLine("Cant regiones: "+regionesResponse.Count);
                return regionesResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }



        }





    }
}
