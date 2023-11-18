using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Controllers;
using ToDoList.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Firebase.Auth;

namespace Diseno.Controllers
{
    public class TareaController : Controller
    {
        public HandlerObtenerDatos _handlerObtenerDatos;
        public TareaController() { 
            _handlerObtenerDatos = new HandlerObtenerDatos();
        }

        public IActionResult ListIndex()
        {
            var userToken = HttpContext.Session.GetString("_UserToken");
            if (userToken == null) { // Si no hay un token de usuario
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var userId = HttpContext.Session.GetString("_UserId");
                ViewData["token"] = userToken;

                List<Tarea> listaTareas = _handlerObtenerDatos.ObtenerTareasUsuario(userId.ToUpper());
                List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
                int contadorPendientes = 0;
                int contadorProceso = 0;
                int contadorTerminado = 0;
                ViewBag.Categorias = categorias;
                ViewBag.Tareas = listaTareas;
                foreach (Tarea tarea in listaTareas)
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
                return View();
            }
        }

        public IActionResult GameIndex()
        {
            var userToken = HttpContext.Session.GetString("_UserToken");
            if (userToken == null)
            { // Si no hay un token de usuario
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var userId = HttpContext.Session.GetString("_UserId");
                ViewData["token"] = userToken;
                List<Tarea> listaTareas = _handlerObtenerDatos.ObtenerTareasUsuario(userId.ToUpper());
                List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
        int contadorPendientes = 0;
                int contadorTerminado = 0;
                ViewBag.Categorias = categorias;
                ViewBag.Tareas = listaTareas;
                foreach (Tarea tarea in listaTareas)
                {
                    if (tarea.Estado == 1)
                    {
                        contadorPendientes += 1;
                    }
                    if (tarea.Estado == 3)
                    {
                        contadorTerminado += 1;
                    }
                }
                ViewBag.Pendientes = contadorPendientes;
                ViewBag.Terminados = contadorTerminado;

                return View();
            }
    }
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewData["token"] = HttpContext.Session.GetString("_UserToken"); // The view needs the token
            ViewData["userId"] = HttpContext.Session.GetString("_UserId");
            List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
            ViewBag.Categorias = categorias;
            return View();
        }

        [HttpPost]
        public IActionResult AddTask(Tarea tarea)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var task = new Tarea(
                        tarea.Titulo,
                        tarea.Descripcion,
                        tarea.FechaInicial,
                        tarea.FechaFinal,
                        tarea.Estado!,
                        tarea.Categoria!,
                        tarea.Prioridad,
                        tarea.Dificultad,
                        tarea.UsuarioCreador!
                    );
                    bool insertado = _handlerObtenerDatos.InsertarNuevaTarea(task);
                    ViewBag.Message = "Se agrego la tarea!";
                    ModelState.Clear();
                }

            }
            catch
            {
                ViewBag.Message = "Algo salio mal y no fue posible crear la tarea!";
            }
            return RedirectToAction("ListIndex", "Tarea");
        }
    }
}
