/*using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GH.Models;
using Proyecto_Musica_GH.Services;
using Proyecto_Musica_GHDAL.Entidades;

namespace Proyecto_Musica_GH.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginApiClient _apiClient;

        public LoginController(LoginApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Proyecto_Musica_GH.Models.LoginRequest request)

        {
            var result = await _apiClient.LoginAsync(request);

            if (result == null)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View();
            }

            ViewBag.Message = result.Message;
            ViewBag.Usuario = result.Usuario;
            return View("Success");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Proyecto_Musica_GH.Models.Usuario usuario)
        {

            {
                var result = await _apiClient.RegisterAsync(usuario);

                if (result == null)
                {
                    ViewBag.Error = "Error al registrar usuario";
                    return View();
                }

                ViewBag.Message = result.Message;
                ViewBag.Usuario = result.Usuario;
                return View("Success");
            }
        }
    }
}*/


using Microsoft.AspNetCore.Mvc;
using Proyecto_Musica_GH.Models;
using Proyecto_Musica_GH.Services;
using Proyecto_Musica_GHDAL.Entidades;

namespace Proyecto_Musica_GH.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginApiClient _apiClient;

        public LoginController(LoginApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Proyecto_Musica_GH.Models.LoginRequest request)
        {
            var result = await _apiClient.LoginAsync(request);

            if (result == null)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View();
            }

            
            HttpContext.Session.SetInt32("UsuarioId", result.Usuario_ID);
            HttpContext.Session.SetString("UsuarioNombre", result.Usuario);
            Console.WriteLine($"Sesion guardada: UsuarioId={result.Usuario_ID}, UsuarioNombre={result.Usuario}");


            
            return RedirectToAction("Index", "Cancion");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Proyecto_Musica_GH.Models.Usuario usuario)
        {
            var result = await _apiClient.RegisterAsync(usuario);

            if (result == null)
            {
                ViewBag.Error = "Error al registrar usuario";
                return View();
            }

            HttpContext.Session.SetInt32("UsuarioId", result.Usuario_ID);
            HttpContext.Session.SetString("UsuarioNombre", result.Usuario);

            return RedirectToAction("Index", "Cancion");
        }
    }
}