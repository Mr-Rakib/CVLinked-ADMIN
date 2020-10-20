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
    [Authorize(Roles = Roles.CallCenterAgentAndAbove)]
    public class RequestedSkillController : Controller
    {
        private readonly RequestedSkillService RequestedSkillService = new RequestedSkillService();
        public IActionResult UnapprovedSkills()
        {
            string token = User.FindFirst("access_token").Value;
            List<RequestedSkill> RequestedSkillList = RequestedSkillService.FindAlllUnapprovedSkill(token);
            return View(RequestedSkillList);
        }

        public IActionResult ApprovedSkills()
        {
            string token = User.FindFirst("access_token").Value;
            List<RequestedSkill> RequestedSkillList = RequestedSkillService.FindAlllApprovedSkill(token);
            return View(RequestedSkillList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RequestedSkill RequestedSkill)
        {
            if (ModelState.IsValid)
            {
                string token = User.FindFirst("access_token").Value;
                string message = RequestedSkillService.SaveRequestedSkill(token, RequestedSkill);
                if (string.IsNullOrEmpty(message))
                {
                    TempData["Success"] = Messages.Created;
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

        public IActionResult ApproveRequestedSkill(int id)
        {
            string token = User.FindFirst("access_token").Value;
            string message = RequestedSkillService.ApproveSkillById(token, id);
            return View(RequestedSkill);
        }

        public IActionResult ApproveRequestedSkill(int loginId)
        {
            string token = User.FindFirst("access_token").Value;
            string message = RequestedSkillService.Delete(token, loginId);
            TempData["Error"] = message;
            return RedirectToAction(nameof(Index));
        }


    }
}
