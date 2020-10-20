using CVLAdminPanel.DAL.Repositories;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Nancy.Json;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CVLAdminPanel.BLL.Services
{
    public class BackOfficeUserService
    {
        private readonly string BaseUrl = URLs.API + "api/";
        public HttpClient client;
        private PersonalInformationRepository personalInformationRepository = new PersonalInformationRepository();

        public List<BackOfficeUser> FindAll(string token)
        {
            List<BackOfficeUser> backOfficeUserList = new List<BackOfficeUser>();
            List<LoginInfo> loginInfoList = new List<LoginInfo>();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}/usermanagement/user/all").Result;
                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    backOfficeUserList = JsonConvert.DeserializeObject<List<BackOfficeUser>>(result);
                    List<PersonalInformation> personalInformationList = personalInformationRepository.FindAll();

                    foreach (var user in backOfficeUserList)
                    {
                        user.personalInformation = personalInformationList.Find(info => info.loginId == user.loginId);
                    }
                }
            }
            return backOfficeUserList;
        }

        public BackOfficeUser FindById(string token, int id)
        {
            BackOfficeUser BackOfficeUser = new BackOfficeUser();
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.GetAsync($"{BaseUrl}/usermanagement/user/{id}").Result;

                var Authorization = response.Headers.WwwAuthenticate;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    BackOfficeUser = JsonConvert.DeserializeObject<BackOfficeUser>(result);
                    BackOfficeUser.personalInformation = personalInformationRepository.FindById(id);
                }
            }
            return BackOfficeUser;
        }

        public string Save(string token, BackOfficeUser backOfficeUser)
        {
            using (client = new HttpClient())
            {
                var json = new JavaScriptSerializer().Serialize(backOfficeUser);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.PostAsync($"{BaseUrl}/account/addbackofficeuser", stringContent).Result;
                var Authorization = response.Headers.WwwAuthenticate;
                var result = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var login = JsonConvert.DeserializeObject<LoginInfo>(result);
                    backOfficeUser.personalInformation.loginId = login.loginId;
                    return personalInformationRepository.Save(backOfficeUser.personalInformation) ? null : Messages.Issue;
                }
                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
                    return errorResponse.detail;
                }
            }
        }

        public string Update(string token, BackOfficeUser backOfficeUser)
        {
            if (ChangeRoles(token, backOfficeUser))
            {
                backOfficeUser.personalInformation.loginId = backOfficeUser.loginId;
                if (personalInformationRepository.FindById(backOfficeUser.personalInformation.loginId) != null)
                {
                    return personalInformationRepository.Update(backOfficeUser.personalInformation) ? null : Messages.Issue;
                }
                else
                {
                    return personalInformationRepository.Save(backOfficeUser.personalInformation) ? null : Messages.Issue;
                }
            }
            else return Messages.InvalidRole;
        }

        public string Delete(string token, int loginId)
        {
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = client.DeleteAsync($"{BaseUrl}/{loginId}").Result;
                var Authorization = response.Headers.WwwAuthenticate;
                var result = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return personalInformationRepository.Delete(loginId) ? Messages.Deleted : Messages.Issue;
                }
                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
                    return errorResponse.detail;
                }
            }
        }

        public bool SetActiveStatus(string token, int loginId, bool isActive)
        {
            using (client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = client.PostAsync($"{BaseUrl}usermanagement/user/{loginId}/status/change/{isActive}", null).Result;
                var Authorization = response.Headers.WwwAuthenticate;

                return response.IsSuccessStatusCode ? true : false;
            }
        }

        //--------------------------------------------Private Methods-------------------------------------------------//
        private bool ChangeRoles(string token, BackOfficeUser backOfficeUser)
        {
            BackOfficeUser user = FindById(token, backOfficeUser.loginId);
            if (user != null)
            {
                if (user.role.roleId != backOfficeUser.role.roleId)
                {
                    using (client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var response = client.PutAsync($"{BaseUrl}/usermanagement/user/{backOfficeUser.loginId}/role/change/{backOfficeUser.role.roleId}", null).Result;

                        return (response.IsSuccessStatusCode) ? true : false;
                    }
                }
                return true;
            }
            else return false;
        }

    }
}
