using System.Collections.Generic;

namespace Proyecto_Musica_GHDAL.Entidades
{
    public partial class Artista
    {
        public int Artista_ID { get; set; }
        public string? Nombre { get; set; }
        public string? Biografia { get; set; }

        public List<Album>? Albums { get; set; }
    }
}
