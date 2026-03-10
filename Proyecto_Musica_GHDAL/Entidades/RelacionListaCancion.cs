using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{
    public partial class RelacionListaCancion
    {
        public int LCRelId { get; set; }
        public int PlaylistId { get; set; } // FK hacia Playlist
        public int CancionId { get; set; }  // FK hacia Cancion
        public int? Orden { get; set; }     // Puede ser nulo si no se define orden

        // Propiedades de navegación opcionales
        public Playlist? Playlist { get; set; }
        public Cancion? Cancion { get; set; }
    }

}
