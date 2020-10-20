using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using CVLAdminPanel.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.Developer)]
    public class LogController : Controller
    {
        private static LoggingService LoggingService = new LoggingService();

        [Route("Log/RequestLogs/{dateTime:datetime?}")]
        public ActionResult RequestLogs(DateTime dateTime)
        {
            string token = User.FindFirst("access_token").Value;
            List<RequestLog> RequestLogList = LoggingService.FindAllRequestByDate(token, dateTime);
            ViewBag.RequestLogDate = dateTime;
            return View(RequestLogList);
        }

        [Route("Log/ErrorLogs/{fromDate:datetime?}/{toDate:datetime?}")]
        public ActionResult ErrorLogs(DateTime fromDate, DateTime toDate)
        {
            string token = User.FindFirst("access_token").Value;
            List<Error> ErrorLogList = LoggingService.FindAllErrorByDate(token, fromDate, toDate);
            ViewBag.ErrorFromDate = fromDate.Date;
            ViewBag.ErrorToDate = toDate.Date;
            return View(ErrorLogList);
        }

        public ActionResult ErrorLog(string errorId)
        {
            string token = User.FindFirst("access_token").Value;
            Error Error = LoggingService.FindErrorById(token, errorId);
            return PartialView("ErrorLog", Error);
        }
    }
}
