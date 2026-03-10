using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{
    public partial class Cancion
    {
        public int CancionId { get; set; }
        public string? Titulo { get; set; }
        public string? FechaPublicacion { get; set; }
        public int? Duracion { get; set; }   // Puede ser nulo si no se registra duración
        public string? URLCancion { get; set; }
        public int AlbumId { get; set; } // FK hacia Album

        // Propiedad de navegación opcional para enlazar con Album
        public Album? Album { get; set; }
    }

}
