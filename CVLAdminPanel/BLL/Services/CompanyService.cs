using CVLAdminPanel.BLL.Services.Interfaces;
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
    public class CompanyService : IServiceCRUD<Company, int>
    {
        private readonly string BaseUrl = URLs.API + "api/callcenter/";
        private HttpClient client;
        private PackageService packageService;

        public List<Company> FindAll(string token)
        {
            List<Company> list = new List<Company>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync(BaseUrl + "allemployers").Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<Company>>(result);
                }
            }
            return list;
        }


        internal Company FindCompanyBasicById(string token, int id)
        {
            Company Company = new Company();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}employer/basic/{id}").Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Company = JsonConvert.DeserializeObject<Company>(result);
                }
            }
            return Company;
        }

        public string DeleteCompnayById(string token, int id)
        {
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.DeleteAsync($"{BaseUrl}deleteemployer/{id}").Result;

                var Authorization = response.Headers.WwwAuthenticate;

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

        internal Employer FindById(string token, int id)
        {
            Employer Employer = new Employer();
            if (!String.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.GetAsync(BaseUrl + $"employer/{id}").Result;

                    var Authorization = response.Headers.WwwAuthenticate;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Employer = JsonConvert.DeserializeObject<Employer>(result);

                        packageService = new PackageService();
                        Employer.PackageSubscriptionHistories = packageService.PackageSubscriptionHistoryByEmployerId(Employer.employerId, token);
                    }
                    else
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        //var Errorresponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
                        return null;
                    }
                }
                return Employer;
            }
            else return null;
        }
    }
}
