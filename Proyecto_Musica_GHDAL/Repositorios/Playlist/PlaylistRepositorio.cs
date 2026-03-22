using Proyecto_Musica_GHDAL.Data;
using Proyecto_Musica_GHDAL.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Musica_GHDAL.Repositorios.Playlist
{
    public class PlaylistRepositorio : IPlaylistRepositorio
    {
        private readonly Proyecto_Musica_GHDbContext _context;

        public PlaylistRepositorio(Proyecto_Musica_GHDbContext context)
        {
            _context = context;
        }

        public bool AgregarPlaylist(Entidades.Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            return _context.SaveChanges() > 0;
        }

        public bool ExistePlaylist(int id)
        {
            return _context.Playlists.Any(p => p.Playlist_ID == id);
        }

        public bool TieneCancionesAsociadas(int id)
        {
            return _context.RelacionesListaCancion.Any(r => r.Playlist_ID == id);
        }

        public bool EditarPlaylist(Entidades.Playlist playlist)
        {
            var existing = _context.Playlists.Find(playlist.Playlist_ID);
            if (existing == null) return false;

            existing.Nombre = playlist.Nombre;
            existing.Fecha_creacion = playlist.Fecha_creacion;
            existing.Usuario_ID = playlist.Usuario_ID;

            _context.Playlists.Update(existing);
            return _context.SaveChanges() > 0;
        }

        public bool EliminarPlaylist(int id)
        {
            var existing = _context.Playlists.Find(id);
            if (existing == null) return false;

            _context.Playlists.Remove(existing);
            return _context.SaveChanges() > 0;
        }

        public Entidades.Playlist ObtenerPlaylistPorId(int id)
        {
            return _context.Playlists
                .Include(p => p.Usuario)
                .Include(p => p.Canciones)
                .ThenInclude(rc => rc.Cancion)
                .AsNoTracking()
                .FirstOrDefault(p => p.Playlist_ID == id);
        }

        public List<Entidades.Playlist> ObtenerPlaylists()
        {
            return _context.Playlists
                .Include(p => p.Usuario)
                .Include(p => p.Canciones)
                .ThenInclude(rc => rc.Cancion)
                .AsNoTracking()
                .ToList();
        }
    }
}
