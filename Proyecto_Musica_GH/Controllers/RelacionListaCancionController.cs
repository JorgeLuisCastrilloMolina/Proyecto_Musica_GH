using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GHBLL.Dtos.RelacionListaCancion;
using Proyecto_Musica_GHBLL.Servicios.RelacionListaCancion;
using System.Threading.Tasks;

namespace Proyecto_Musica_GH.Controllers
{
    public class RelacionListaCancionController : Controller
    {
        private readonly IRelacionListaCancionServicio _relacionServicio;

        public RelacionListaCancionController(IRelacionListaCancionServicio relacionServicio)
        {
            _relacionServicio = relacionServicio;
        }

        // Página principal
        public IActionResult Index()
        {
            return View();
        }

        // Obtener canciones de una playlist
        public async Task<IActionResult> ObtenerCancionesPorPlaylist(int playlistId)
        {
            var response = await _relacionServicio.ObtenerCancionesPorPlaylistAsync(playlistId);
            return Json(response);
        }

        // Agregar canción a playlist
        [HttpPost]
        public async Task<IActionResult> AgregarCancionAPlaylist(RelacionListaCancionDto dto)
        {
            var response = await _relacionServicio.AgregarCancionAPlaylistAsync(dto);
            return Json(response);
        }

        // Eliminar canción de playlist
        [HttpPost]
        public async Task<IActionResult> EliminarCancionDePlaylist(int lc_rel_id)
        {
            var response = await _relacionServicio.EliminarCancionDePlaylistAsync(lc_rel_id);
            return Json(response);
        }
    }
}