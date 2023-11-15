using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Handlers;
using Firebase.Auth;
using Newtonsoft.Json;

namespace ToDoList.Controllers
{
  public class AccountController : Controller
  {
    private static readonly string apiKEY = "AIzaSyAhfcu4Po8oWj-5IvUGivpeXsRpA0P_2fI";
    // GET: CuentaLogin

    FirebaseAuthProvider auth;
    HandlerObtenerDatos DBServer = new HandlerObtenerDatos();

    public AccountController()
    {
      auth = new FirebaseAuthProvider(new FirebaseConfig(apiKEY));
    }

    [HttpGet]
    public IActionResult SignUp()
    {
      ViewData["token"] = HttpContext.Session.GetString("_UserToken");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(RegisterModel model)
    {
      try
      {
        //create the user
        await auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password);
        //log in the new user
        var fbAuthLink = await auth
                        .SignInWithEmailAndPasswordAsync(model.Email, model.Password);
        string token = fbAuthLink.FirebaseToken;
        //saving the token in a session variable
        if (token != null)
        {

          var user = new Usuario(model.Name!, model.LastName!, model.SecondLastName!, model.Email!, model.IsAGame);
          var inserted = DBServer.InsertarNuevoUsuario(user);
          if (inserted)
          {
            var UserExistInDB = SetSession(model.Email!, token);
            if (UserExistInDB)
            {
              ViewData["username"] = model.Name! + " " + model.LastName!;
              if (DBServer.ObtenerEsJuego(model.Email!))
              {
                return RedirectToAction("GameIndex", "Tarea");
              }
              else
              {
                return RedirectToAction("ListIndex", "Tarea");
              }
            }
            else
            {
              // Poner que el usuario no existe
            }
          }
          else
          {
            // error inserting user to database
          }
        }
      }
      catch (FirebaseAuthException ex)
      {
        var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
        ModelState.AddModelError(String.Empty, firebaseEx.error.Message);
        return View(model);
      }

      return View();

    }

    [HttpGet]
    public IActionResult Login()
    {
      ViewData["token"] = HttpContext.Session.GetString("_UserToken");
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
      try
      {
        // login an existing user
        var fbAuthLink = await auth
                        .SignInWithEmailAndPasswordAsync(model.Email, model.Password);
        string token = fbAuthLink.FirebaseToken;
        // save the token to a session variable
        if (token != null)
        {
          var userExistInDB = SetSession(model.Email!, token);
          if (userExistInDB)
          {
            List<Usuario> usuarios = DBServer.ObtenerUsuario(DBServer.ObtenerIDUsuario(model.Email!));
            Usuario user = usuarios[0];
            TempData["username"] = user.Nombre + " " + user.PrimerApellido;
            if (DBServer.ObtenerEsJuego(model.Email!))
            {
              return RedirectToAction("GameIndex", "Tarea");
            }
            else
            {
              return RedirectToAction("ListIndex", "Tarea");
            }
          }
          else
          {
            //Que devuelva al registrarse
          }
        }

      }
      catch (FirebaseAuthException ex)
      {
        var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
        ModelState.AddModelError(String.Empty, firebaseEx.error.Message);
        return View(model);
      }

      return View();
    }

    [HttpGet]
    public IActionResult Logout()
    {
      HttpContext.Session.Remove("_UserToken");
      HttpContext.Session.Remove("_UserId");
      return RedirectToAction("Login", "Account");
    }

    public bool SetSession(string email, string token)
    {
      var guid = DBServer.ObtenerIDUsuario(email);
      HttpContext.Session.SetString("_UserId", guid);
      HttpContext.Session.SetString("_UserToken", token);
      bool isValid = Guid.TryParse(guid, out Guid guidOutput); // Guid retornado es valido
      if (isValid)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}