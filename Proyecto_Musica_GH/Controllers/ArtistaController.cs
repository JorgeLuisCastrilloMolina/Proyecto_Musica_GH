using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GHBLL.Dtos.Artista;
using Proyecto_Musica_GHBLL.Servicios.Artista;
using System.Threading.Tasks;

namespace Proyecto_Musica_GH.Controllers
{
    public class ArtistaController : Controller
    {
        private readonly IArtistaServicio _artistaServicio;

        public ArtistaController(IArtistaServicio artistaServicio)
        {
            _artistaServicio = artistaServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerArtistas()
        {
            var response = await _artistaServicio.ObtenerArtistasAsync();
            return Json(response);
        }

        public async Task<IActionResult> ObtenerArtistaPorId(int id)
        {
            var response = await _artistaServicio.ObtenerArtistaPorIdAsync(id);
            return Json(response);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var response = await _artistaServicio.ObtenerArtistaPorIdAsync(id);
            if (!response.esCorrecto || response.Data is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarArtista(ArtistaDto dto)
        {
            var response = await _artistaServicio.AgregarArtistaAsync(dto);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> EditarArtista(ArtistaDto dto)
        {
            var response = await _artistaServicio.EditarArtistaAsync(dto);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarArtista(int id)
        {
            var response = await _artistaServicio.EliminarArtistaAsync(id);
            return Json(response);
        }
    }
}
