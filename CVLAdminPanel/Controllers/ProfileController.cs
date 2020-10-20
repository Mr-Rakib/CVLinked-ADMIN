using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVLAdminPanel.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult MyProfile()
        {
            return View();
        }

        public IActionResult Setting()
        {
            return View();
        }

        public IActionResult Activity()
        {
            return View();
        }

        public IActionResult Support()
        {
            return View();
        }
    }
}