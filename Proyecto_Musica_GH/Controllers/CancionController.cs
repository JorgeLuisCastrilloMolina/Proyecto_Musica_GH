using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GHBLL.Dtos.Cancion;
using Proyecto_Musica_GHBLL.Servicios.Cancion;
using System.Threading.Tasks;

namespace Proyecto_Musica_GH.Controllers
{
    public class CancionController : Controller
    {
        private readonly ICancionServicio _cancionServicio;

        public CancionController(ICancionServicio cancionServicio)
        {
            _cancionServicio = cancionServicio;
        }

        // Página principal
        public IActionResult Index()
        {
            return View();
        }

        // Obtener todas las canciones (con paginación)
        public async Task<IActionResult> ObtenerCanciones(int pagina = 1, int tamañoPagina = 10)
        {
            var response = await _cancionServicio.ObtenerCancionesAsync(pagina, tamañoPagina);
            return Json(response);
        }

        // Buscar canciones por título
        public async Task<IActionResult> BuscarCanciones(string titulo)
        {
            var response = await _cancionServicio.BuscarCancionesPorTituloAsync(titulo);
            return Json(response);
        }

        // Obtener canción por Id
        public async Task<IActionResult> ObtenerCancionPorId(int id)
        {
            var response = await _cancionServicio.ObtenerCancionPorIdAsync(id);
            return Json(response);
        }

        // Crear canción
        [HttpPost]
        public async Task<IActionResult> AgregarCancion(CancionDto dto)
        {
            var response = await _cancionServicio.AgregarCancionAsync(dto);
            return Json(response);
        }

        // Editar canción
        [HttpPost]
        public async Task<IActionResult> EditarCancion(CancionDto dto)
        {
            var response = await _cancionServicio.EditarCancionAsync(dto);
            return Json(response);
        }

        // Eliminar canción
        [HttpPost]
        public async Task<IActionResult> EliminarCancion(int id)
        {
            var response = await _cancionServicio.EliminarCancionAsync(id);
            return Json(response);
        }
    }
}