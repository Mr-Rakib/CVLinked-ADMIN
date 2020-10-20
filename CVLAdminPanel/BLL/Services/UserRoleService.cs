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
    public class UserRoleService
    {
        private string BaseUrl = string.Concat(URLs.API, "api/");
        public HttpClient client;
        ///api/account/roles/backoffice/all

        public List<Role> FindAll()
        {
            List<Role> list = new List<Role>();

            using (client = new HttpClient())
            {
                var response = client.GetAsync(BaseUrl + "account/roles/backoffice/all").Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Role>>(result);
                }
            }
            list = list.OrderBy(s => s.roleName).ToList();
            return list;
        }
    }
}
