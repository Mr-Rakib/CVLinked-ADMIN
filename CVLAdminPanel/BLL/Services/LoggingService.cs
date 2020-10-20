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
    public class LoggingService
    {
        private readonly string BaseUrl = URLs.API + "api/";
        public HttpClient client;

        public List<RequestLog> FindAllRequestByDate(string token, DateTime Date)
        {
            List<RequestLog> RequestLogList = new List<RequestLog>();
            if (!String.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    var dateOnly = DateTime.Parse(Date.ToString()).ToString("MM-dd-yyyy");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.GetAsync($"{BaseUrl}log/request/{dateOnly}").Result;

                    var Authorization = response.Headers.WwwAuthenticate;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        RequestLogList = JsonConvert.DeserializeObject<List<RequestLog>>(result);
                    }
                }
                return RequestLogList;
            }
            else return null;
        }

        public List<Error> FindAllErrorByDate(string token, DateTime formDate, DateTime toDate)
        {
            List<Error> ErrorLogList = new List<Error>();
            if (!String.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    var fromdateOnly = formDate.ToString("MM-dd-yyyy");
                    var todateOnly = toDate.ToString("MM-dd-yyyy");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.GetAsync($"{BaseUrl}error/errorlogs/{fromdateOnly}/{todateOnly}").Result;

                    var Authorization = response.Headers.WwwAuthenticate;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        ErrorLogList = JsonConvert.DeserializeObject<List<Error>>(result);
                    }
                }
                return ErrorLogList;
            }
            else return null;
        }

        public Error FindErrorById(string token, string Id)
        {
            Error Error = new Error();
            if (!String.IsNullOrEmpty(token))
            {
                using (client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = client.GetAsync($"{BaseUrl}error/errorlog/{Id}").Result;

                    var Authorization = response.Headers.WwwAuthenticate;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Error = JsonConvert.DeserializeObject<Error>(result);
                    }
                }
                return Error;
            }
            else return null;
        }



    }
}
