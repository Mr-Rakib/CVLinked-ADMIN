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
    public class RequestedSkillService
    {
        private readonly string BaseUrl = URLs.API + "api/requestedskill/";
        public HttpClient client;

        public List<RequestedSkill> FindAlllUnapprovedSkill(string token)
        {
            List<RequestedSkill> RequestedSkills = new List<RequestedSkill>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}unapproved").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    RequestedSkills = JsonConvert.DeserializeObject<List<RequestedSkill>>(result);
                }
            }
            return RequestedSkills;
        }

        public List<RequestedSkill> FindAlllApprovedSkill(string token)
        {
            List<RequestedSkill> RequestedSkills = new List<RequestedSkill>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}approved").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    RequestedSkills = JsonConvert.DeserializeObject<List<RequestedSkill>>(result);
                }
            }
            return RequestedSkills;
        }

        public RequestedSkill FindUnapprovedSkillById(string token, int id)
        {
            RequestedSkill requestedSkill = new RequestedSkill();
            requestedSkill = FindAlllUnapprovedSkill(token).Find(skill => skill.requestedSkillId == id);
            return requestedSkill;
        }

        internal string ApproveSkillById(string token, int id)
        {
            throw new NotImplementedException();
        }

        public RequestedSkill FindApprovedSkillById(string token, int id)
        {
            RequestedSkill requestedSkill = new RequestedSkill();
            requestedSkill = FindAlllApprovedSkill(token).Find(skill => skill.requestedSkillId == id);
            return requestedSkill;
        }

        public string SaveRequestedSkill(string token, RequestedSkill RequestedSkill)
        {
            using (client = new HttpClient())
            {
                var json = new JavaScriptSerializer().Serialize(RequestedSkill);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.PostAsync($"{BaseUrl}new", stringContent).Result;

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

        public string ApproveRequestedSkillsByRequestedSkill(string token, RequestedSkill RequestedSkill)
        {
            using (client = new HttpClient())
            {
                var json = new JavaScriptSerializer().Serialize(RequestedSkill);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.PostAsync($"{BaseUrl}approve", stringContent).Result;

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

        public string DisApproveRequestedSkillsByRequestedSkillId(string token, int id)
        {
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.PostAsync($"{BaseUrl}remove/{id}", null).Result;

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

    }
}
