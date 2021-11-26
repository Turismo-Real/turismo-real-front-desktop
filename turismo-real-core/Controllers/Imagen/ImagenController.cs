using System;
using System.Collections.Generic;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.REST.Imagen;

namespace turismo_real_controller.Controllers.Imagen
{
    public class ImagenController
    {
        private ImagenService imagenService;

        public ImagenesDeptoDTO ObtenerImagenes(int id)
        {
            imagenService = new ImagenService();
            ImagenesDeptoDTO imagenes = imagenService.GetImages(id);
            return imagenes;
        }
    }
}
