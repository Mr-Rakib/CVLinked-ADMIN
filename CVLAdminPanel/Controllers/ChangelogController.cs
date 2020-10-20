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
    public class ChangelogController : Controller
    {
        private ChangeLogService changeLogService = new ChangeLogService();

        [Route("ChangeLog/{id:int?}")]
        public IActionResult ChangeLog(int id)
        {
            string token = User.FindFirst("access_token").Value;
            List<ChangeLog> changeLog = changeLogService.GetChangeLogsByLoginId(token, id);
            TempData["Verification-On-Hold-User-Id"] = id;
            return View(changeLog);
        }

        [Route("ChangeLog/ApproveChangeLog/{changeLogId:Guid?}/{userId:int?}")]
        public IActionResult ApproveChangeLog(Guid changeLogId, int userId)
        {
            string token = User.FindFirst("access_token").Value;

            if (changeLogService.ApproveChangeLogsByChangeLogId(token, changeLogId))
            {
                TempData["Success"] = Messages.Approved;
            }
            else
            {
                TempData["Error"] = Messages.NotApproved;
            }
            return RedirectToAction("ChangeLog", new { id = userId });
        }

    }
}
