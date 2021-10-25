using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.Utils;

namespace turismo_real_services.REST.Usuario
{
    public class UsuarioService
    {
        public UsuarioDTO GetUsuarioById(int id)
        {
            try
            {
                WebRequest request = WebRequest.Create(URLService.URL_USUARIO_GET_BY_ID + id);
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.ContentType = "Application/json; Charset=UTF-8";
                request.Timeout = 5000;

                string result = "";
                HttpWebResponse httpResponse = (HttpWebResponse) request.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                dynamic response = JsonConvert.DeserializeObject(result);
                UsuarioDTO userResponse = ParseUsuarioResponse(response);
                return userResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public UsuarioDTO ParseUsuarioResponse(dynamic obj)
        {
            UsuarioDTO usuario = new UsuarioDTO();
            usuario.idUsuario = obj["id"];
            usuario.pasaporte = obj["pasaporte"];
            usuario.rut = obj["rut"];
            usuario.dv = obj["dv"];
            usuario.primerNombre = obj["primerNombre"];
            usuario.segundoNombre = obj["segundoNombre"];
            usuario.primerApellido = obj["apellidoPaterno"];
            usuario.segundoApellido = obj["apellidoMaterno"];
            return usuario;
        }
    }
}
