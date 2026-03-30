using AutoMapper;
using Proyecto_Musica_GHBLL.Dtos;
using Proyecto_Musica_GHBLL.Dtos.Album;
using Proyecto_Musica_GHDAL.Repositorios.Album;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHBLL.Servicios.Album
{
    public class AlbumServicio : IAlbumServicio
    {
        private readonly IAlbumRepositorio _albumRepositorio;
        private readonly IMapper _mapper;

        public AlbumServicio(IAlbumRepositorio albumRepositorio, IMapper mapper)
        {
            _albumRepositorio = albumRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<AlbumDto>>> ObtenerAlbumsAsync()
        {
            var response = new CustomResponse<List<AlbumDto>>();
            var albums = _albumRepositorio.ObtenerAlbums();
            response.Data = _mapper.Map<List<AlbumDto>>(albums);
            return response;
        }

        public async Task<CustomResponse<AlbumDto>> ObtenerAlbumPorIdAsync(int id)
        {
            var response = new CustomResponse<AlbumDto>();
            var album = _albumRepositorio.ObtenerAlbumPorId(id);

            if (album is null)
            {
                response.esCorrecto = false;
                response.mensaje = "El álbum no existe.";
                response.codigoStatus = 404;
                return response;
            }

            response.Data = _mapper.Map<AlbumDto>(album);
            return response;
        }

        public async Task<CustomResponse<AlbumDto>> AgregarAlbumAsync(AlbumDto dto)
        {
            var response = new CustomResponse<AlbumDto>();

            if (dto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "El álbum no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            if (dto.Artista_ID <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "Debe seleccionar un artista válido.";
                response.codigoStatus = 400;
                return response;
            }

            dto.Titulo = dto.Titulo?.Trim();
            dto.Fecha_publicacion = string.IsNullOrWhiteSpace(dto.Fecha_publicacion)
                ? DateTime.UtcNow.ToString("yyyy-MM-dd")
                : dto.Fecha_publicacion;

            var entidad = new Proyecto_Musica_GHDAL.Entidades.Album
            {
                Titulo = dto.Titulo,
                Fecha_publicacion = dto.Fecha_publicacion,
                Artista_ID = dto.Artista_ID
            };

            if (!_albumRepositorio.AgregarAlbum(entidad))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al guardar el álbum.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<AlbumDto>> EditarAlbumAsync(AlbumDto dto)
        {
            var response = new CustomResponse<AlbumDto>();

            if (dto is null)
            {
                response.esCorrecto = false;
                response.mensaje = "El álbum no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            if (dto.Album_ID <= 0 || dto.Artista_ID <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El álbum o el artista seleccionado no es válido.";
                response.codigoStatus = 400;
                return response;
            }

            dto.Titulo = dto.Titulo?.Trim();
            dto.Fecha_publicacion = string.IsNullOrWhiteSpace(dto.Fecha_publicacion)
                ? DateTime.UtcNow.ToString("yyyy-MM-dd")
                : dto.Fecha_publicacion;

            var entidad = new Proyecto_Musica_GHDAL.Entidades.Album
            {
                Album_ID = dto.Album_ID,
                Titulo = dto.Titulo,
                Fecha_publicacion = dto.Fecha_publicacion,
                Artista_ID = dto.Artista_ID
            };

            if (!_albumRepositorio.ActualizarAlbum(entidad))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al actualizar el álbum.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }

        public async Task<CustomResponse<AlbumDto>> EliminarAlbumAsync(int id)
        {
            var response = new CustomResponse<AlbumDto>();

            if (id == 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id del álbum no puede ser nulo.";
                response.codigoStatus = 400;
                return response;
            }

            if (!_albumRepositorio.EliminarAlbum(id))
            {
                response.esCorrecto = false;
                response.mensaje = "Error al eliminar el álbum.";
                response.codigoStatus = 500;
                return response;
            }

            return response;
        }
    }

}
