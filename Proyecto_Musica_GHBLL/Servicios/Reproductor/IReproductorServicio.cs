using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHBLL.Servicios.Reproductor
{
    public interface IReproductorServicio
    {
        Task<CustomResponse<CancionDto>> CargarPlaylistAsync(int playlistId);
        Task<CustomResponse<CancionDto>> ObtenerCancionActualAsync();
        Task<CustomResponse<CancionDto>> CancionSiguienteAsync();
        Task<CustomResponse<CancionDto>> CancionPreviaAsync();
        Task<CustomResponse<string>> PlayAsync();
        Task<CustomResponse<string>> DetenerAsync();
    }

}
