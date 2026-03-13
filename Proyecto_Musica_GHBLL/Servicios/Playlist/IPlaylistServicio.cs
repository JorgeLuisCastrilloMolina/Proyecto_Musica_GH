using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Playlist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHBLL.Servicios.Playlist
{
    public interface IPlaylistServicio
    {
        Task<CustomResponse<PlaylistDto>> CrearPlaylistAsync(PlaylistDto dto);
        Task<CustomResponse<PlaylistDto>> EditarPlaylistAsync(PlaylistDto dto);
        Task<CustomResponse<PlaylistDto>> EliminarPlaylistAsync(int id);
        Task<CustomResponse<PlaylistDto>> ObtenerPlaylistPorIdAsync(int id);
        Task<CustomResponse<List<PlaylistDto>>> ObtenerTodasPlaylistsAsync();

    }

}
