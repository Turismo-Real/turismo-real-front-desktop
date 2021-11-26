using turismo_real_business.DTOs;
using turismo_real_business.Messages;
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

        public bool AgregarImagen(int id, string nombre, string formato, string imagen)
        {
            ImagenDeptoPayload payload = new ImagenDeptoPayload(id, nombre, formato, imagen);
            imagenService = new ImagenService();
            bool saved = imagenService.AddImage(payload);
            return saved;
        }

        public bool EliminarImagen(int id)
        {
            imagenService = new ImagenService();
            bool removed = imagenService.DeleteImage(id);
            return removed;
        }
    }
}
