using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.RelacionListaCancion;
using Proyecto_Musica_GHDAL.Repositorios.RelacionListaCancion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.RelacionListaCancion
{
    public class RelacionListaCancionServicio : IRelacionListaCancionServicio
    {
        private readonly IRelacionListaCancionRepositorio _repositorio;
        private readonly IMapper _mapper;

        public RelacionListaCancionServicio(IRelacionListaCancionRepositorio repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<RelacionListaCancionDto>>> ObtenerCancionesPorPlaylistAsync(int playlistId)
        {
            var response = new CustomResponse<List<RelacionListaCancionDto>>();
            var relaciones = _repositorio.ObtenerCancionesPorPlaylist(playlistId);

            response.Data = relaciones.Select(r => new RelacionListaCancionDto
            {
                LC_REL_ID = r.LC_REL_ID,
                Playlist_ID = r.Playlist_ID,
                Cancion_ID = r.Cancion_ID,
                CancionTitulo = r.Cancion?.Titulo
            }).ToList();

            return response;
        }

        public async Task<CustomResponse<RelacionListaCancionDto>> AgregarCancionAPlaylistAsync(RelacionListaCancionDto dto)
        {
            var response = new CustomResponse<RelacionListaCancionDto>();

            if (dto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "La relación no puede ser nula.";
                response.codigoStatus = 400;
                return response;
            }

            var entidad = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.RelacionListaCancion>(dto);

            if (!_repositorio.AgregarCancionAPlaylist(entidad))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al guardar la relación.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<RelacionListaCancionDto>> EliminarCancionDePlaylistAsync(int lc_rel_id)
        {
            var response = new CustomResponse<RelacionListaCancionDto>();

            if (lc_rel_id == 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id de la relación no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            if (!_repositorio.EliminarCancionDePlaylist(lc_rel_id))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al eliminar la relación.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }
    }
}