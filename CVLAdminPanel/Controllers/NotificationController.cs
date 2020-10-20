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
    public class NotificationController : Controller
    {
        private NotificationService NotificationService;
        public IActionResult Index()
        {
            NotificationService = new NotificationService();
            string token = User.FindFirst("access_token").Value;
            List<Notification> NotificationList = NotificationService.FindAll(token);
            return View(NotificationList);
        }

       // [Route("Notification/Handlar/{notificationId:int?}/{idType:string?}/{releventId:int?}")]
        public IActionResult Handler( int notificationId, string notificationType, int releventId)
        {
            NotificationService = new NotificationService();
            string token = User.FindFirst("access_token").Value;
            NotificationService.MarkAsRead(notificationId, token);
            Dictionary<string, string> ActionController = NotificationService.GetURL(notificationType);
            return RedirectToAction(ActionController["Action"], ActionController["Controller"], new { id = releventId} );
        }

    }
}
