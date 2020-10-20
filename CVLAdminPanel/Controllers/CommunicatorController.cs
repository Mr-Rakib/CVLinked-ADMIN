using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CallCenterPanel.Helper;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.CallCenterAgentAndAbove)]
    public class CommunicatorController : Controller
    {
        readonly CommunicatorService _service = new CommunicatorService();
        private readonly ILogger<CommunicatorController> _logger;

        public IActionResult Index()
        {
            PushNotification model = new PushNotification();
            model.Title = "CVLinked JobSeeker";
            return View("PushNotification", model);
        }

        public async Task<bool> SendPushNotificationToAlls(string content, string title)
        {
            var serverKey = "key=AAAAuE3A8JE:APA91bF8bjhiKB-4FLt1j4rMIdwwf_p6sDb0kaU6oY4m2LxolZ0yWyqCC4z4oglYHyaBLU78XoaatmKXWJhQUhYJUAYo2iIMMFzcaJCsVQCetSI9MwOoE336VPrUvbP71I3M2IM5nv3M";
            try
            {
                var jsonMessage = "{\"data\": {\"title\": \"" + title + "\", \"content\" : \"" + content + "\",\"imageUrl\": null, \"gameUrl\": \"https://h5.4j.com/Ninja-Run/index.php?pubid=noad\"},\"to\": \"/topics/all\"}";
                var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
                request.Headers.TryAddWithoutValidation("Authorization", serverKey);
                request.Content = new StringContent(jsonMessage);
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    if ((int)result.StatusCode == 200)
                    {
                        //this.TempData["Success"] = "Notification sends to All successfully";
                        return true;
                    }
                    if ((int)result.StatusCode == 500)
                    {
                        //this.TempData["Error"] = "Failed to Connect Server";
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                Logger.SendErrorToText(ex);
            }

            return false;
        }

        public IActionResult SendIndividualNotification(PushNotification notification)
        {
            try
            {
                PushNotification push = new PushNotification
                {
                    Title = notification.Title,
                    Content = notification.Content,
                    Recipient = notification.Recipient,
                    ImageUrl = "https://cdn.shortpixel.ai/client/to_webp,q_lossy,ret_img/https://hashtagdevs.tech/wp-content/uploads/elementor/thumbs/298x96-dark-oml7xgzg2pdoy2sijhoxr15he37817vgdvxhr88lpw-omlb01py22r0qo69usdykmml4w6ojdup96qt62abqs.png"
                };

                var result = _service.SendPushNotification(push);
                if (result)
                {
                    this.TempData["Success"] = "Push Notification has been Initiated Successfully";
                }
                else
                {
                    this.TempData["Error"] = "Failed to sent Push Notification. Try again";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                Logger.SendErrorToText(ex);
            }

            ModelState.Clear();
            return View("PushNotification");
        }

        public IActionResult SendNotificationToAll(PushNotification notification)
        {
            try
            {
                PushNotification push = new PushNotification
                {
                    Title = notification.Title,
                    Content = notification.Content,
                    Recipient = "all"
                };

                var result = _service.SendPushNotification(push);
                if (result)
                {
                    this.TempData["Success"] = "Push Notification has been Initiated Successfully";
                }
                else
                {
                    this.TempData["Error"] = "Failed to sent Push Notification. Try again";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }

            return View("PushNotification");
        }
    }
}