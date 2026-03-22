using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHBLL
{
    public class MapeoClases : Profile

    {
        public MapeoClases()
        {
            // Playlist
            CreateMap<Proyecto_Musica_GHDAL.Entidades.Playlist, Proyecto_Musica_GHBLL.Dtos.Playlist.PlaylistDto>()
                .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.Nombre))
                .ReverseMap();

            // Cancion
            CreateMap<Proyecto_Musica_GHDAL.Entidades.Cancion, Proyecto_Musica_GHBLL.Dtos.Cancion.CancionDto>()
                .ForMember(dest => dest.AlbumNombre, opt => opt.MapFrom(src => src.Album.Titulo))
                .ForMember(dest => dest.URL_cancion, opt => opt.MapFrom(src => src.URL_cancion))
                .ReverseMap();

            // RelacionListaCancion
            CreateMap<Proyecto_Musica_GHDAL.Entidades.RelacionListaCancion, Proyecto_Musica_GHBLL.Dtos.RelacionListaCancion.RelacionListaCancionDto>()
                .ForMember(dest => dest.PlaylistNombre, opt => opt.MapFrom(src => src.Playlist.Nombre))
                .ForMember(dest => dest.CancionTitulo, opt => opt.MapFrom(src => src.Cancion.Titulo))
                .ReverseMap();
        }

    }
}
