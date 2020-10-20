using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class BackOfficeUserController : Controller
    {
        private readonly BackOfficeUserService BackOfficeUserService = new BackOfficeUserService();
        public IActionResult Index()
        {
            string token = User.FindFirst("access_token").Value;
            List<BackOfficeUser> backOfficeUserList = BackOfficeUserService.FindAll(token);
            return View(backOfficeUserList);
        }

        public IActionResult Save()
        {
            return View("Save");
        }

        [HttpPost]
        public IActionResult Save(BackOfficeUser backOfficeUser)
        {
            if (ModelState.IsValid)
            {
                string token = User.FindFirst("access_token").Value;
                string message = BackOfficeUserService.Save(token, backOfficeUser);
                if (string.IsNullOrEmpty(message))
                {
                    TempData["Success"] = Messages.Created;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = message;
                    return View("Save");
                }
            }
            return View("Save");
        }

        public IActionResult Update(int id)
        {
            string token = User.FindFirst("access_token").Value;
            BackOfficeUser backOfficeUser = BackOfficeUserService.FindById(token, id);
            return View(backOfficeUser);
        }

        [HttpPost]
        public IActionResult Update(BackOfficeUser backOfficeUser)
        {
            if (ModelState.IsValid)
            {
                string token = User.FindFirst("access_token").Value;
                string message = BackOfficeUserService.Update(token, backOfficeUser);
                if (string.IsNullOrEmpty(message))
                {
                    TempData["Success"] = Messages.Updated;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = message;
                    return View();
                }
            }
            return View();
        }

        public IActionResult Details(int loginId)
        {
            string token = User.FindFirst("access_token").Value;
            BackOfficeUser backOfficeUser = BackOfficeUserService.FindById(token, loginId);
            return PartialView("Details", backOfficeUser);
        }

        public IActionResult Delete(int loginId)
        {
            string token = User.FindFirst("access_token").Value;
            string message = BackOfficeUserService.Delete(token, loginId);
            TempData["Error"] = message;
            return RedirectToAction(nameof(Index));
        }


        public IActionResult ChangeActiveStatus(int loginId, bool isActive)
        {
            string token = User.FindFirst("access_token").Value;
            if (BackOfficeUserService.SetActiveStatus(token, loginId, !isActive))
            {
                if (isActive)
                {
                    TempData["Success"] = Messages.Deactivate;
                }
                else
                {
                    TempData["Success"] = Messages.Activate;
                }
            }
            else
            {
                TempData["EditError"] = Messages.NotWorking;
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
