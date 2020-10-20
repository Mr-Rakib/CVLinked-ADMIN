using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.SalesmanAndAbove)]
    public class PackagesController : Controller
    {
        private static PackageService packageService = new PackageService();
        // GET: ManagePackages
        public ActionResult Index()
        {

            string token = User.FindFirst("access_token").Value;
            List<Package> packageList = packageService.FindAll(token);
            return View(packageList);
        }

        public IActionResult GetPackagesById(int id)
        {
            string token = User.FindFirst("access_token").Value;
            Package package = packageService.FindById(id, token);
            return (package != null) ?
                PartialView("_PartialUpdPackage", package) :
                (IActionResult)View();
        }

        [Authorize(Roles = Roles.SupervisorAndAbove)]
        public IActionResult SavePackageView()
        {
            return PartialView("Save");
        }

        // POST: ManagePackages/Edit/5
        [Authorize(Roles = Roles.SupervisorAndAbove)]
        [HttpPost]
        public ActionResult Save(Package package)
        {
            string token = User.FindFirst("access_token").Value;
            string message = packageService.Save(package, token);
            if (string.IsNullOrEmpty(message))
            {
                TempData["EditSuccess"] = "Package Saved Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["EditError"] = message;
                return View();
            }
        }

        [Authorize(Roles = Roles.SupervisorAndAbove)]
        [HttpPost]
        public ActionResult Edit(Package package)
        {
            string token = User.FindFirst("access_token").Value;
            string message = packageService.Update(package, token);
            if (string.IsNullOrEmpty(message))
            {
                TempData["EditSuccess"] = "Package Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["EditError"] = message;
                return View();
            }
        }

        //----------------------------------------------------------------------------///
        [Authorize(Roles = Roles.SupervisorAndAbove)]
        public IActionResult DeactivePackages(int id)
        {
            string token = User.FindFirst("access_token").Value;
            TempData["Error"] = packageService.Deactive(id, token);
            return RedirectToAction(nameof(Index));
        }
        
        [Authorize(Roles = Roles.SupervisorAndAbove)]
        public IActionResult ActivePackage(int id)
        {
            string token = User.FindFirst("access_token").Value;
            TempData["Success"] = packageService.ActivePackage(id, token);
            return RedirectToAction(nameof(Index));
        }
       
    }
}