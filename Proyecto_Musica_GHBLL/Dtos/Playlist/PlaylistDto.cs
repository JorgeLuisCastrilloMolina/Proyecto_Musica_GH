using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_Musica_GHBLL.Dtos.Playlist
{
    public class PlaylistDto
    {
        public int Playlist_ID { get; set; }

        [Required(ErrorMessage = "El nombre de la playlist es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        public string Fecha_creacion { get; set; }

        [Required(ErrorMessage = "Debe asignar un usuario")]
        public int Usuario_ID { get; set; }

        // Solo lectura para mostrar en la tabla o vista
        public string UsuarioNombre { get; set; }
        public int CancionesCount { get; set; }
    }

}
