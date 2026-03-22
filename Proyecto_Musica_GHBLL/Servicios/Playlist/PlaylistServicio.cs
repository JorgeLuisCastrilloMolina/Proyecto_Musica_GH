using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Playlist;
using Proyecto_Musica_GHBLL.Servicios.Playlist;
using Proyecto_Musica_GHDAL.Repositorios.Playlist;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Musica_GHBLL.Servicios.Playlist
{
    public class PlaylistServicio : IPlaylistServicio
    {
        private readonly IPlaylistRepositorio _playlistRepositorio;
        private readonly IMapper _mapper;

        public PlaylistServicio(IPlaylistRepositorio playlistRepositorio, IMapper mapper)
        {
            _playlistRepositorio = playlistRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<PlaylistDto>> CrearPlaylistAsync(PlaylistDto dto)
        {
            var response = new CustomResponse<PlaylistDto>();

            if (dto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "La playlist no puede ser nula.";
                response.codigoStatus = 400;
                return response;
            }

            dto.Nombre = dto.Nombre?.Trim();
            dto.Fecha_creacion = string.IsNullOrWhiteSpace(dto.Fecha_creacion)
                ? DateTime.UtcNow.ToString("yyyy-MM-dd")
                : dto.Fecha_creacion;
            dto.Usuario_ID = dto.Usuario_ID <= 0 ? 1 : dto.Usuario_ID;

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                response.esCorrecto = false;
                response.mensaje = "El nombre de la playlist es obligatorio.";
                response.codigoStatus = 400;
                return response;
            }

            var playlistGuardar = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Playlist>(dto);

            if (!_playlistRepositorio.AgregarPlaylist(playlistGuardar))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al guardar la playlist.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<PlaylistDto>> EditarPlaylistAsync(PlaylistDto dto)
        {
            var response = new CustomResponse<PlaylistDto>();

            if (dto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "La playlist no puede ser nula.";
                response.codigoStatus = 400;
                return response;
            }

            dto.Nombre = dto.Nombre?.Trim();
            dto.Fecha_creacion = string.IsNullOrWhiteSpace(dto.Fecha_creacion)
                ? DateTime.UtcNow.ToString("yyyy-MM-dd")
                : dto.Fecha_creacion;
            dto.Usuario_ID = dto.Usuario_ID <= 0 ? 1 : dto.Usuario_ID;

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                response.esCorrecto = false;
                response.mensaje = "El nombre de la playlist es obligatorio.";
                response.codigoStatus = 400;
                return response;
            }

            if (!_playlistRepositorio.ExistePlaylist(dto.Playlist_ID))
            {
                response.esCorrecto = false;
                response.mensaje = "La playlist no existe.";
                response.codigoStatus = 404;
                return response;
            }

            var playlistEditar = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Playlist>(dto);

            if (!_playlistRepositorio.EditarPlaylist(playlistEditar))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al actualizar la playlist.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<PlaylistDto>> EliminarPlaylistAsync(int id)
        {
            var response = new CustomResponse<PlaylistDto>();

            if (id == 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id de la playlist no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            if (!_playlistRepositorio.ExistePlaylist(id))
            {
                response.esCorrecto = false;
                response.mensaje = "La playlist no existe.";
                response.codigoStatus = 404;
                return response;
            }

            if (_playlistRepositorio.TieneCancionesAsociadas(id))
            {
                response.esCorrecto = false;
                response.mensaje = "Primero debes quitar las canciones asociadas a esta playlist.";
                response.codigoStatus = 409;
                return response;
            }

            if (!_playlistRepositorio.EliminarPlaylist(id))
            {
                response.esCorrecto = false;
                response.mensaje = "No se pudo eliminar la playlist.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<PlaylistDto>> ObtenerPlaylistPorIdAsync(int id)
        {
            var response = new CustomResponse<PlaylistDto>();
            var playlist = _playlistRepositorio.ObtenerPlaylistPorId(id);

            if (playlist is null)
            {
                response.esCorrecto = false;
                response.mensaje = "La playlist no existe.";
                response.codigoStatus = 404;
                return response;
            }

            response.Data = _mapper.Map<PlaylistDto>(playlist);
            return response;
        }

        public async Task<CustomResponse<List<PlaylistDto>>> ObtenerTodasPlaylistsAsync()
        {
            var response = new CustomResponse<List<PlaylistDto>>();
            var playlists = _playlistRepositorio.ObtenerPlaylists();

            response.Data = playlists.Select(p => new PlaylistDto
            {
                Playlist_ID = p.Playlist_ID,
                Nombre = p.Nombre,
                Fecha_creacion = p.Fecha_creacion,
                Usuario_ID = p.Usuario_ID,
                UsuarioNombre = p.Usuario?.Nombre,
                CancionesCount = p.Canciones?.Count ?? 0
            }).ToList();

            return response;
        }
    }
}
