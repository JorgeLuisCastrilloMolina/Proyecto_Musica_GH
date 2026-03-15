using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHBLL.Servicios.Cancion
{
    public interface ICancionServicio
    {
        Task<CustomResponse<List<CancionDto>>> ObtenerCancionesAsync(int pagina, int tamañoPagina);
        Task<CustomResponse<List<CancionDto>>> ObtenerTodasCancionesAsync();
        Task<CustomResponse<List<CancionDto>>> BuscarCancionesPorTituloAsync(string titulo);
        Task<CustomResponse<CancionDto>> ObtenerCancionPorIdAsync(int id);
        Task<CustomResponse<CancionDto>> AgregarCancionAsync(CancionDto dto);
        Task<CustomResponse<CancionDto>> EditarCancionAsync(CancionDto dto);
        Task<CustomResponse<CancionDto>> EliminarCancionAsync(int id);

    }

}
