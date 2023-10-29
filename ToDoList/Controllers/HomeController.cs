using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Handlers;
using System.Data;
using System.Threading;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        HandlerObtenerDatos handlerObtenerDatos = new HandlerObtenerDatos();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            string id = handlerObtenerDatos.ObtenerIDUsuarioAdmin();
            handlerObtenerDatos.ObtenerCategoriasUsuario(id);
            handlerObtenerDatos.ObtenerEstadosUsuario(id);
            string resultado = handlerObtenerDatos.ObtenerIDUsuario("sabry.brenes@outlook.es");
            handlerObtenerDatos.ObtenerTareasUsuario(resultado);
            handlerObtenerDatos.ObtenerEstadosUsuario(resultado);
            System.Diagnostics.Debug.WriteLine(handlerObtenerDatos.ObtenerIDUsuarioAdmin());
            //List<Tarea> tareas;
            //tareas = handlerObtenerDatos.ObtenerTareasUsuario(2);

            //foreach (Tarea tarea in tareas)
            //{
            //    System.Diagnostics.Debug.WriteLine("ID " + tarea.Id);
            //    System.Diagnostics.Debug.WriteLine("Titulo " +tarea.Titulo);
            //    System.Diagnostics.Debug.WriteLine("Descripcion "+tarea.Descripcion);
            //    System.Diagnostics.Debug.WriteLine("FechaFinal " +tarea.FechaFinal);
            //    System.Diagnostics.Debug.WriteLine("FechaInicial " +tarea.FechaInicial);
            //    System.Diagnostics.Debug.WriteLine("Dificultad: " +tarea.Dificultad);
            //    System.Diagnostics.Debug.WriteLine("Prioridad: " +tarea.Prioridad);
            //    System.Diagnostics.Debug.WriteLine("Usuario Creador: " + tarea.UsuarioCreador);
            //    System.Diagnostics.Debug.WriteLine("Categoria " + tarea.Categoria);
            //    System.Diagnostics.Debug.WriteLine("Estado " +tarea.Estado);
            //}

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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