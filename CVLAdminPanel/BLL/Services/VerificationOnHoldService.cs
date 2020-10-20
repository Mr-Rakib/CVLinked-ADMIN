using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CVLAdminPanel.BLL.Services
{
    public class VerificationOnHoldService
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
                    var response = client.GetAsync($"{BaseUrl}callcenter/cvs/verificationonhold/{rowsToSkip}/{rowsToGet}").Result;

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
        
        public string PrettyJson(string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(unPrettyJson);
            return System.Text.Json.JsonSerializer.Serialize(jsonElement, options);
        }
    }


}


