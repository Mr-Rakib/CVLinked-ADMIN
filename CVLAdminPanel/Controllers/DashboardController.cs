using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.ALLUSER + "," + Roles.Developer)]
    public class DashboardController : Controller
    {
        private static DashboardService DashboardService = new DashboardService();
        // GET: Dashboard
        public ActionResult Index()
        {
            string token = User.FindFirst("access_token").Value;
            string role = User.FindFirst(ClaimTypes.Role).Value;
            if (!string.IsNullOrEmpty(token))
            {
                string action = DashboardService.GetActionForUser(role);
                if (!string.IsNullOrEmpty(action))
                {
                    return RedirectToAction(action);
                }
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Admin()
        {
            string token = User.FindFirst("access_token").Value;
            return View(DashboardService.FindAllDataForAdminAndSupervisor(token));
        }

        public ActionResult Supervisor()
        {
            string token = User.FindFirst("access_token").Value;
            return View();
        }

        public ActionResult Salesman()
        {
            string token = User.FindFirst("access_token").Value;
            return View();
        }

        public ActionResult CallCenterAgent()
        {
            string token = User.FindFirst("access_token").Value;
            return View();
        }

        public ActionResult Developer()
        {
            string token = User.FindFirst("access_token").Value;
            return View();
        }
    }
}