using Proyecto_Musica_GHDAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Musica_GHDAL.Repositorios.Album
{
    public class AlbumRepositorio : IAlbumRepositorio
    {
        private readonly Proyecto_Musica_GHDbContext _context;

        public AlbumRepositorio(Proyecto_Musica_GHDbContext context)
        {
            _context = context;
        }

        // Agregar un nuevo álbum
        public bool AgregarAlbum(Entidades.Album album)
        {
            _context.Albums.Add(album);
            return _context.SaveChanges() > 0;
        }

        // Actualizar un álbum existente
        public bool ActualizarAlbum(Entidades.Album album)
        {
            var existing = _context.Albums.Find(album.Album_ID);
            if (existing == null) return false;

            existing.Titulo = album.Titulo;
            existing.Fecha_publicacion = album.Fecha_publicacion;
            existing.Artista_ID = album.Artista_ID;

            _context.Albums.Update(existing);
            return _context.SaveChanges() > 0;
        }

        // Eliminar un álbum
        public bool EliminarAlbum(int id)
        {
            var existing = _context.Albums.Find(id);
            if (existing == null) return false;

            _context.Albums.Remove(existing);
            return _context.SaveChanges() > 0;
        }

        // Obtener un álbum por Id
        public Entidades.Album ObtenerAlbumPorId(int id)
        {
            return _context.Albums
                .Include(a => a.Artista)
                .Include(a => a.Canciones) // incluye las canciones del álbum
                .AsNoTracking()
                .FirstOrDefault(a => a.Album_ID == id);
        }

        // Obtener todos los álbumes
        public List<Entidades.Album> ObtenerAlbums()
        {
            return _context.Albums
                .Include(a => a.Artista)
                .Include(a => a.Canciones)
                .AsNoTracking()
                .ToList();
        }
    }

}
