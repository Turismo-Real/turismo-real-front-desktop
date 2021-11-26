using System;
using System.Collections.Generic;
using System.Text;

namespace turismo_real_business.Messages
{
    public class ImagenDeptoPayload
    {
        public ImagenDeptoPayload(int id, string nombre, string formato, string b64)
        {
            idDepartamento = id;
            this.nombre = nombre;
            this.formato = formato;
            b64imagen = b64;
        }

        public int idDepartamento { get; set; }
        public string nombre { get; set; }
        public string formato { get; set; }
        public string b64imagen { get; set; }
    }
}
