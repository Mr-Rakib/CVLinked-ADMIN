
using CVLAdminPanel.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using CVLAdminPanel.Utility;

namespace CVLAdminPanel.BLL.Services
{
    public class LoginService
    {
        public readonly string BaseUrl = URLs.API;
        public HttpClient client;

        public Token Login(Login login)
        {
            var response = GetApiLogin(login.UserName, login.Password);
            
            return (!string.IsNullOrEmpty(response.access_token) && ! Roles.RestictedAppUser.Contains(response.user_type.ToLower())) ?
            response : null;
        }

        [HttpGet]
        public Token GetApiLogin(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", email),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    });
                var result = client.PostAsync(BaseUrl + "account/token", content).Result;
                var stringAsync = result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Token>(stringAsync);
                return data;
            }
        }

    }
}
