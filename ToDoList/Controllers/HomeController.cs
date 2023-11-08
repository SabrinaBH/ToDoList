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
        HandlerObtenerDatos handlerObtenerDatos = new();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            //string id = handlerObtenerDatos.ObtenerIDUsuarioAdmin();
            //handlerObtenerDatos.ObtenerCategoriasUsuario(id);
            //handlerObtenerDatos.ObtenerEstadosUsuario(id);
            //string resultado = handlerObtenerDatos.ObtenerIDUsuario("sabry.brenes@outlook.es");
            //handlerObtenerDatos.ObtenerTareasUsuario(resultado);
            //handlerObtenerDatos.ObtenerEstadosUsuario(resultado);

            //Usuario usuario = new Usuario();
            //usuario.Id = " ";
            //usuario.Nombre = "Kendall";
            //usuario.PrimerApellido = "Chacon";
            //usuario.SegundoApellido = "Alfaro";
            //usuario.Email = "ken@gamil.com";
            //usuario.EsUsuarioDeJuego = false;

            //Categoria categoria = new Categoria();
            //categoria.Id = -1;
            //categoria.Nombre = "Juegos";
            //categoria.UsuarioCreador = "54B7179A-C237-491C-842E-AA3ED2A70AE9";

            //Estado estado = new Estado();
            //estado.Id = -1;
            //estado.Nombre = "Casi completadas";
            //estado.UsuarioCreador = "54B7179A-C237-491C-842E-AA3ED2A70AE9";

            //Tarea tarea = new Tarea();
            //tarea.Id = " ";
            //tarea.Titulo = "Proyecto de software";
            //tarea.Descripcion = "hacer metodos de base de datos";
            //tarea.FechaInicial = Convert.ToDateTime("2023-11-01");
            //tarea.FechaFinal = Convert.ToDateTime("2023-11-01");
            //tarea.Dificultad = 5;
            //tarea.Prioridad = 3;
            //tarea.UsuarioCreador = "54B7179A-C237-491C-842E-AA3ED2A70AE9";
            //tarea.Categoria = 3;
            //tarea.Estado = 2;

            //bool completado = handlerObtenerDatos.InsertarNuevoUsuario(usuario);
            //completado = handlerObtenerDatos.InsertarNuevaCategoria(categoria);
            //completado = handlerObtenerDatos.InsertarNuevoEstado(estado);   
            //completado = handlerObtenerDatos.InsertarNuevaTarea(tarea);


            //System.Diagnostics.Debug.WriteLine(handlerObtenerDatos.ObtenerIDUsuarioAdmin());
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
            var userToken = HttpContext.Session.GetString("_UserToken");
            if (userToken == null)
            { // Si no hay un token de usuario
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewData["token"] = userToken;
                return View("~/Shared/TableroBase.cshtml");
            }
        }

        public IActionResult Home() {
      return View();
    }

        public IActionResult Privacy()
        {
            ViewData["token"] = HttpContext.Session.GetString("_UserToken");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}