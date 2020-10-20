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
    public class VerificationPendingService
    {
        private readonly string BaseUrl = URLs.API + "api/";
        public HttpClient client;

        public CVSummaryCollection FindAll(string token, int rowsToSkip, int rowsToGet)
        {
            CVSummaryCollection verificationOnHold = new CVSummaryCollection();
            if (!string.IsNullOrEmpty(token))
            {
                rowsToSkip = rowsToSkip <= 0 ? 0 : rowsToSkip;
                rowsToGet = rowsToGet <= 0 ? 10 : rowsToGet;
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.GetAsync($"{BaseUrl}callcenter/cvs/verificationpending/{rowsToSkip}/{rowsToGet}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        verificationOnHold = JsonConvert.DeserializeObject<CVSummaryCollection>(result);
                    }
                }
                return verificationOnHold;
            }
            else return null;
        }
    }
}
