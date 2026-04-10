using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SmartCampusMVC.Models;

namespace SmartCampusMVC.Controllers
{
    public class LectureController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public LectureController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult ViewConsultations()
        {
            return View();
        }

        public IActionResult ApproveRequests()
        {
            return View();
        }
        
        // ✅ GET: Load Lecturer Profile
        public async Task<IActionResult> ManageProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }

        // ✅ POST: Update Lecturer Profile
        [HttpPost]
        public async Task<IActionResult> ManageProfile(Users model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                user.FullName = model.FullName;
                user.Faculty = model.Faculty;

                await _userManager.UpdateAsync(user);

                TempData["Success"] = "Profile updated successfully!";
            }

            return RedirectToAction("ManageProfile");
        }
    }
}