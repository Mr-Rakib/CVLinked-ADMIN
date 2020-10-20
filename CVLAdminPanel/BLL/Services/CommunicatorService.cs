using CallCenterPanel.Helper;
using CVLAdminPanel.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CVLAdminPanel.BLL.Services
{
    public class CommunicatorService
    {
        public HttpClient client;
        public bool SendPushNotification(PushNotification push)
        {
            var response = false;
            var serverKey = "key=AAAAuE3A8JE:APA91bF8bjhiKB-4FLt1j4rMIdwwf_p6sDb0kaU6oY4m2LxolZ0yWyqCC4z4oglYHyaBLU78XoaatmKXWJhQUhYJUAYo2iIMMFzcaJCsVQCetSI9MwOoE336VPrUvbP71I3M2IM5nv3M";
            try
            {
                var recipient = (push.Recipient == "all") ? push.Recipient : push.Recipient.Replace("@", "");
                var jsonMessage = "{\"data\": {\"title\": \"" + push.Title + "\", \"content\" : \"" + push.Content + "\",\"imageUrl\": null, \"gameUrl\": \"https://h5.4j.com/Ninja-Run/index.php?pubid=noad\"},\"to\": \"/topics/" + recipient + "\"}";
                //var jsonMessage = "{\"data\": {\"title\": \"" + push.Title + "\", \"content\" : \"" + push.Content + "\",\"imageUrl\": \"" + push.ImageUrl + "\", \"gameUrl\": \"https://h5.4j.com/Ninja-Run/index.php?pubid=noad\"},\"to\": \"/topics/" + recipient + "\"}";
                var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
                request.Headers.TryAddWithoutValidation("Authorization", serverKey);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = client.SendAsync(request).Result;
                    if ((int)result.StatusCode == 200)
                    {
                        response = true;
                    }
                    if ((int)result.StatusCode == 500)
                    {
                        response = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
