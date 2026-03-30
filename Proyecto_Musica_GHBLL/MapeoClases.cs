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
                .ForMember(dest => dest.Artista_ID, opt => opt.MapFrom(src => src.Album.Artista_ID))
                .ForMember(dest => dest.ArtistaNombre, opt => opt.MapFrom(src => src.Album.Artista.Nombre))
                .ForMember(dest => dest.URL_cancion, opt => opt.MapFrom(src => src.URL_cancion));

            CreateMap<Proyecto_Musica_GHBLL.Dtos.Cancion.CancionDto, Proyecto_Musica_GHDAL.Entidades.Cancion>();

            // RelacionListaCancion
            CreateMap<Proyecto_Musica_GHDAL.Entidades.RelacionListaCancion, Proyecto_Musica_GHBLL.Dtos.RelacionListaCancion.RelacionListaCancionDto>()
                .ForMember(dest => dest.PlaylistNombre, opt => opt.MapFrom(src => src.Playlist.Nombre))
                .ForMember(dest => dest.CancionTitulo, opt => opt.MapFrom(src => src.Cancion.Titulo))
                .ReverseMap();

            // Album
            CreateMap<Proyecto_Musica_GHDAL.Entidades.Album, Proyecto_Musica_GHBLL.Dtos.Album.AlbumDto>()
                .ForMember(dest => dest.ArtistaNombre, opt => opt.MapFrom(src => src.Artista.Nombre))
                .ForMember(dest => dest.Canciones, opt => opt.MapFrom(src => src.Canciones.Select(c => c.Titulo)))
                .ReverseMap();

            // Artista
            CreateMap<Proyecto_Musica_GHDAL.Entidades.Artista, Proyecto_Musica_GHBLL.Dtos.Artista.ArtistaDto>()
                .ForMember(dest => dest.Albumes, opt => opt.MapFrom(src => src.Albums.Select(a => a.Titulo)))
                .ReverseMap();

        }

    }
}
