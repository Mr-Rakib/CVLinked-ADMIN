using CVLAdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.BLL.Services
{
    public static class ErrorService
    {
        public static void SendErrorToText(Logs Logs)
        {
            //string filepath = Path.Combine(Environment.CurrentDirectory,"LOGS","Smart Gowala Logs");
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOGS");
            try
            {
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = Path.Combine(filepath, DateTime.Today.ToString("dd-MM-yy") + ".txt");   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using var sw = File.AppendText(filepath);
                SetTheMessage(sw, Logs);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        private static void SetTheMessage(StreamWriter sw, Logs Logs)
        {
            string line = Environment.NewLine;
            string space = "  ";
            string error =
                "Log Written Date:" + space + Logs.date + line + line +
                "File Name       :" + space + Logs.fileName + line +
                "Methods Name    :" + space + Logs.methodsName + line +
                "Error Line No   :" + space + Logs.errorLineNo + line +
                "Coulumn Number  :" + space + Logs.columnNumber + line +
                "Error Message   :" + space + Logs.errorMessage + line +
                "Exception Type  :" + space + Logs.exceptiontype + line +
                "Descriptions    :" + space + Logs.errorMessageDescriptions + line;

            sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now + "-----------------");
            sw.WriteLine("-------------------------------------------------------------------------------------");
            sw.WriteLine(Environment.NewLine);
            sw.WriteLine(error);
            sw.WriteLine("--------------------------------*End*------------------------------------------");
            sw.WriteLine(Environment.NewLine);
        }
    }

}
