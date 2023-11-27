using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Controllers;
using ToDoList.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Firebase.Auth;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diseno.Controllers
{
  public class TareaController : Controller
  {
    public HandlerObtenerDatos _handlerObtenerDatos = new();
    public List<SelectListItem> prioridades = new();
    public List<SelectListItem> dificultad = new();

    public TareaController()
    {
      prioridades.Add(new SelectListItem { Text = "Baja", Value = "3" });
      prioridades.Add(new SelectListItem { Text = "Media", Value = "2" });
      prioridades.Add(new SelectListItem { Text = "Alta", Value = "1" });

      dificultad.Add(new SelectListItem { Text = "1", Value = "1" });
      dificultad.Add(new SelectListItem { Text = "2", Value = "2" });
      dificultad.Add(new SelectListItem { Text = "3", Value = "3" });
      dificultad.Add(new SelectListItem { Text = "4", Value = "4" });
      dificultad.Add(new SelectListItem { Text = "5", Value = "5" });
    }

    public IActionResult ListIndex()
    {
      var userToken = GetUserToken();
      if (userToken == null)
      { // Si no hay un token de usuario
        return RedirectToAction("Login", "Account");
      }
      else
      {
        var userId = GetUserId();
        ViewData["token"] = userToken;

        List<Tarea> listaTareas = _handlerObtenerDatos.ObtenerTareasUsuario(userId.ToUpper());
        List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
        List<Estado> estados = _handlerObtenerDatos.ObtenerEstadosUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
        int contadorPendientes = 0;
        int contadorProceso = 0;
        int contadorTerminado = 0;
        ViewBag.Categorias = categorias;
        ViewBag.Tareas = listaTareas;
        ViewBag.Estados = estados;
        foreach (Tarea tarea in listaTareas)
        {
          if (tarea.Estado == 0)
          {
            contadorPendientes += 1;
          }
          if (tarea.Estado == 1)
          {
            contadorProceso += 1;
          }
          if (tarea.Estado == 2)
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
      var userToken = GetUserToken();
      if (userToken == null)
      { // Si no hay un token de usuario
        return RedirectToAction("Login", "Account");
      }
      else
      {
        var userId = GetUserId();
        ViewData["token"] = userToken;
        List<Tarea> listaTareas = _handlerObtenerDatos.ObtenerTareasUsuario(userId.ToUpper());
        List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
        int contadorPendientes = 0;
        int contadorTerminado = 0;
        ViewBag.Categorias = categorias;
        ViewBag.Tareas = listaTareas;
        foreach (Tarea tarea in listaTareas)
        {
          if (tarea.Estado == 0)
          {
            contadorPendientes += 1;
          }
          if (tarea.Estado == 2)
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
      ViewData["token"] = GetUserToken(); // The view needs the token
      ViewData["userId"] = GetUserId();
      List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
      ViewBag.Categorias = categorias;
      ViewBag.Prioridades = prioridades;
      ViewBag.Dificultad = dificultad;
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

    [Route("/Tarea/{id}/Details")]
    public IActionResult Details(string id)
    {
      ViewData["isGame"] = GetIsGame();
      ViewData["token"] = GetUserToken();
      var userId = GetUserId();
      Tarea task = _handlerObtenerDatos.ObtenerTareasUsuario(userId).FirstOrDefault(l => l.Id == id);
      List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
      List<Estado> estados = _handlerObtenerDatos.ObtenerEstadosUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
      ViewBag.Categorias = categorias;
      ViewBag.Estados = estados;
      ViewBag.Prioridades = prioridades;
      ViewBag.Dificultad = dificultad;
      return View("Details", task);
    }

    public string GetUserId() => HttpContext.Session.GetString("_UserId");
    public string GetUserToken() => HttpContext.Session.GetString("_UserToken");
    public string GetIsGame() => HttpContext.Session.GetString("_IsGame");

    [Route("/Tarea/{id}/Delete")]
    public IActionResult Delete(string id)
    {
      var user = GetUserId();
      var tarea = _handlerObtenerDatos.ObtenerTareasUsuario(user).Where(t => t.Id == id).FirstOrDefault();
      if (tarea != null)
      {
        _handlerObtenerDatos.BorrarTarea(tarea);
      }

      return RedirectToAction("ListIndex", "Tarea");
    }

    [HttpPost]
    public IActionResult Edit(Tarea tarea)
    {
      tarea.UsuarioCreador = GetUserId();
      if (ModelState.IsValid)
      {
        try
        {
          _handlerObtenerDatos.ActualizarTarea(tarea);
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!TaskExists(tarea.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction("ListIndex", "Tarea");
      }
      return View(tarea);
    }

    [HttpPost]
    public string UpdateState(string data)
    {
      string[] datos = data.Split('|');
      try
      {
        if (_handlerObtenerDatos.ActualizarEstado(datos[0], Int32.Parse(datos[1])))
        {
          return "Actualizado";
        }
        else
        {
          return "Nel";
        }
      }
      catch (DbUpdateConcurrencyException)
      {
        return "Nel";
      }
    }

    private bool TaskExists(string id)
    {
      var user = GetUserId();
      var tareaBD = _handlerObtenerDatos.ObtenerTareasUsuario(user).Where(t => t.Id == id).FirstOrDefault();
      if (tareaBD == null) { return false; }
      return true;
    }
  }
}
