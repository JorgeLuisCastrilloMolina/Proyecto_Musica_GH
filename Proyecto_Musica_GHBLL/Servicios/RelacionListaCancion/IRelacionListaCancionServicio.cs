using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.RelacionListaCancion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHBLL.Servicios.RelacionListaCancion
{
    public interface IRelacionListaCancionServicio
    {
        Task<CustomResponse<List<RelacionListaCancionDto>>> ObtenerCancionesPorPlaylistAsync(int playlistId);
        Task<CustomResponse<RelacionListaCancionDto>> AgregarCancionAPlaylistAsync(RelacionListaCancionDto dto);
        Task<CustomResponse<RelacionListaCancionDto>> EliminarCancionDePlaylistAsync(int lc_rel_id);


    }
}

