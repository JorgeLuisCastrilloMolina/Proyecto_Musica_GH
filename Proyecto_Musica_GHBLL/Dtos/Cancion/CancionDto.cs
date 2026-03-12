using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_Musica_GHBLL.Dtos.Cancion
{
    public class CancionDto
    {
        public int Cancion_ID { get; set; }

        [Required(ErrorMessage = "El título de la canción es obligatorio")]
        public string Titulo { get; set; }

        public string? Fecha_publicacion { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        public int Duracion { get; set; }

        [Required(ErrorMessage = "La URL de la canción es obligatoria")]
        public string URL_cancion { get; set; }

        [Required(ErrorMessage = "Debe asignar un álbum")]
        public int Album_ID { get; set; }

        // Solo lectura para mostrar en la vista
        public string AlbumNombre { get; set; }
    }

}
