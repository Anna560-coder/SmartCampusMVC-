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

        // GET METHOD
        public IActionResult BookConsultation()
        {
            return View("~/Views/Student/BookConsultation.cshtml");
        }

        // POST METHOD (NEW)
        [HttpPost]
        public IActionResult BookConsultation(
            string Lecturer,
            DateTime? ConsultationDate,
            TimeSpan? ConsultationTime,
            string Reason)
        {
            if (string.IsNullOrEmpty(Lecturer) ||
                ConsultationDate == null ||
                ConsultationTime == null ||
                string.IsNullOrEmpty(Reason))
            {
                ViewBag.ErrorMessage = "Please complete all required fields.";
                return View("~/Views/Student/BookConsultation.cshtml");
            }

            ViewBag.SuccessMessage = "Consultation booked successfully.";

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