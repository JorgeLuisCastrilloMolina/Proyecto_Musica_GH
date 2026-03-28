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

            var entidad = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Album>(dto);

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

            var entidad = _mapper.Map<Proyecto_Musica_GHDAL.Entidades.Album>(dto);

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
