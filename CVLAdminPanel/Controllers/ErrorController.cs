using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CVLAdminPanel.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHadler(int statusCode)
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                ViewBag.ErrorMessage = statusCode;
                return View("NotFound");
            }
            else
                return RedirectToAction("Login", "Login");
        }

        [Route("/Error")]
        [AllowAnonymous]
        public IActionResult Error505()
        {
            var ExceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            Exception ex = ExceptionDetails.Error;
            var st = new StackTrace(ex, true);
            var frame = st.GetFrame(st.FrameCount - 1);
            Logs Logs = new Logs
            {
                date = DateTime.Now,
                fileName = ExceptionDetails.Path,
                methodsName = frame.GetMethod().Name,
                errorLineNo = frame.GetFileLineNumber(),
                columnNumber = frame.GetFileColumnNumber(),
                errorMessage = ex.GetType().Name,
                exceptiontype = ex.GetType().ToString(),
                errorMessageDescriptions = ex.StackTrace
            };

            ErrorService.SendErrorToText(Logs);
            return View("Error505");
        }
    }
}
