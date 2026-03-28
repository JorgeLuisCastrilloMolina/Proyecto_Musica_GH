using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Album;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHBLL.Servicios.Album
{
    public interface IAlbumServicio
    {
        Task<CustomResponse<List<AlbumDto>>> ObtenerAlbumsAsync();
        Task<CustomResponse<AlbumDto>> ObtenerAlbumPorIdAsync(int id);
        Task<CustomResponse<AlbumDto>> AgregarAlbumAsync(AlbumDto dto);
        Task<CustomResponse<AlbumDto>> EditarAlbumAsync(AlbumDto dto);
        Task<CustomResponse<AlbumDto>> EliminarAlbumAsync(int id);
    }

}
