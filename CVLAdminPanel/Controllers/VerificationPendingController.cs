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
    [Authorize(Roles =Roles.CallCenterAgentAndAbove)]
    public class VerificationPendingController : Controller
    {
        private readonly VerificationPendingService VerificationPendingService = new VerificationPendingService();

        [Route("VerificationPending/{rowsToSkip:int?}/{rowsToGet :int?}")]
        public IActionResult Index(int rowsToSkip, int rowsToGet)
        {
            string token = User.FindFirst("access_token").Value;
            CVSummaryCollection VerificationOnHold = VerificationPendingService.FindAll(token, rowsToSkip, rowsToGet);
            return View(VerificationOnHold);
        }
    }
}
