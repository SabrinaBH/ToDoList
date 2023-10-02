using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Models;
using Firebase.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {
        private static string apiKEY = "AIzaSyAhfcu4Po8oWj-5IvUGivpeXsRpA0P_2fI";
        // GET: CuentaLogin

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(RegisterModel model)
        {
            try
            {
                var autho = new FirebaseAuthProvider(new FirebaseConfig(apiKEY));

                var a = await autho.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);
                ModelState.AddModelError(string.Empty, "Please Verify your email");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }
        // GET: CuentaLogin/Edit/5

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnURL)
        {
           /* try
            {
                if (Request.IsAuthenticated)
                {
                    return RedirectToRoute(returnURL);
                }
                return RedirectToRoute(returnURL);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }*/
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public async Task<ActionResult> Login(LoginModel model, string returnURL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKEY));
                    var a = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
                    string token = a.FirebaseToken;
                    var user = a.User;
                    if (token != "")
                    {
                        SignInUser(user.Email, token, false);
                        return RedirectToLocal(returnURL);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error username or password.");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return View();
        }
        // POST: CuentaLogin/Edit/5
        private void SignInUser(string email, string token, bool isPersistent)
        {
            // Initialization.
            var claims = new List<Claim>();

            try
            {
                // Setting
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Authentication, token));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                //var ctx = Request.GetOwinContext();
                //var authenticationManager = ctx.Authentication;
                // Sign In.
                //authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.
                    return Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // Info.
            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            //var ctx = Request.GetOwinContext();
            //var authenticationManager = ctx.Authentication;
            //authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "CuentaLogin");
        }

        // POST: CuentaLogin/Delete/5

    }
}