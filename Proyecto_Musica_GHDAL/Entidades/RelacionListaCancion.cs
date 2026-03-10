using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{
    public partial class RelacionListaCancion
    {
        public int LC_REL_ID { get; set; }
        public int Playlist_ID { get; set; } // FK hacia Playlist
        public int Cancion_ID { get; set; }  // FK hacia Cancion
        public int? Orden { get; set; }     // Puede ser nulo si no se define orden

        // Propiedades de navegación opcionales
        public Playlist? Playlist { get; set; }
        public Cancion? Cancion { get; set; }
    }

}
