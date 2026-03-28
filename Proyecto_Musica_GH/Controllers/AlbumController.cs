using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GHBLL.Dtos.Album;
using Proyecto_Musica_GHBLL.Servicios.Album;
using System.Threading.Tasks;

namespace Proyecto_Musica_GH.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumServicio _albumServicio;

        public AlbumController(IAlbumServicio albumServicio)
        {
            _albumServicio = albumServicio;
        }

        // Página principal
        public IActionResult Index()
        {
            return View();
        }

        // Obtener todos los álbumes
        public async Task<IActionResult> ObtenerAlbums()
        {
            var response = await _albumServicio.ObtenerAlbumsAsync();
            return Json(response);
        }

        // Obtener álbum por Id
        public async Task<IActionResult> ObtenerAlbumPorId(int id)
        {
            var response = await _albumServicio.ObtenerAlbumPorIdAsync(id);
            return Json(response);
        }

        // Crear álbum
        [HttpPost]
        public async Task<IActionResult> AgregarAlbum(AlbumDto dto)
        {
            var response = await _albumServicio.AgregarAlbumAsync(dto);
            return Json(response);
        }

        // Editar álbum
        [HttpPost]
        public async Task<IActionResult> EditarAlbum(AlbumDto dto)
        {
            var response = await _albumServicio.EditarAlbumAsync(dto);
            return Json(response);
        }

        // Eliminar álbum
        [HttpPost]
        public async Task<IActionResult> EliminarAlbum(int id)
        {
            var response = await _albumServicio.EliminarAlbumAsync(id);
            return Json(response);
        }
    }
}