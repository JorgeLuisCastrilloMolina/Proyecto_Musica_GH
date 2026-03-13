using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GHBLL.Dtos.Playlist;
using Proyecto_Musica_GHBLL.Servicios.Playlist;
using System.Threading.Tasks;

namespace Proyecto_Musica_GH.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistServicio _playlistServicio;

        public PlaylistController(IPlaylistServicio playlistServicio)
        {
            _playlistServicio = playlistServicio;
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