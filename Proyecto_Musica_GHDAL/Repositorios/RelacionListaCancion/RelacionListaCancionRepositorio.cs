using Proyecto_Musica_GHDAL.Data;
using Proyecto_Musica_GHDAL.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Musica_GHDAL.Repositorios.RelacionListaCancion
{
    public class RelacionListaCancionRepositorio : IRelacionListaCancionRepositorio
    {
        private readonly Proyecto_Musica_GHDbContext _context;

        public RelacionListaCancionRepositorio(Proyecto_Musica_GHDbContext context)
        {
            _context = context;
        }

        public bool ExisteRelacion(int playlistId, int cancionId)
        {
            return _context.RelacionesListaCancion.Any(r =>
                r.Playlist_ID == playlistId && r.Cancion_ID == cancionId);
        }

        public bool ExistePlaylist(int playlistId)
        {
            return _context.Playlists.Any(p => p.Playlist_ID == playlistId);
        }

        public bool ExisteCancion(int cancionId)
        {
            return _context.Canciones.Any(c => c.Cancion_ID == cancionId);
        }

        // Agregar canción a una playlist
        public bool AgregarCancionAPlaylist(Entidades.RelacionListaCancion relacion)
        {
            try
            {
                _context.RelacionesListaCancion.Add(relacion);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        // Eliminar canción de una playlist
        public bool EliminarCancionDePlaylist(int lc_rel_id)
        {
            var existing = _context.RelacionesListaCancion.Find(lc_rel_id);
            if (existing == null) return false;

            _context.RelacionesListaCancion.Remove(existing);
            return _context.SaveChanges() > 0;
        }

        // Obtener todas las canciones de una playlist
        public List<Entidades.RelacionListaCancion> ObtenerCancionesPorPlaylist(int playlistId)
        {
            return _context.RelacionesListaCancion
                .Include(r => r.Cancion)
                .Include(r => r.Playlist)
                .Where(r => r.Playlist_ID == playlistId)
                .OrderBy(r => r.Orden ?? int.MaxValue)
                .ThenBy(r => r.LC_REL_ID)
                .AsNoTracking()
                .ToList();
        }
    }
}
