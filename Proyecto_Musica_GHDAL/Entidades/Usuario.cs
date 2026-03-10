using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{
    public partial class Usuario
    {
        public int Usuario_ID { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Contraseña { get; set; }

        // Relación con playlists
        public List<Playlist>? Playlists { get; set; }
    }

}
