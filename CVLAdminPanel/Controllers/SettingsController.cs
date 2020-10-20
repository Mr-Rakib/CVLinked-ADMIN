using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CVLAdminPanel.Controllers
{
    public class SettingsController : Controller
    {
        private SettingsService settingsService;
        
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(string email)
        {
            settingsService = new SettingsService();
            if (settingsService.ResetPassword(email))
            {
                TempData["Success"] = Messages.EmailSended;
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["Error"] = Messages.InvalidAccount;
                return View();
            }
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(PasswordChangeDTO passwordChangeDTO, string ConfirmPassword)
        {
            settingsService = new SettingsService();
            string message = settingsService.ChangePassword(User.FindFirst("access_token").Value, passwordChangeDTO, ConfirmPassword);
            if (string.IsNullOrEmpty(message))
            {
                TempData["Success"] = Messages.PasswordChanged;
                return RedirectToAction(nameof(ChangePassword));
            }
            else
            {
                TempData["Error"] = message;
                return View();
            }
        }

    }
}
