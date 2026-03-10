using Proyecto_Musica_GHDAL.Data;
using Proyecto_Musica_GHDAL.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Musica_GHDAL.Repositorios.RelacionListaCancion
{
    public class RelacionListaCancionRepositorio : IRelacionListaCancionRepositorio
    {
        private readonly ProyectoMusicaDbContext _context;

        public RelacionListaCancionRepositorio(ProyectoMusicaDbContext context)
        {
            _context = context;
        }

        // Agregar canción a una playlist
        public bool AgregarCancionAPlaylist(Entidades.RelacionListaCancion relacion)
        {
            _context.RelacionesListaCancion.Add(relacion);
            return _context.SaveChanges() > 0;
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
                .AsNoTracking()
                .ToList();
        }
    }
}