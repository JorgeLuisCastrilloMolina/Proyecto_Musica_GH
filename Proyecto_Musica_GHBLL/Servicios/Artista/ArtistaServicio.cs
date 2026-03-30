using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Artista;
using Proyecto_Musica_GHDAL.Repositorios.Artista;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.Artista
{
    public class ArtistaServicio : IArtistaServicio
    {
        private readonly IArtistaRepositorio _artistaRepositorio;
        private readonly IMapper _mapper;

        public ArtistaServicio(IArtistaRepositorio artistaRepositorio, IMapper mapper)
        {
            _artistaRepositorio = artistaRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<ArtistaDto>>> ObtenerArtistasAsync()
        {
            var response = new CustomResponse<List<ArtistaDto>>();
            var artistas = _artistaRepositorio.ObtenerArtistas();
            response.Data = _mapper.Map<List<ArtistaDto>>(artistas);
            return response;
        }

        public async Task<CustomResponse<ArtistaDto>> ObtenerArtistaPorIdAsync(int id)
        {
            var response = new CustomResponse<ArtistaDto>();
            var artista = _artistaRepositorio.ObtenerArtistaPorId(id);

            if (artista is null)
            {
                response.esCorrecto = false;
                response.mensaje = "El artista no existe.";
                response.codigoStatus = 404;
                return response;
            }

            response.Data = _mapper.Map<ArtistaDto>(artista);
            return response;
        }

        public async Task<CustomResponse<ArtistaDto>> AgregarArtistaAsync(ArtistaDto dto)
        {
            var response = new CustomResponse<ArtistaDto>();

            if (dto is null || string.IsNullOrWhiteSpace(dto.Nombre))
            {
                response.esCorrecto = false;
                response.mensaje = "El artista es inválido.";
                response.codigoStatus = 400;
                return response;
            }

            var entidad = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Artista>(dto);

            if (!_artistaRepositorio.AgregarArtista(entidad))
            {
                response.esCorrecto = false;
                response.mensaje = "No se pudo guardar el artista.";
                response.codigoStatus = 500;
            }

            return response;
        }

        public async Task<CustomResponse<ArtistaDto>> EditarArtistaAsync(ArtistaDto dto)
        {
            var response = new CustomResponse<ArtistaDto>();

            if (dto is null || string.IsNullOrWhiteSpace(dto.Nombre))
            {
                response.esCorrecto = false;
                response.mensaje = "El artista es inválido.";
                response.codigoStatus = 400;
                return response;
            }

            var entidad = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Artista>(dto);

            if (!_artistaRepositorio.ActualizarArtista(entidad))
            {
                response.esCorrecto = false;
                response.mensaje = "No se pudo actualizar el artista.";
                response.codigoStatus = 500;
            }

            return response;
        }

        public async Task<CustomResponse<ArtistaDto>> EliminarArtistaAsync(int id)
        {
            var response = new CustomResponse<ArtistaDto>();

            if (id <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id del artista no es válido.";
                response.codigoStatus = 400;
                return response;
            }

            if (!_artistaRepositorio.EliminarArtista(id))
            {
                response.esCorrecto = false;
                response.mensaje = "No se pudo eliminar el artista. Verifica que no tenga álbumes asociados.";
                response.codigoStatus = 409;
            }

            return response;
        }
    }
}
