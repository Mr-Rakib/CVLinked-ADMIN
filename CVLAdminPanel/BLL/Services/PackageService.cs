using CVLAdminPanel.BLL.Services.Interfaces;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CVLAdminPanel.BLL.Services
{
    public class PackageService : IServiceCRUD<Package, int>
    {
        private readonly string BaseUrl = URLs.API + "api/";
        public HttpClient client;

        public List<Package> FindAll(string token)
        {
            List<Package> list = new List<Package>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}package/all").Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Package>>(result);
                }
            }
            list = list.OrderBy(s => s.packageId).ToList();
            return list;
        }

        public Package FindById(int id, string token)
        {
            Package Package = new Package();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}package/{id}").Result;
                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Package = JsonConvert.DeserializeObject<Package>(result);
                }
            }
            return Package;
        }

        public string Save(Package package, string token)
        {
            using (client = new HttpClient())
            {
                var json = new JavaScriptSerializer().Serialize(package);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage result = client.PostAsync($"{BaseUrl}package/add", stringContent).Result;

                if (result.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ErrorResponse>(response).detail;
                }
            }
        }

        public string Deactive(int id, string token)
        {
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.DeleteAsync($"{BaseUrl}package/{id}").Result;
                var Authorization = response.Headers.WwwAuthenticate;

                var result = response.Content.ReadAsStringAsync().Result;
                return (response.IsSuccessStatusCode) ?
                    Messages.Deactivate : JsonConvert.DeserializeObject<ErrorResponse>(result).detail;
            }
        }

        internal string ActivePackage(int id, string token)
        {
            Package package = FindById(id, token);
            package.isActive = true;
            string message = Update(package, token);
            return (string.IsNullOrEmpty(message)) ? Messages.Activate : message;
        }

        public string Update(Package package, string token)
        {
            using (client = new HttpClient())
            {
                var json = new JavaScriptSerializer().Serialize(package);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage result = client.PutAsync($"{BaseUrl}package/update", stringContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ErrorResponse>(response).detail;
                }
            }
        }

        public List<PackageSubscriptionHistory> PackageSubscriptionHistoryByEmployerId(int id, string token)
        {
            List< PackageSubscriptionHistory> packageSubscriptionHistories = new List<PackageSubscriptionHistory>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}callcenter/subscription/history/{id}").Result;
                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    packageSubscriptionHistories = JsonConvert.DeserializeObject<List<PackageSubscriptionHistory>>(result);
                }
            }
            return packageSubscriptionHistories;
        }

    }
}
