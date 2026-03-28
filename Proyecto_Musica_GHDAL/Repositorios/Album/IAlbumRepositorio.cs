using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Musica_GHDAL.Repositorios.Album
{
    public interface IAlbumRepositorio
    {
        List<Entidades.Album> ObtenerAlbums();
        Entidades.Album ObtenerAlbumPorId(int id);
        bool AgregarAlbum(Entidades.Album album);
        bool ActualizarAlbum(Entidades.Album album);
        bool EliminarAlbum(int id);
    }

}
