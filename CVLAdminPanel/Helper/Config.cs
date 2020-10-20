﻿using Infobip.Api.Config;
using Infobip.Api.Model.Sms.Mt.Reports;
using Infobip.Api.Model.Omni.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infobip.Api.Client;

namespace CallCenterPanel.Helper
{
    abstract class Config
    {
        protected static readonly string BASE_URL = "https://zmwqw.api.infobip.com/sms/2/text/advanced";
        protected static readonly string USERNAME = "MatchWheel";
        protected static readonly string PASSWORD = "*#MWLinfobip2121*#";

        protected static readonly BasicAuthConfiguration BASIC_AUTH_CONFIGURATION = new BasicAuthConfiguration(BASE_URL, USERNAME, PASSWORD);

        protected static readonly string FROM = "+8804445655551";
        protected static readonly string TO = "8801713753853";
        protected static readonly List<string> TO_LIST = new List<string>(1) { "PHONE" };
        protected static readonly string MESSAGE_TEXT = "This is an example message sent via C# example lib.";

        protected static readonly string NOTIFY_URL = "https://notify.me";

        public abstract Task RunExampleAsync();

        protected static async Task<SMSReportResponse> GetSmsReportAsync(string messageId)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Fetching report...");

            GetSentSmsDeliveryReports reportsClient = new GetSentSmsDeliveryReports(BASIC_AUTH_CONFIGURATION);
            GetSentSmsDeliveryReportsExecuteContext context = new GetSentSmsDeliveryReportsExecuteContext
            {
                MessageId = messageId
            };
            SMSReportResponse response = await reportsClient.ExecuteAsync(context);

            if (!response.Results.Any())
            {
                Console.WriteLine("No report to fetch.");
                return new SMSReportResponse();
            }
            Console.WriteLine("Fetching report complete.");

            PrintFirstReportDetails(response);

            return response;
        }

        protected static void PrintFirstReportDetails(SMSReportResponse response)
        {
            SMSReport report = response.Results[0];
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Message ID: " + report.MessageId);
            Console.WriteLine("Sent at: " + report.SentAt);
            Console.WriteLine("Receiver: " + report.To);
            Console.WriteLine("Status: " + report.Status.Name);
            Console.WriteLine("Price: " + report.Price.PricePerMessage + " " + report.Price.Currency);
            Console.WriteLine("-------------------------------");
        }

        protected static async Task<OMNIReportsResponse> GetOmniReportAsync(string bulkId)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Fetching reports...");

            GetOMNIReports omniReportsClient = new GetOMNIReports(BASIC_AUTH_CONFIGURATION);
            GetOMNIReportsExecuteContext context = new GetOMNIReportsExecuteContext
            {
                BulkId = bulkId
            };
            OMNIReportsResponse response = await omniReportsClient.ExecuteAsync(context);

            if (!response.Results.Any())
            {
                Console.WriteLine("No report to fetch.");
                return new OMNIReportsResponse();
            }
            Console.WriteLine("Fetching report complete.");

            foreach (OMNIReport report in response.Results)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Message ID: " + report.MessageId);
                Console.WriteLine("Sent at: " + report.SentAt);
                Console.WriteLine("Channel: " + report.Channel);
                Console.WriteLine("Status: " + report.Status.Name);
                Console.WriteLine("Price: " + report.Price.PricePerMessage + " " + report.Price.Currency);
                Console.WriteLine("-------------------------------");
            }

            return response;
        }
    }
}
