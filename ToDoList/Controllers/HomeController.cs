using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Handlers;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        HandlerObtenerDatos handlerObtenerDatos = new HandlerObtenerDatos();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
         
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userToken = HttpContext.Session.GetString("_UserToken");
            if (userToken == null)
            { // Si no hay un token de usuario
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}