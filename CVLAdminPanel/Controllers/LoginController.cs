using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using CVLAdminPanel.Utility;

namespace CVLAdminPanel.Controllers
{
    public class LoginController : Controller
    {
        private readonly static LoginService loginService = new LoginService();

        public ActionResult Login()
        {
            return IsAuthorize();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                Token token = loginService.Login(login);
                if (token != null)
                {
                    if (GetLogin(token).IsCompleted)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else TempData["Error"] = Messages.Unauthorize;
                }
                else
                {
                    TempData["Error"] = Messages.InvalidUserLogin;
                }
                return View();
            }
            else return View();
        }

        private Task GetLogin(Token token)
        {
            var identity = new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.Name, token.email),
                new Claim(ClaimTypes.Role, token.user_type.ToLower()),
                new Claim ("access_token", token.access_token)

            }, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.SetString("access_token", token.access_token);
            var principal = new ClaimsPrincipal(identity);
            return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        private ActionResult IsAuthorize()
        {
            return (string.IsNullOrEmpty(User.Identity.Name)) ?
                    View() : (ActionResult)RedirectToAction("Index", "Dashboard");
        }

    }
}