using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
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
                Prioridad = 3,
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
                Prioridad = 4,
                Dificultad = 4,
            },
            new Tarea
            {
                Titulo = "Asistencia",
                Descripcion = "Completar las horas semanales",
                FechaInicial = DateTime.Now,
                FechaFinal = DateTime.Now.AddDays(2),
                Estado = 1,
                Categoria = 4,
                Prioridad = 4,
                Dificultad = 1,
            },
            new Tarea
            {
                Titulo = "Asistencia",
                Descripcion = "Completar las horas semanales",
                FechaInicial = DateTime.Now,
                FechaFinal = DateTime.Now.AddDays(2),
                Estado = 3,
                Categoria = 4,
                Prioridad = 4,
                Dificultad = 1,
            },
        };

        public IActionResult Index()
        {
            ViewBag.Tareas = tareas;
            return View();
        }
    }
}
