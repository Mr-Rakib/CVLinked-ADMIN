using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.CallCenterAgentAndAbove)]
    public class VerificationController : Controller
    {
        public VerificationService verificationService = new VerificationService();
        public IActionResult Verify(int jobseekerId)
        {
            if (jobseekerId > 0)
            {
                string token = User.FindFirst("access_token").Value;
                string message = verificationService.VerifyJobSeker(token, jobseekerId);
                if (string.IsNullOrEmpty(message))
                {
                    TempData["Success"] = Messages.Verified;
                    return RedirectToAction("Index", "VerificationOnHold", new { rowsToSkip = 0, rowsToGet = 10 });
                }
                else
                {
                    TempData["Warning"] = message;
                }
            }
            return RedirectToAction("ChangeLog", "ChangeLog", new { id = jobseekerId });
        }

        public IActionResult Unverify(int jobseekerId)
        {
            if (jobseekerId > 0)
            {
                string token = User.FindFirst("access_token").Value;
                string message = verificationService.VerifyJobSeker(token, jobseekerId);
                if (string.IsNullOrEmpty(message))
                {
                    TempData["Success"] = Messages.Verified;
                }
                else
                {
                    TempData["Warning"] = message;
                }
            }
            return RedirectToAction("ChangeLog", "ChangeLog", new { id = jobseekerId });
        }
    }
}
