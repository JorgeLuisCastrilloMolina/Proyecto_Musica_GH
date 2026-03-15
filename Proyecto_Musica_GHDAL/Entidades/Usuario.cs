using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Entidades
{
    public partial class Usuario
    {
        public int Usuario_ID { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }


        // Relación con playlists
        public List<Playlist>? Playlists { get; set; }
    }

}
