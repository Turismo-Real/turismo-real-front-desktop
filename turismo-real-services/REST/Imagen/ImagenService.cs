using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using turismo_real_business.DTOs;
using turismo_real_business.Messages;
using turismo_real_services.Utils;

namespace turismo_real_services.REST.Imagen
{
    public class ImagenService
    {
        public ImagenesDeptoDTO GetImages(int id)
        {
            WebRequest request = WebRequest.Create($"{URLService.URL_IMAGENES}/{id}");
            request.Method = "GET";
            request.PreAuthenticate = true;
            request.ContentType = "Application/json; Charset=UTF-8";
            request.Timeout = 10000;

            string result = string.Empty;
            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            dynamic response = JsonConvert.DeserializeObject(result);
            ImagenesDeptoDTO imagenResponse = ConvertToImagenesDepto(response);
            return imagenResponse;
        }

        public bool AddImage(ImagenDeptoPayload imagen)
        {
            string json = JsonConvert.SerializeObject(imagen);
            WebRequest request = WebRequest.Create(URLService.URL_IMAGENES);
            request.Method = "POST";
            request.PreAuthenticate = true;
            request.ContentType = "Application/json; Charset=UTF-8";
            request.Timeout = 8000;

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            string result = string.Empty;
            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            dynamic response = JsonConvert.DeserializeObject(result);
            int createResponse = response["imagenId"];
            bool saved = createResponse > 0 ? true : false;
            return saved;
        }

        public bool DeleteImage(int id)
        {
            try
            {
                WebRequest request = WebRequest.Create($"{URLService.URL_IMAGENES}/{id}");
                request.Method = "DELETE";
                request.PreAuthenticate = true;
                request.ContentType = "Application/json; Charset=UTF-8";
                request.Timeout = 8000;

                string result = string.Empty;
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

        private ImagenesDeptoDTO ConvertToImagenesDepto(dynamic imagenesREST)
        {
            ImagenesDeptoDTO imagenesDepto = new ImagenesDeptoDTO();
            imagenesDepto.idDepartamento = imagenesREST["idDepartamento"];
            imagenesDepto.imagenes = DynamicToImagenes(imagenesREST["imagenes"]);
            return imagenesDepto;
        }

        private List<ImagenDTO> DynamicToImagenes(dynamic imagenes)
        {
            List<ImagenDTO> imagenesList = new List<ImagenDTO>();
            foreach (dynamic imagen in imagenes)
            {
                ImagenDTO img = new ImagenDTO();
                img.idImagen = imagen["idImagen"];
                img.nombre = imagen["nombre"];
                img.formato = imagen["formato"];
                img.imagen = imagen["imagen"];
                imagenesList.Add(img);
            }
            return imagenesList;
        }

    }
}
