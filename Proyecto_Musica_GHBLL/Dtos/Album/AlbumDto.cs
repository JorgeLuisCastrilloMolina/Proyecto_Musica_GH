using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_Musica_GHBLL.Dtos.Album
{
    public class AlbumDto
    {
        public int Album_ID { get; set; }

        [Required(ErrorMessage = "El título del álbum es obligatorio")]
        public string Titulo { get; set; }

        public string? Fecha_publicacion { get; set; }
        public int Artista_ID { get; set; }
        public string? ArtistaNombre { get; set; }

        // Solo lectura para mostrar canciones asociadas
        public List<string>? Canciones { get; set; }
    }

}
