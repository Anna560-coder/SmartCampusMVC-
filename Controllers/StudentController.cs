using Microsoft.AspNetCore.Mvc;

namespace SmartCampusMVC.Controllers
{
    public class StudentController : Controller
    {

        public IActionResult RequestService()
        {
            return View("~/Views/Student/RequestService.cshtml");
        }

        public IActionResult ReportIssue()
        {
            return View("~/Views/Student/ReportIssue.cshtml");
        }

        public IActionResult BookConsultation()
        {
            return View("~/Views/Student/BookConsultation.cshtml");
        }

        public IActionResult ViewConsultationHistory()
        {
            return View("~/Views/Student/ViewConsultationHistory.cshtml");
        }

        public IActionResult ManageAccount()
        {
            return View("~/Views/Student/ManageAccount.cshtml");
        }
    }
}