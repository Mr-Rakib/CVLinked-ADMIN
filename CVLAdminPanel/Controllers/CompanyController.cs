using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using CVLAdminPanel.BLL.Services;
using CVLAdminPanel.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CallCenterPanel.Helper;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using CVLAdminPanel.Utility;

namespace CVLAdminPanel.Controllers
{
    [Authorize(Roles = Roles.SalesmanAndAbove)]
    public class CompanyController : Controller
    {
        private static CompanyService companyService = new CompanyService();
        private readonly IEmailSender _emailSender;

        public CompanyController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        // GET: Company
        public ActionResult Index()
        {
            string token = User.FindFirst("access_token").Value;
            List<Company> companyList = companyService.FindAll(token);
            return View(companyList);
        }
        public ActionResult ListIndex()
        {
            string token = User.FindFirst("access_token").Value;
            List<Company> companyList = companyService.FindAll(token);
            return View(companyList);
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            string token = User.FindFirst("access_token").Value;
            Employer Employer = companyService.FindById(token, id);
            if (Employer != null)
                return View("_partialCompanyProfile", Employer);
            else
            {
                TempData["Warning"] = Messages.EmployersNotFound;
                return RedirectToAction("Index");
            }
        }

        public ActionResult BasicDetails(int id)
        {
            string token = User.FindFirst("access_token").Value;
            Company Company = companyService.FindCompanyBasicById(token, id);
            return View("BasicDetails", Company);
        }

        public ActionResult Delete(int id)
        {
            string token = User.FindFirst("access_token").Value;
            string message = companyService.DeleteCompnayById(token, id);
            TempData["Error"] = message;
            return RedirectToAction("Index");
        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult SendSms(IFormCollection collection)
        {
            try
            {
                string message = Convert.ToString(collection["smsContent"]);
                string phone = Convert.ToString(collection["receiver"]);
                message = Regex.Replace(message, @"\r\n?|\n", "\\n");
                var sendSms = new SendSms
                {
                    PhoneNumber = phone,
                    Message = message
                };

                var response = sendSms.Send();

                if (response)
                {
                    ViewData["Success"] = "SMS successfully sent to its recipient.";
                }
                else
                {
                    ViewData["Error"] = "Failed to send SMS. Please try again.";
                }
            }
            catch (Exception ex)
            {
                Logger.SendErrorToText(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult SendEmail(IFormCollection collection)
        {
            try
            {
                string content = Convert.ToString(collection["emailContent"]);
                string subject = Convert.ToString(collection["subject"]);
                string email = Convert.ToString(collection["ToEmail"]);
                var message = new Message(new string[] { email }, subject, content, null);
                _emailSender.SendEmail(message);
                ViewData["Success"] = "An Email has been triggered to " + email + ". recipient. Thank You!";
            }
            catch (Exception ex)
            {
                Logger.SendErrorToText(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        //-----------------------------------JSON AJAX API---------------------------------

        public JsonResult CompanyList()
        {
            string token = User.FindFirst("access_token").Value;
            List<Company> companyList = companyService.FindAll(token);
            return Json(companyList);
        }

        public ActionResult DetailsGrid(int id)
        {
            string token = User.FindFirst("access_token").Value;
            Employer Employer = companyService.FindById(token, id);
            if (Employer != null)
                return PartialView("Employer/Profile", Employer);
            else
                return PartialView("Employer/ItemsNotFound");
        }

        public ActionResult SendSmsView(int id)
        {
            string token = User.FindFirst("access_token").Value;
            Company Company = companyService.FindCompanyBasicById(token, id);
            return PartialView("_partialSendSms", Company);
        }

        public ActionResult SendEmailView(int id)
        {
            string token = User.FindFirst("access_token").Value;
            Company Company = companyService.FindCompanyBasicById(token, id);
            return PartialView("_partialCompanyEmail", Company);
        }

    }
}