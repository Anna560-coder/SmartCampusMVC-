using Microsoft.AspNetCore.Mvc;

namespace SmartCampusMVC.Controllers
{
    public class TechnicianController : Controller
    {
        public IActionResult TechnicianDashboard()
        {
            return View();
        }

        public IActionResult ViewReportedIssues()
        {
            return View();
        }

        public IActionResult UpdateIssueStatus()
        {
            return View();
        }

        public IActionResult ManageProfile()
        {
            return View();
        }
    }
}