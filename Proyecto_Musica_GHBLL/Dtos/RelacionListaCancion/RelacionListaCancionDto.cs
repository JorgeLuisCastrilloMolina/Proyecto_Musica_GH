using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Proyecto_Musica_GHBLL.Dtos.RelacionListaCancion
{
    public class RelacionListaCancionDto
    {
        public int LC_REL_ID { get; set; }

        [Required(ErrorMessage = "Debe asignar una playlist")]
        public int Playlist_ID { get; set; }

        [Required(ErrorMessage = "Debe asignar una canción")]
        public int Cancion_ID { get; set; }

        public int? Orden { get; set; }

        // Solo lectura para mostrar nombres en la vista
        public string PlaylistNombre { get; set; }
        public string CancionTitulo { get; set; }
    }

}
