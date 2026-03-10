using Proyecto_Musica_GHDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{

        public partial class Playlist
        {
            public int Playlist_ID { get; set; }
            public string? Nombre { get; set; }
            public string? Fecha_creacion { get; set; }
            public int Usuario_ID { get; set; } // FK hacia Usuario

            // Propiedad de navegación opcional para enlazar con Usuario
            public Usuario? Usuario { get; set; }

            // Relación con canciones
            public List<RelacionListaCancion>? Canciones { get; set; }
        }
}




