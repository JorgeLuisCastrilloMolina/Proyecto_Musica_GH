using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Repositorios.Playlist
{
    public interface IPlaylistRepositorio
    {
        List<Entidades.Playlist> ObtenerPlaylists();
        Entidades.Playlist ObtenerPlaylistPorId(int id);
        bool ExistePlaylist(int id);
        bool TieneCancionesAsociadas(int id);
        bool AgregarPlaylist(Entidades.Playlist playlist);
        bool EditarPlaylist(Entidades.Playlist playlist);
        bool EliminarPlaylist(int id);
    }

}
