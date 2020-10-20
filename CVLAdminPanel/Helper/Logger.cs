// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="">
//   
// </copyright>
// <summary>
//   The exception handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CallCenterPanel.Helper
{
    using Microsoft.AspNetCore.Hosting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;


    /// <summary>
    /// The exception handler.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// The errorline no.
        /// </summary>
        private static string errorlineNo;

        /// <summary>
        /// The errorline no.
        /// </summary>
        private static string errormsg;

        /// <summary>
        /// The errorline no.
        /// </summary>
        private static string extype;

        /// <summary>
        /// The errorline no.
        /// </summary>
        private static readonly string exurl;

        /// <summary>
        /// The errorline no.
        /// </summary>
        private static readonly string hostIp;

        /// <summary>
        /// The errorline no.
        /// </summary>
        private static string errorLocation;

        /// <summary>
        /// The send error to text.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public static void SendErrorToText(Exception ex)
        {
            errorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            errormsg = ex.GetType().Name;
            extype = ex.GetType().ToString();
            errorLocation = ex.Message;

            try
            {
                var filepath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory()), "Log/Error/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (var sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now + Environment.NewLine + "Error Line No :" + " " + errorlineNo + Environment.NewLine + "Error Message:" + " " + errormsg + Environment.NewLine + "Exception Type:" + " " + extype + Environment.NewLine + "Error Location :" + " " + errorLocation + Environment.NewLine + "Error Page Url:" + " " + exurl + Environment.NewLine + "User Host IP:" + " " + hostIp + Environment.NewLine;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now);
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(Environment.NewLine);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// The sms log.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="phone">
        /// The phone.
        /// </param>
        /// <param name="msgBody">
        /// </param>
        public static void SmsLog(string message, string phone, string msgBody)
        {
            try
            {
                string filepath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory()), "Log/SmsLog/"); //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (var sw = File.AppendText(filepath))
                {
                    string sms = "Log Written Date:" + " " + DateTime.Now.ToString("hh:mm:tt") + Environment.NewLine
                                   + "Message :" + " " + message + Environment.NewLine + "Message Body:" + " " + msgBody
                                   + Environment.NewLine;
                    sw.WriteLine(sms);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(Environment.NewLine);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                e.ToString();
            }
        }

        public static void PushNotificationLog(string message, string recipient, string title)
        {
            try
            {
                var filepath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory()), "Log/PushLog/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (var sw = File.AppendText(filepath))
                {
                    string push = "Log Written Date:" + " " + DateTime.Now.ToString("hh:mm:tt") + Environment.NewLine
                                   + "Title :" + " " + title + Environment.NewLine + "Message Body:" + " " + message + Environment.NewLine + "Recipient:" + " " + recipient
                                   + Environment.NewLine;
                    sw.WriteLine(push);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(Environment.NewLine);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                e.ToString();
            }
        }

    }
}
