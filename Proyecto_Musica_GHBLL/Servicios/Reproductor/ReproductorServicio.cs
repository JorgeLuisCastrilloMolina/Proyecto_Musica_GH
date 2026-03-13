using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using Proyecto_Musica_GHDAL.Repositorios.RelacionListaCancion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.Reproductor
{
    public class ReproductorServicio : IReproductorServicio
    {
        private readonly IRelacionListaCancionRepositorio _relacionRepo;
        private readonly IMapper _mapper;

        private List<Proyecto_Musica_GHDAL.Entidades.RelacionListaCancion> _canciones;
        private int _indiceActual = 0;
        private bool _reproduciendo = false;

        public ReproductorServicio(IRelacionListaCancionRepositorio relacionRepo, IMapper mapper)
        {
            _relacionRepo = relacionRepo;
            _mapper = mapper;
        }

        public async Task<CustomResponse<CancionDto>> CargarPlaylistAsync(int playlistId)
        {
            var response = new CustomResponse<CancionDto>();
            _canciones = _relacionRepo.ObtenerCancionesPorPlaylist(playlistId);

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "La playlist está vacía.";
                response.codigoStatus = 404;
                return response;
            }

            _indiceActual = 0;
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual].Cancion);
            return response;
        }

        public async Task<CustomResponse<CancionDto>> ObtenerCancionActualAsync()
        {
            var response = new CustomResponse<CancionDto>();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones cargadas.";
                response.codigoStatus = 400;
                return response;
            }

            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual].Cancion);
            return response;
        }

        public async Task<CustomResponse<CancionDto>> CancionSiguienteAsync()
        {
            var response = new CustomResponse<CancionDto>();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones cargadas.";
                response.codigoStatus = 400;
                return response;
            }

            _indiceActual = (_indiceActual + 1) % _canciones.Count;
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual].Cancion);
            return response;
        }

        public async Task<CustomResponse<CancionDto>> CancionPreviaAsync()
        {
            var response = new CustomResponse<CancionDto>();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones cargadas.";
                response.codigoStatus = 400;
                return response;
            }

            _indiceActual = (_indiceActual - 1 + _canciones.Count) % _canciones.Count;
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual].Cancion);
            return response;
        }

        public async Task<CustomResponse<string>> PlayAsync()
        {
            var response = new CustomResponse<string>();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones cargadas.";
                response.codigoStatus = 400;
                return response;
            }

            _reproduciendo = true;
            response.Data = $"Reproduciendo: {_canciones[_indiceActual].Cancion?.Titulo}";
            return response;
        }

        public async Task<CustomResponse<string>> DetenerAsync()
        {
            var response = new CustomResponse<string>();

            if (!_reproduciendo)
            {
                response.esCorrecto = false;
                response.mensaje = "No hay reproducción activa.";
                response.codigoStatus = 400;
                return response;
            }

            _reproduciendo = false;
            response.Data = "Reproducción detenida.";
            return response;
        }
    }
}