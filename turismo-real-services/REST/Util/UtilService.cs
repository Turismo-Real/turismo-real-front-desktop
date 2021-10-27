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
                object payload = new { region = region };
                string json = JsonConvert.SerializeObject(payload);

                string url = $"{URLService.URL_UTILS}/comuna";
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

                dynamic response = JsonConvert.DeserializeObject(result);
                List<string> regionesResponse = ParseStrObjectsToList(response);
                return regionesResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<string> GetInstalacionesREST()
        {
            try
            {
                string url = $"{URLService.URL_UTILS}/instalaciones";
                WebRequest request = WebRequest.Create(url);
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
                List<string> instalaciones = ParseStrObjectsToList(response);
                return instalaciones;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
