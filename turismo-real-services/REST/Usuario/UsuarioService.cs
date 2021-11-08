using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
                WebRequest request = WebRequest.Create($"{URLService.URL_USUARIOS}/{id}");
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
            usuario.apellidoPaterno = obj["apellidoPaterno"];
            usuario.apellidoMaterno = obj["apellidoMaterno"];
            usuario.fechaNacimiento = obj["fechaNacimiento"];
            usuario.correo = obj["correo"];
            usuario.telefonoMovil = obj["telefonoMovil"];
            usuario.telefonoFijo = obj["telefonoFijo"];
            usuario.genero = obj["genero"];
            usuario.pais = obj["pais"];
            usuario.tipoUsuario = obj["tipoUsuario"];

            DireccionDTO direccion = new DireccionDTO();
            direccion.region = obj["direccion"]["region"];
            direccion.comuna = obj["direccion"]["comuna"];
            direccion.calle = obj["direccion"]["calle"];
            direccion.numero = obj["direccion"]["numero"];
            direccion.depto = obj["direccion"]["depto"];
            direccion.casa = obj["direccion"]["casa"];
            usuario.direccion = direccion;

            return usuario;
        }

        public List<UsuarioDTO> ParseUsuariosResponse(dynamic obj)
        {
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();
            foreach (dynamic usuarioJSON in obj)
            {
                UsuarioDTO usuario = DynamicToUsuario(usuarioJSON);
                usuarios.Add(usuario);
            }
            return usuarios;
        }

        public UsuarioDTO DynamicToUsuario(dynamic usuarioJSON)
        {
            UsuarioDTO usuario = new UsuarioDTO();
            usuario.idUsuario = usuarioJSON["id"];
            usuario.pasaporte = usuarioJSON["pasaporte"];
            usuario.rut = usuarioJSON["rut"];
            usuario.dv= usuarioJSON["dv"];
            usuario.primerNombre= usuarioJSON["primerNombre"];
            usuario.segundoNombre = usuarioJSON["segundoNombre"];
            usuario.apellidoPaterno = usuarioJSON["apellidoPaterno"];
            usuario.apellidoMaterno = usuarioJSON["apellidoMaterno"];
            usuario.fechaNacimiento = usuarioJSON["fechaNacimiento"];
            usuario.correo = usuarioJSON["correo"];
            usuario.telefonoMovil = usuarioJSON["telefonoMovil"];
            usuario.telefonoFijo = usuarioJSON["telefonoFijo"];
            usuario.genero = usuarioJSON["genero"];
            usuario.pais = usuarioJSON["pais"];
            usuario.tipoUsuario = usuarioJSON["tipoUsuario"];

            DireccionDTO direccion = new DireccionDTO();
            direccion.region = usuarioJSON["direccion"]["region"];
            direccion.comuna = usuarioJSON["direccion"]["comuna"];
            direccion.calle = usuarioJSON["direccion"]["calle"];
            direccion.numero = usuarioJSON["direccion"]["numero"];
            direccion.depto = usuarioJSON["direccion"]["depto"];
            direccion.casa = usuarioJSON["direccion"]["casa"];
            usuario.direccion = direccion;
            return usuario;
        }

        public List<UsuarioDTO> GetUsuarios()
        {
            try
            {
                WebRequest request = WebRequest.Create(URLService.URL_USUARIOS);
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
                List<UsuarioDTO> userResponse = ParseUsuariosResponse(response);
                return userResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool DeleteUsuario(int id)
        {
            try
            {
                WebRequest request = WebRequest.Create($"{URLService.URL_USUARIOS}/{id}");
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

        public bool CreateUser(UsuarioDTO usuario)
        {
            try
            {
                string json = JsonConvert.SerializeObject(usuario);

                WebRequest request = WebRequest.Create(URLService.URL_USUARIOS);
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

        public UsuarioDTO UpdateUsuario(UsuarioDTO usuario)
        {
            string json = JsonConvert.SerializeObject(usuario);
            WebRequest request = WebRequest.Create($"{URLService.URL_USUARIOS}/{usuario.idUsuario}");
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

            if (updated)
            {
                UsuarioDTO usuarioResponse = DynamicToUsuario(response["usuario"]);
                return usuarioResponse;
            }
            return null;
        }

        public bool UpdatePassword(int id, string currentPassword, string newPassword)
        {
            var payload = new { currentPassword, newPassword };
            string json = JsonConvert.SerializeObject(payload);
            WebRequest request = WebRequest.Create($"{URLService.URL_USUARIOS}/{id}");
            request.Method = "PATCH";
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
            return updated;
        }

    }
}
