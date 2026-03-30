using Microsoft.EntityFrameworkCore;
using Proyecto_Musica_GHDAL.Data;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Musica_GHDAL.Repositorios.Artista
{
    public class ArtistaRepositorio : IArtistaRepositorio
    {
        private readonly Proyecto_Musica_GHDbContext _context;

        public ArtistaRepositorio(Proyecto_Musica_GHDbContext context)
        {
            _context = context;
        }

        public List<Entidades.Artista> ObtenerArtistas()
        {
            return _context.Artistas
                .Include(a => a.Albums)
                .AsNoTracking()
                .OrderBy(a => a.Nombre)
                .ToList();
        }

        public Entidades.Artista ObtenerArtistaPorId(int id)
        {
            return _context.Artistas
                .Include(a => a.Albums)
                .ThenInclude(al => al.Canciones)
                .AsNoTracking()
                .FirstOrDefault(a => a.Artista_ID == id);
        }

        public bool AgregarArtista(Entidades.Artista artista)
        {
            _context.Artistas.Add(artista);
            return _context.SaveChanges() > 0;
        }

        public bool ActualizarArtista(Entidades.Artista artista)
        {
            var existing = _context.Artistas.Find(artista.Artista_ID);
            if (existing == null) return false;

            existing.Nombre = artista.Nombre;
            existing.Biografia = artista.Biografia;

            _context.Artistas.Update(existing);
            return _context.SaveChanges() > 0;
        }

        public bool EliminarArtista(int id)
        {
            var existing = _context.Artistas
                .Include(a => a.Albums)
                .FirstOrDefault(a => a.Artista_ID == id);

            if (existing == null || (existing.Albums?.Any() ?? false)) return false;

            _context.Artistas.Remove(existing);
            return _context.SaveChanges() > 0;
        }
    }
}
