using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{
    public partial class Album
    {
        public int Album_ID { get; set; }
        public string? Titulo { get; set; }
        public string? Fecha_publicacion { get; set; }

        // Relación con canciones
        public List<Cancion>? Canciones { get; set; }
    }

}
