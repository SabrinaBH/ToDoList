using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Controllers;
using ToDoList.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Firebase.Auth;
using System.Threading;

namespace Diseno.Controllers
{
  public class TareaController : Controller
  {
    public HandlerObtenerDatos _handlerObtenerDatos;
    public TareaController()
    {
      _handlerObtenerDatos = new HandlerObtenerDatos();
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
      ViewData["token"] = GetUserToken();
      var userId = GetUserId();
      Tarea task = _handlerObtenerDatos.ObtenerTareasUsuario(userId).FirstOrDefault(l => l.Id == id);
      List<Categoria> categorias = _handlerObtenerDatos.ObtenerCategoriasUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
      List<Estado> estados = _handlerObtenerDatos.ObtenerEstadosUsuario(_handlerObtenerDatos.ObtenerIDUsuarioAdmin());
      ViewBag.Categorias = categorias;
      ViewBag.Estados = estados;
      return View("Details", task);
    }

    public string GetUserId() => HttpContext.Session.GetString("_UserId");
    public string GetUserToken() => HttpContext.Session.GetString("_UserToken");

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

    private bool TaskExists(string id)
    {
      var user = GetUserId();
      var tareaBD = _handlerObtenerDatos.ObtenerTareasUsuario(user).Where(t => t.Id == id).FirstOrDefault();
      if (tareaBD == null) { return false; }
      return true;
    }
  }
}
