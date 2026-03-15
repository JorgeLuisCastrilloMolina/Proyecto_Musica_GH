using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GH.Models;
using Proyecto_Musica_GHBLL.Dtos.Playlist;
using Proyecto_Musica_GHBLL.Servicios.Cancion;
using Proyecto_Musica_GHBLL.Servicios.Playlist;
using Proyecto_Musica_GHBLL.Servicios.RelacionListaCancion;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Musica_GH.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistServicio _playlistServicio;
        private readonly IRelacionListaCancionServicio _relacionServicio;
        private readonly ICancionServicio _cancionServicio;

        public PlaylistController(
            IPlaylistServicio playlistServicio,
            IRelacionListaCancionServicio relacionServicio,
            ICancionServicio cancionServicio)
        {
            _playlistServicio = playlistServicio;
            _relacionServicio = relacionServicio;
            _cancionServicio = cancionServicio;
        }

        // Página principal
        public IActionResult Index()
        {
            return View();
        }

        // Obtener todas las playlists
        public async Task<IActionResult> ObtenerPlaylists()
        {
            var response = await _playlistServicio.ObtenerTodasPlaylistsAsync();
            return Json(response);
        }

        // Obtener playlist por Id
        public async Task<IActionResult> ObtenerPlaylistPorId(int id)
        {
            var response = await _playlistServicio.ObtenerPlaylistPorIdAsync(id);
            return Json(response);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var playlistResponse = await _playlistServicio.ObtenerPlaylistPorIdAsync(id);
            if (!playlistResponse.esCorrecto || playlistResponse.Data is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var cancionesPlaylistResponse = await _relacionServicio.ObtenerCancionesPorPlaylistAsync(id);
            var cancionesResponse = await _cancionServicio.ObtenerTodasCancionesAsync();

            var cancionesEnPlaylist = cancionesPlaylistResponse.Data?
                .Select(c => c.Cancion_ID)
                .ToHashSet() ?? new HashSet<int>();

            var model = new PlaylistDetalleViewModel
            {
                Playlist = playlistResponse.Data,
                Canciones = cancionesPlaylistResponse.Data ?? new(),
                CancionesDisponibles = cancionesResponse.Data?
                    .Where(c => !cancionesEnPlaylist.Contains(c.Cancion_ID))
                    .OrderBy(c => c.Titulo)
                    .ToList() ?? new()
            };

            return View(model);
        }

        // Crear playlist
        [HttpPost]
        public async Task<IActionResult> CrearPlaylist(PlaylistDto dto)
        {
            var response = await _playlistServicio.CrearPlaylistAsync(dto);
            return Json(response);
        }

        // Editar playlist
        [HttpPost]
        public async Task<IActionResult> EditarPlaylist(PlaylistDto dto)
        {
            var response = await _playlistServicio.EditarPlaylistAsync(dto);
            return Json(response);
        }

        // Eliminar playlist
        [HttpPost]
        public async Task<IActionResult> EliminarPlaylist(int id)
        {
            var response = await _playlistServicio.EliminarPlaylistAsync(id);
            return Json(response);
        }
    }
}
