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
    public class VerificationService
    {
        private readonly string BaseUrl = URLs.API + "api/";
        public HttpClient client;

        public string VerifyJobSeker(string token, int jobseekerId)
        {
            if (!string.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.PostAsync($"{BaseUrl}cv/verify/{jobseekerId}", null).Result;

                    var result = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return null;
                    }else
                    {
                        return JsonConvert.DeserializeObject<ErrorResponse>(result).detail;
                    }
                }
            }
            else return null;
        }

        public string UnverifyJobSeker(string token, int jobseekerId)
        {
            if (!string.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.PostAsync($"{BaseUrl}cv/unverify/{jobseekerId}", null).Result;

                    var result = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<ErrorResponse>(result).detail;
                    }
                }
            }
            else return null;
        }
    }
}