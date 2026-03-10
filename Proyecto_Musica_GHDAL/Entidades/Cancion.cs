using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{
   public partial class Cancion
    {
        public int Cancion_ID { get; set; }
        public string? Titulo { get; set; }
        public string? Fecha_publicacion { get; set; }
        public int? Duracion { get; set; }   // Puede ser nulo si no se registra duración
        public string? URL_cancion { get; set; }
        public int Album_ID { get; set; } // FK hacia Album

        // Propiedad de navegación opcional para enlazar con Album
        public Album? Album { get; set; }
    }

}
