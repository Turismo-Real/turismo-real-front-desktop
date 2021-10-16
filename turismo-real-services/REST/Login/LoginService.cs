using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using turismo_real_business.DTOs;
using turismo_real_business.Messages;

namespace turismo_real_services.REST.Login
{
    public class LoginService
    {
        protected const string URL_LOGIN = "http://localhost:5000/api/v1/login";

        public LoginResponse CallService(LoginDTO login)
        {
            try
            {
                string json = JsonConvert.SerializeObject(login);

                WebRequest request = WebRequest.Create(URL_LOGIN);
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
                LoginResponse loginResponse = ParseLoginResponse(response);
                return loginResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new LoginResponse("ERROR", false, String.Empty);
            }
        }

        public LoginResponse ParseLoginResponse(dynamic obj)
        {
            string mensaje = obj["mensaje"];
            bool login = obj["login"];
            string tipo = obj["tipo"];
            return new LoginResponse(mensaje,login,tipo);
        }
    }
}
