using CallCenterPanel.Helper;
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
    public class PromotionService : IServiceCRUD<Promotion, int>
    {
        private readonly string BaseUrl = URLs.API + "api/promotion/";
        public static HttpClient client;

        public List<Promotion> FindAll(string token)
        {
            List<Promotion> list = new List<Promotion>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync(BaseUrl + "all").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Promotion>>(result);
                }
            }

            list = list.OrderBy(s => s.PackageId).ToList();
            return list;
        }

        internal Promotion FindById(int id, string token)
        {
            Promotion promotion = new Promotion();
            promotion = FindAll(token).Find(u => u.PromotionId == id);
            return promotion;
        }

        //internal Promotion FindById(int id, string token)
        // {
        //     Promotion promotion = new Promotion();
        //     using (client = new HttpClient())
        //     {
        //         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //         var response = client.GetAsync($"{BaseUrl}{id}").Result;
        //         var Authorization = response.Headers.WwwAuthenticate;

        //         if (response.IsSuccessStatusCode)
        //         {
        //             var result = response.Content.ReadAsStringAsync().Result;
        //             promotion = JsonConvert.DeserializeObject<Promotion>(result);
        //         }
        //     }
        //     return promotion;
        // }

        internal string Update(Promotion Promotion, string token)
        {
            using (client = new HttpClient())
            {
                var json = new JavaScriptSerializer().Serialize(Promotion);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.PutAsync($"{BaseUrl}update", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ErrorResponse>(result).detail;
                }
            }
        }

        internal string Delete(int promotionId, string token)
        {
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.DeleteAsync($"{BaseUrl}delete/{promotionId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return Messages.Deleted;
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ErrorResponse>(result).detail;
                }
            }
        }

        public string Save(Promotion promotion, string token)
        {
            using (client = new HttpClient())
            {
                var json = new JavaScriptSerializer().Serialize(promotion);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage result = client.PostAsync(BaseUrl + "add", stringContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    return null;
                }
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ErrorResponse>(response).detail;
                }
            }
        }
    }
}
