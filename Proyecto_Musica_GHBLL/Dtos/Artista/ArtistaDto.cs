using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Musica_GHBLL.Dtos.Artista
{
    public class ArtistaDto
    {
        public int Artista_ID { get; set; }

        [Required(ErrorMessage = "El nombre del artista es obligatorio")]
        public string Nombre { get; set; }

        public string? Biografia { get; set; }
        public List<string>? Albumes { get; set; }
    }
}
