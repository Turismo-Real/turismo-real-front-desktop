using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using turismo_real_business.DTOs;
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
