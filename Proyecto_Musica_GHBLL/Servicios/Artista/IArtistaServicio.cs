using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Artista;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.Artista
{
    public interface IArtistaServicio
    {
        Task<CustomResponse<List<ArtistaDto>>> ObtenerArtistasAsync();
        Task<CustomResponse<ArtistaDto>> ObtenerArtistaPorIdAsync(int id);
        Task<CustomResponse<ArtistaDto>> AgregarArtistaAsync(ArtistaDto dto);
        Task<CustomResponse<ArtistaDto>> EditarArtistaAsync(ArtistaDto dto);
        Task<CustomResponse<ArtistaDto>> EliminarArtistaAsync(int id);
    }
}
