using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CallCenterPanel.Helper
{
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Hosting;
    using RestSharp;

    /// <summary>
    /// The send sms.
    /// </summary>
    public class SendSms
    {
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        [JsonProperty(PropertyName = "Phone List")]
        public List<string> PhoneList { get; set; }

        /// <summary>
        /// The send.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Send()
        {
            var returnText = false;
            try
            {
                var saltPhone = Common.RemoveCountryCode(this.PhoneNumber);
                var client = new RestClient("https://zmwqw.api.infobip.com/sms/2/text/single");
                var validPhone = "88" + saltPhone;
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Basic bWF0Y2h3aGVlbDoqI01XTGluZm9iaXAyMTIxKiM=");
                request.AddParameter(
                    "application/json",
                    "{\"from\":\"+8804445655551\", \"to\":\"" + validPhone + "\",\"text\":\"" + this.Message + "\"}",
                    ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                Logger.SmsLog(response.Content, saltPhone, this.Message);
                returnText = response.IsSuccessful;
            }
            catch (Exception ex)
            {
                Logger.SendErrorToText(ex);
            }

            return returnText;
        }

        public bool SendBulk()
        {
            var returnText = false;
            try
            {
                var client = new RestClient("https://zmwqw.api.infobip.com/sms/2/text/single");
                //var validPhone = "88" + this.PhoneNumber;
                var message = Message.Replace("\r\n", "\n");
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Basic bWF0Y2h3aGVlbDoqI01XTGluZm9iaXAyMTIxKiM=");
                request.AddParameter(
                    "application/json",
                    "{\"from\":\"+8804445655551\"," +
                    " \"to\":[" + string.Join(",", PhoneList) + "]" +
                    ",\"text\":\"" + message.Replace(Environment.NewLine,"\n") + "\"}",
                    ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                Logger.SmsLog(response.Content, this.PhoneNumber, this.Message);
                returnText = response.IsSuccessful;
            }
            catch (Exception ex)
            {
                Logger.SendErrorToText(ex);
            }

            return returnText;
        }

        public bool SendBulkAdvanced()
        {
            var returnText = false;
            try
            {
                var validPhone = "88" + this.PhoneNumber;
                var client = new RestClient("https://zmwqw.api.infobip.com/sms/2/text/advanced");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("accept", "application/json");
                request.AddHeader("authorization", "Basic bWF0Y2h3aGVlbDoqI01XTGluZm9iaXAyMTIxKiM=");
                request.AddParameter("application/json", "{" +
                                                         "\r\n\t\"bulkId\":\"BULK-ID-123\"," +
                                                         "\r\n\t\"messages\":[\r\n\t\t{\r\n\t\t\t\"from\":\"+8804445655551\"," +
                                                         "\r\n\t\t\t\"destinations\":[\r\n\t\t\t\t{\r\n\t\t\t\t\t\"to\":\"" + validPhone + "\"," +
                                                         "\r\n\t\t\t\t\t\"messageId\":\"MESSAGE-ID-123-xyz\"\r\n\t\t\t\t}," +
                                                         "\r\n\t\t\t\"text\":\"" + Message + "\"," + ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Logger.SmsLog(response.Content, this.PhoneNumber, this.Message);
                returnText = response.IsSuccessful;
            }
            catch (Exception ex)
            {
                Logger.SendErrorToText(ex);
            }

            return returnText;
        }
    }
}