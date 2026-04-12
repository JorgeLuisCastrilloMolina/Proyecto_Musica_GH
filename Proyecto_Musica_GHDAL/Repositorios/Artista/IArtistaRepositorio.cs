using System.Collections.Generic;

namespace Proyecto_Musica_GHDAL.Repositorios.Artista
{
    public interface IArtistaRepositorio
    {
        List<Entidades.Artista> ObtenerArtistas();
        Entidades.Artista ObtenerArtistaPorId(int id);
        bool AgregarArtista(Entidades.Artista artista);
        bool ActualizarArtista(Entidades.Artista artista);
        bool EliminarArtista(int id);
    }
}
