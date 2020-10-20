using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.CallCenterAgentAndAbove)]
    public class VerificationOnHoldController : Controller
    {
        private readonly VerificationOnHoldService VerificationOnHoldService = new VerificationOnHoldService();

        [Route("VerificationOnHold/{rowsToSkip:int?}/{rowsToGet :int?}")]
        public IActionResult Index(int rowsToSkip, int rowsToGet)
        {
            string token = User.FindFirst("access_token").Value;
            CVSummaryCollection VerificationOnHold = VerificationOnHoldService.FindAll(token, rowsToSkip, rowsToGet);
            return View(VerificationOnHold);
        }

    }
}
