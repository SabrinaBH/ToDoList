using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Firebase.Auth;
using Newtonsoft.Json;

namespace ToDoList.Controllers
{
  public class AccountController : Controller
  {
    private static readonly string apiKEY = "AIzaSyAhfcu4Po8oWj-5IvUGivpeXsRpA0P_2fI";
    // GET: CuentaLogin

    FirebaseAuthProvider auth;

    public AccountController()
    {
      auth = new FirebaseAuthProvider(new FirebaseConfig(apiKEY));
    }

    public IActionResult Index()
    {
      return View();
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
          HttpContext.Session.SetString("_UserToken", token);

          return RedirectToAction("Index", "Tarea");
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
          HttpContext.Session.SetString("_UserToken", token);
          // HttpContext.Session.SetString("_UserEmail", model.Email!);

          return RedirectToAction("Index", "Tarea");
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
      return RedirectToAction("Login", "Account");
    }
  }
}