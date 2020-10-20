using CVLAdminPanel.Models;
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
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace CVLAdminPanel.BLL.Services
{
    public class SettingsService
    {
        private readonly string BaseUrl = URLs.API + "api/";
        public HttpClient client;

        public bool ResetPassword(string email)
        {
            using (client = new HttpClient())
            {
                var response = client.PostAsync($"{BaseUrl}account/resetpasswordasync?email={email}", null).Result;

                var Authorization = response.Headers.WwwAuthenticate;
                return (response.IsSuccessStatusCode) ? true : false;
            }
        }

        public string ChangePassword(string token, PasswordChangeDTO passwordChangeDTO, string confirmPassword)
        {
            if(confirmPassword == passwordChangeDTO.newPassword)
            {
                using (client = new HttpClient())
                {
                    var json = new JavaScriptSerializer().Serialize(passwordChangeDTO);
                    
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                    client.DefaultRequestHeaders.UserAgent.ParseAdd($"CVlinedAdminz {GetIPAddress()}");
                    
                    var response = client.PostAsync($"{BaseUrl}account/changepasswordwotoken", stringContent).Result;
                    
                    if (response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    else
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
                        return errorResponse.detail;
                    }
                }
            }return Messages.PasswordNotMatched;
        }

        private string GetIPAddress()
        {
            var address = Dns.GetHostEntry(Dns.GetHostName());

            if (address.AddressList.Count() != 0 && address.AddressList.Count() >= 2)
            {
                return address.AddressList[1].ToString();
            }
            else return null;
        }
    }
}
