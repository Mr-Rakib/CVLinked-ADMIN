using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CVLAdminPanel.BLL.Services
{
    public class NotificationService
    {
        private readonly string BaseUrl = URLs.API + "api/notification/";
        public HttpClient client;

        public List<Notification> FindAll(string token)
        {
            List<Notification> NotificationList = new List<Notification>();
            if (!String.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.GetAsync($"{BaseUrl}/notifications").Result;

                    var Authorization = response.Headers.WwwAuthenticate;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        NotificationList = JsonConvert.DeserializeObject<List<Notification>>(result);
                    }
                }
                return NotificationList;
            }
            else return null;
        }

        internal Dictionary<string, string> GetURL(string notificationType)
        {
            Dictionary<string, string> ActionController = new Dictionary<string, string>();
            switch (notificationType)
            {
                case NotificationType.new_employer_registration:
                    ActionController.Add("Controller", "Company");
                    ActionController.Add("Action", "BasicDetails");
                    break;

                case NotificationType.new_package_subscription:
                    ActionController.Add("Controller", "Company");
                    ActionController.Add("Action", "Details");
                    break;

                default:
                    ActionController.Add("Controller", "Dashboard");
                    ActionController.Add("Action", "Index");
                    break;
            }

            return ActionController;
        }

        internal bool MarkAsRead(int notificationId, string token)
        {
            List<Notification> NotificationList = new List<Notification>();
            if (!String.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.GetAsync($"{BaseUrl}/markasread/{notificationId}").Result;

                    var Authorization = response.Headers.WwwAuthenticate;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
