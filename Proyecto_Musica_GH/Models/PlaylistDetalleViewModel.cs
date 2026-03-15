using Proyecto_Musica_GHBLL.Dtos.Cancion;
using Proyecto_Musica_GHBLL.Dtos.Playlist;
using Proyecto_Musica_GHBLL.Dtos.RelacionListaCancion;

namespace Proyecto_Musica_GH.Models
{
    public class PlaylistDetalleViewModel
    {
        public PlaylistDto? Playlist { get; set; }
        public List<RelacionListaCancionDto> Canciones { get; set; } = new();
        public List<CancionDto> CancionesDisponibles { get; set; } = new();
    }
}
