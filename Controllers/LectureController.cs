using Microsoft.AspNetCore.Mvc;

namespace SmartCampusMVC.Controllers
{
    public class LectureController : Controller
    {

        public IActionResult ViewConsultations()
        {
            return View();
        }

        public IActionResult ApproveRequests()
        {
            return View();
        }

        public IActionResult ManageProfile()
        {
            return View();
        }
    }
}