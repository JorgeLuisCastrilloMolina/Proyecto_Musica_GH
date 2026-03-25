using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GHBLL.Servicios.Reproductor;
using System.Threading.Tasks;

namespace Proyecto_Musica_GH.Controllers
{
    public class ReproductorController : Controller
    {
        private readonly IReproductorServicio _reproductorServicio;

        public ReproductorController(IReproductorServicio reproductorServicio)
        {
            _reproductorServicio = reproductorServicio;
        }

        [HttpPost]
        public async Task<IActionResult> CargarCanciones()
        {
            var response = await _reproductorServicio.CargarCancionesAsync();
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> CancionActual()
        {
            var response = await _reproductorServicio.ObtenerCancionActualAsync();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Siguiente()
        {
            var response = await _reproductorServicio.CancionSiguienteAsync();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Previa()
        {
            var response = await _reproductorServicio.CancionPreviaAsync();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Play()
        {
            var response = await _reproductorServicio.PlayAsync();
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Detener()
        {
            var response = await _reproductorServicio.DetenerAsync();
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> SeleccionarCancion(int id)
        {
            var response = await _reproductorServicio.SeleccionarCancionAsync(id);
            return Json(response);
        }
    }
}