using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GHBLL.Servicios.Reproductor;

namespace Proyecto_Musica_GH.Controllers
{
    public class ReproductorController : Controller
    {
        private readonly IReproductorServicio _reproductorServicio;

        public ReproductorController(IReproductorServicio reproductorServicio)
        {
            _reproductorServicio = reproductorServicio;
        }

        private int ObtenerIndice()
        {
            var valor = HttpContext.Session.GetString("indice");

            if (string.IsNullOrEmpty(valor))
                return 0;

            return int.Parse(valor);
        }

        private void GuardarIndice(int index)
        {
            HttpContext.Session.SetString("indice", index.ToString());
        }

        [HttpPost]
        public IActionResult SeleccionarCancion(int id)
        {
            var lista = _reproductorServicio.ObtenerListaOrdenada();

            var index = lista.FindIndex(c => c.Cancion_ID == id);

            if (index == -1)
                return Json(new { esCorrecto = false, mensaje = "No encontrada" });

            GuardarIndice(index);

            return Json(new
            {
                esCorrecto = true,
                data = _reproductorServicio.Mapear(lista[index])
            });
        }

        [HttpPost]
        public IActionResult Siguiente()
        {
            var lista = _reproductorServicio.ObtenerListaOrdenada();

            int indice = ObtenerIndice();

            indice = (indice + 1) % lista.Count;

            GuardarIndice(indice);

            return Json(new
            {
                esCorrecto = true,
                data = _reproductorServicio.Mapear(lista[indice])
            });
        }

        [HttpPost]
        public IActionResult Previa()
        {
            var lista = _reproductorServicio.ObtenerListaOrdenada();

            int indice = ObtenerIndice();

            indice = (indice - 1 + lista.Count) % lista.Count;

            GuardarIndice(indice);

            return Json(new
            {
                esCorrecto = true,
                data = _reproductorServicio.Mapear(lista[indice])
            });
        }
    }
}