using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.SalesmanAndAbove)]
    public class PromotionalOfferController : Controller
    {
        private PromotionService promotionService = new PromotionService();
        private PackageService packageService = new PackageService();
        public ActionResult Index()
        {
            string token = User.FindFirst("access_token").Value;
            List<Promotion> promotionList = promotionService.FindAll(token);
            return View(promotionList);
        }

        [Authorize(Roles = Roles.SupervisorAndAbove)]
        public IActionResult PromotionAddView()
        {
            string token = User.FindFirst("access_token").Value;
            List<Package> package = packageService.FindAll(token);
            ViewData["Packages"] = new SelectList(package, "packageId", "packageName");
            return (package != null) ? PartialView("_partialAddPromotion") : (IActionResult)View();
        }

        [Authorize(Roles = Roles.SupervisorAndAbove)]
        [HttpPost]
        public ActionResult AddPromotion(Promotion promotion)
        {
            string token = User.FindFirst("access_token").Value;
            string message = promotionService.Save(promotion, token);
            if (string.IsNullOrEmpty(message))
            {
                TempData["Success"] = "New Promotion Added Successfully";
                return RedirectToAction("Index", "PromotionalOffer");
            }
            else
            {
                TempData["Error"] = message;
                return RedirectToAction("Index", "PromotionalOffer");
            }
        }

        [Authorize(Roles = Roles.SupervisorAndAbove)]
        public IActionResult Update(int id)
        {
            string token = User.FindFirst("access_token").Value;
            Promotion promotion = promotionService.FindById(id, token);
            return View(promotion);
        }

        [Authorize(Roles = Roles.SupervisorAndAbove)]
        [HttpPost]
        public ActionResult Update(Promotion promotion)
        {
            string token = User.FindFirst("access_token").Value;
            string message = promotionService.Update(promotion, token);
            if (string.IsNullOrEmpty(message))
            {
                TempData["Success"] = Messages.Updated;
                return RedirectToAction("Index", "PromotionalOffer");
            }
            else
            {
                TempData["Error"] = message;
                return View();
            }
        }

        [Authorize(Roles = Roles.SupervisorAndAbove)]
        public ActionResult DeletePromotions(int promotionId)
        {
            string token = User.FindFirst("access_token").Value;
            string message = promotionService.Delete(promotionId, token);
            TempData["Error"] = message; 
            return RedirectToAction("Index");
        }
        [Authorize(Roles = Roles.SupervisorAndAbove)]
        public IActionResult FindPromotionById(int promotionId)
        {
            string token = User.FindFirst("access_token").Value;
            Promotion promotion = promotionService.FindById(promotionId, token);
            return PartialView("_partialDeletePromotion", promotion);
        }
    }
}