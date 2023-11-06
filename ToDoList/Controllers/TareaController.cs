using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diseno.Controllers
{
    public class TareaController : Controller
    {
        Tarea[] tareas = new Tarea[]
        {
            new Tarea
            {
                Titulo = "Bases II",
                Descripcion = "Quiz de bases",
                FechaInicial = DateTime.Now,
                FechaFinal = DateTime.Now.AddDays(5),
                Estado = 1,
                Categoria = 2,
                Prioridad = 1,
                Dificultad = 3,
            },
            new Tarea
            {
                Titulo = "Diseno Proyecto",
                Descripcion = "Entrega de interfaces sin funcionalidad",
                FechaInicial = DateTime.Now,
                FechaFinal = DateTime.Now.AddDays(3),
                Estado = 2,
                Categoria = 1,
                Prioridad = 1,
                Dificultad = 4,
            },
            new Tarea
            {
                Titulo = "Asistencia",
                Descripcion = "Completar las horas semanales",
                FechaInicial = DateTime.Now,
                FechaFinal = DateTime.Now.AddDays(2),
                Estado = 2,
                Categoria = 4,
                Prioridad = 2,
                Dificultad = 1,
            },
            new Tarea
            {
                Titulo = "Asistencia",
                Descripcion = "Completar las horas semanales",
                FechaInicial = DateTime.Now,
                FechaFinal = DateTime.Now.AddDays(2),
                Estado = 1,
                Categoria = 4,
                Prioridad = 3,
                Dificultad = 1,
            },
        };

        Categoria[] categorias = new Categoria[]
        {
            new Categoria
            {
                Id = 1,
                Nombre = "Alimentacion",
                UsuarioCreador = 1,
            },
            new Categoria
            {
                Id = 2,
                Nombre = "Estudio",
                UsuarioCreador = 1,
            },
            new Categoria
            {
                Id = 3,
                Nombre = "Trabajo",
                UsuarioCreador = 1,
            },
            new Categoria
            {
                Id = 4,
                Nombre = "Entretenimiento",
                UsuarioCreador = 1,
            },
        };

        public IActionResult Index()
        {
            var userToken = HttpContext.Session.GetString("_UserToken");
            if (userToken == null) { // Si no hay un token de usuario
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewData["token"] = userToken;
                int contadorPendientes = 0;
                int contadorProceso = 0;
                int contadorTerminado = 0;
                ViewBag.Tareas = tareas;
                foreach (Tarea tarea in tareas)
                {
                    if (tarea.Estado == 1)
                    {
                        contadorPendientes += 1;
                    }
                    if (tarea.Estado == 2)
                    {
                        contadorProceso += 1;
                    }
                    if (tarea.Estado == 3)
                    {
                        contadorTerminado += 1;
                    }
                }
                ViewBag.Pendientes = contadorPendientes;
                ViewBag.Procesos = contadorProceso;
                ViewBag.Terminados = contadorTerminado;
                ViewBag.Categorias = categorias;
                return View();
            }
        }

        public IActionResult AddTask()
        {
            ViewData["token"] = HttpContext.Session.GetString("_UserToken"); // The view needs the token
            return View();
        }

        [HttpPost]
        public IActionResult AddTask(Tarea tarea)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Message = "Se agrego la tarea!";
                    ModelState.Clear();
                }

            }
            catch
            {
                ViewBag.Message = "Algo salio mal y no fue posible crear la tarea!";
            }
            return RedirectToAction("Index", "Tarea");
        }
    }
}
