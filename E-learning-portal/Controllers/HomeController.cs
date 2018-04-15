using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace E_learning_portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var customerName = "1";
            var customerEmail = "anastasia.yurovchik@gmail.com";
            var customerRequest = "Уважаемый," + " + student.Surname + student.Name + ";
            var errorMessage = "";
            var debuggingFlag = false;
            try
            {
                // Initialize WebMail helper
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.UserName = "E-learning portal";
                WebMail.Password = "010203Deadpool";
                WebMail.From = "deadpoollyo@gmail.com";
                WebMail.EnableSsl = true;

                // Send email
                WebMail.Send(to: customerEmail,
                    subject: "Один из ваших студентов выполнил задание",
                    body: customerRequest
                );
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}