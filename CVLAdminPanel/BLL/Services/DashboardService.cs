using CVLAdminPanel.DAL.Repositories;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Schema;

namespace CVLAdminPanel.BLL.Services
{
    public class DashboardService
    {
        private readonly string BaseUrl = URLs.API + "api/backoffice";
        public HttpClient client;

        public DashboardData FindAllDataForAdminAndSupervisor(string token)
        {
            DashboardData dashboardData = new DashboardData();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}/rolewiseuser/total").Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dashboardData.DashboardInformations = JsonConvert.DeserializeObject<List<DashboardInfo>>(result);
                    dashboardData.DashboardTodaysInformations = FindAllTodaysData(token);
                }
            }
            return dashboardData;
        }

        internal string GetActionForUser(string role)
        {
            switch (role.ToLower())
            {
                case Roles.Admin:
                    return Roles.Admin;

                case Roles.Supervisor:
                    return Roles.Supervisor;

                case Roles.Salesman:
                    return Roles.Salesman;

                case Roles.CallCenterAgent:
                    return Roles.CallCenterAgent;

                case Roles.Developer:
                    return Roles.Developer;
                default:
                    return null;
            }
        }

        public List<DashboardInfo> FindAllTodaysData(string token)
        {
            List<DashboardInfo> dashboardInfos = new List<DashboardInfo>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var param = new Dictionary<string, string>
                {
                    { "date", DateTime.Now.ToString("yyyy-MM-dd")}
                };
                var response = client.GetAsync(QueryHelpers.AddQueryString($"{BaseUrl}/rolewiseuser", param)).Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    dashboardInfos = JsonConvert.DeserializeObject<List<DashboardInfo>>(result);
                }
            }
            return dashboardInfos;
        }

        public int TotalPackageSubscriber(string token)
        {
            int totalSubscriber = 0;
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.GetAsync($"{BaseUrl}/activesubscriber").Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    totalSubscriber = JsonConvert.DeserializeObject<int>(result);
                }
            }
            return totalSubscriber;
        }

    }
}
