using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TEPL.QMS.BLL.Component;
using TEPL.QMS.Common;
using TEPL.QMS.Common.Constants;
using TEPL.QMS.Common.Models;
using TEPLQMS.Controllers;
using System.Configuration;

namespace TEPLQMS.Areas.Admin.Controllers
{
    
    public class EmailtestController : Controller
    {
        //[CustomAuthorize(Roles = "QADM")]
        public ActionResult Index()
        {
            try
            {

            }
            catch (Exception ex)
            {
                //return Json(new { success = false, message = "error" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        public JsonResult SendEmail(string fromAddress, string toAddress, string message)
        {
            if (string.IsNullOrEmpty(fromAddress) || string.IsNullOrEmpty(toAddress) || string.IsNullOrEmpty(message))
            {
                return Json("Invalid input data.", JsonRequestBehavior.AllowGet);
            }
            try
            {
                string smtpServer = ConfigurationManager.AppSettings["SMTPHOST"];
                int port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPORT"]);
                var Email = toAddress;
                var ApprovalName = "Manas";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(Email);
                    mail.Subject = ConfigurationManager.AppSettings["Subject"];
                    mail.IsBodyHtml = true;
                    mail.Body = "Dear " + ApprovalName + ",<br /><br />" + message + ".  <br /><br /><br /><br /><br /> Thanks & Regards,<br /> Kaizen Team ";
                    using (SmtpClient smtp = new SmtpClient(smtpServer, port))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUserName"], ConfigurationManager.AppSettings["EmailPassword"]);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return Json("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return Json("Error sending email: " + ex.Message);
            }
        }
    }
}