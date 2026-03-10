using Proyecto_Musica_GHDAL.Data;
using Proyecto_Musica_GHDAL.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Musica_GHDAL.Repositorios.Cancion
{
    public class CancionRepositorio : ICancionRepositorio
    {
        private readonly Proyecto_Musica_GHDbContext _context;

        public CancionRepositorio(Proyecto_Musica_GHDbContext context)
        {
            _context = context;
        }

        // Agregar una nueva canción
        public bool AgregarCancion(Entidades.Cancion cancion)
        {
            _context.Canciones.Add(cancion);
            return _context.SaveChanges() > 0;
        }

        // Actualizar una canción existente
        public bool ActualizarCancion(Entidades.Cancion cancion)
        {
            var existing = _context.Canciones.Find(cancion.Cancion_ID);
            if (existing == null) return false;

            existing.Titulo = cancion.Titulo;
            existing.Fecha_publicacion = cancion.Fecha_publicacion;
            existing.Duracion = cancion.Duracion;
            existing.URL_cancion = cancion.URL_cancion;
            existing.Album_ID = cancion.Album_ID;

            _context.Canciones.Update(existing);
            return _context.SaveChanges() > 0;
        }

        // Eliminar una canción
        public bool EliminarCancion(int id)
        {
            var existing = _context.Canciones.Find(id);
            if (existing == null) return false;

            _context.Canciones.Remove(existing);
            return _context.SaveChanges() > 0;
        }

        // Obtener una canción por Id
        public Entidades.Cancion ObtenerCancionPorId(int id)
        {
            return _context.Canciones
                .Include(c => c.Album) // incluye info del álbum
                .AsNoTracking()
                .FirstOrDefault(c => c.Cancion_ID == id);
        }

        // Obtener todas las canciones
        public List<Entidades.Cancion> ObtenerCanciones()
        {
            return _context.Canciones
                .Include(c => c.Album)
                .AsNoTracking()
                .ToList();
        }
    }
}