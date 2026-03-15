using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using Proyecto_Musica_GHDAL.Repositorios.Cancion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.Cancion
{
    public class CancionServicio : ICancionServicio
    {
        private readonly ICancionRepositorio _cancionRepositorio;
        private readonly IMapper _mapper;

        public CancionServicio(ICancionRepositorio cancionRepositorio, IMapper mapper)
        {
            _cancionRepositorio = cancionRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<CancionDto>>> ObtenerCancionesAsync(int pagina, int tamañoPagina)
        {
            var response = new CustomResponse<List<CancionDto>>();
            var canciones = _cancionRepositorio.ObtenerCanciones();

            var paginadas = canciones
                .Skip((pagina - 1) * tamañoPagina)
                .Take(tamañoPagina)
                .ToList();

            response.Data = _mapper.Map<List<CancionDto>>(paginadas);
            return response;
        }

        public async Task<CustomResponse<List<CancionDto>>> ObtenerTodasCancionesAsync()
        {
            var response = new CustomResponse<List<CancionDto>>();
            var canciones = _cancionRepositorio.ObtenerCanciones();

            response.Data = _mapper.Map<List<CancionDto>>(canciones);
            return response;
        }

        public async Task<CustomResponse<List<CancionDto>>> BuscarCancionesPorTituloAsync(string titulo)
        {
            var response = new CustomResponse<List<CancionDto>>();
            var canciones = _cancionRepositorio.ObtenerCanciones()
                .Where(c => c.Titulo.Contains(titulo))
                .ToList();

            response.Data = _mapper.Map<List<CancionDto>>(canciones);
            return response;
        }

        public async Task<CustomResponse<CancionDto>> ObtenerCancionPorIdAsync(int id)
        {
            var response = new CustomResponse<CancionDto>();
            var cancion = _cancionRepositorio.ObtenerCancionPorId(id);

            if (cancion is null)
            {
                response.esCorrecto = false;
                response.mensaje = "La canción no existe.";
                response.codigoStatus = 404;
                return response;
            }

            response.Data = _mapper.Map<CancionDto>(cancion);
            return response;
        }

        public async Task<CustomResponse<CancionDto>> AgregarCancionAsync(CancionDto dto)
        {
            var response = new CustomResponse<CancionDto>();

            if (dto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "La canción no puede ser nula.";
                response.codigoStatus = 400;
                return response;
            }

            var entidad = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Cancion>(dto);

            if (!_cancionRepositorio.AgregarCancion(entidad))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al guardar la canción.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<CancionDto>> EditarCancionAsync(CancionDto dto)
        {
            var response = new CustomResponse<CancionDto>();

            if (dto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "La canción no puede ser nula.";
                response.codigoStatus = 400;
                return response;
            }

            var entidad = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Cancion>(dto);

            if (!_cancionRepositorio.ActualizarCancion(entidad))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al actualizar la canción.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<CancionDto>> EliminarCancionAsync(int id)
        {
            var response = new CustomResponse<CancionDto>();

            if (id == 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id de la canción no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            if (!_cancionRepositorio.EliminarCancion(id))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al eliminar la canción.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }
    }
}
