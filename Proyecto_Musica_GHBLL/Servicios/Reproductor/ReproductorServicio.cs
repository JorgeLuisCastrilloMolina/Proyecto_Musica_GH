
/*
using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using Proyecto_Musica_GHDAL.Repositorios.Cancion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.Reproductor
{
    public class ReproductorServicio : IReproductorServicio
    {
        private readonly ICancionRepositorio _cancionRepo;
        private readonly IMapper _mapper;
        private List<Proyecto_Musica_GHDAL.Entidades.Cancion> _canciones;
        private int _indiceActual = 0;
        private bool _reproduciendo = false;

        public ReproductorServicio(ICancionRepositorio cancionRepo, IMapper mapper)
        {
            _cancionRepo = cancionRepo;
            _mapper = mapper;
        }

        // Cargar todas las canciones de la tabla Cancion
        public async Task<CustomResponse<CancionDto>> CargarCancionesAsync()
        {
            var response = new CustomResponse<CancionDto>();
            _canciones = _cancionRepo.ObtenerCanciones();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones disponibles.";
                response.codigoStatus = 404;
                return response;
            }

            _indiceActual = 0;
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
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

            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
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
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
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
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
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
            response.Data = $"Reproduciendo: {_canciones[_indiceActual].Titulo}";
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
        public async Task<CustomResponse<CancionDto>> SeleccionarCancionAsync(int id)
        {
            var response = new CustomResponse<CancionDto>();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones cargadas.";
                response.codigoStatus = 400;
                return response;
            }

            var index = _canciones.FindIndex(c => c.Cancion_ID == id);
            if (index == -1)
            {
                response.esCorrecto = false;
                response.mensaje = "Canción no encontrada.";
                response.codigoStatus = 404;
                return response;
            }

            _indiceActual = index;
            _reproduciendo = true;
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
            return response;
        }
    }
}

*/







using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using Proyecto_Musica_GHDAL.Repositorios.Cancion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.Reproductor
{
    public class ReproductorServicio : IReproductorServicio
    {
        private readonly ICancionRepositorio _cancionRepo;
        private readonly IMapper _mapper;
        private List<Proyecto_Musica_GHDAL.Entidades.Cancion> _canciones;
        private int _indiceActual = 0;
        private bool _reproduciendo = false;

        public ReproductorServicio(ICancionRepositorio cancionRepo, IMapper mapper)
        {
            _cancionRepo = cancionRepo;
            _mapper = mapper;
        }

        // Cargar todas las canciones de la tabla Cancion (inicializa playlist en memoria)
        public async Task<CustomResponse<CancionDto>> CargarCancionesAsync()
        {
            var response = new CustomResponse<CancionDto>();
            _canciones = _cancionRepo.ObtenerCanciones();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones disponibles.";
                response.codigoStatus = 404;
                return response;
            }

            _indiceActual = 0;
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
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

            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
            return response;
        }

        // NUEVO: devuelve lista completa de canciones (DTOs)
        public async Task<CustomResponse<List<CancionDto>>> ObtenerTodasCancionesAsync()
        {
            var response = new CustomResponse<List<CancionDto>>();
            var canciones = _cancionRepo.ObtenerCanciones();

            if (canciones == null || !canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones disponibles.";
                response.codigoStatus = 404;
                return response;
            }

            response.Data = _mapper.Map<List<CancionDto>>(canciones);
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
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
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
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
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
            response.Data = $"Reproduciendo: {_canciones[_indiceActual].Titulo}";
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
        public async Task<CustomResponse<CancionDto>> SeleccionarCancionAsync(int id)
        {
            var response = new CustomResponse<CancionDto>();

            if (_canciones == null || !_canciones.Any())
            {
                response.esCorrecto = false;
                response.mensaje = "No hay canciones cargadas.";
                response.codigoStatus = 400;
                return response;
            }

            var index = _canciones.FindIndex(c => c.Cancion_ID == id);
            if (index == -1)
            {
                response.esCorrecto = false;
                response.mensaje = "Canción no encontrada.";
                response.codigoStatus = 404;
                return response;
            }

            _indiceActual = index;
            _reproduciendo = true;
            response.Data = _mapper.Map<CancionDto>(_canciones[_indiceActual]);
            return response;
        }
    }
}