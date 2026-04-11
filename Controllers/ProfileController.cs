using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartCampusMVC.Models;
using SmartCampusMVC.ViewModels;

namespace SmartCampusMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public ProfileController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new UserProfileViewModel
            {
                FullName = user.FullName,
                StudentNumber = user.StudentNumber,
                Faculty = user.Faculty,
                Email = user.Email,
                ExistingImagePath = user.ProfileImagePath
            };

            return View("~/Views/Profile/Index.cshtml", model);
        }

        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new UserProfileViewModel
            {
                FullName = user.FullName,
                StudentNumber = user.StudentNumber,
                Faculty = user.Faculty,
                Email = user.Email,
                ExistingImagePath = user.ProfileImagePath
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            
            if (model.ProfileImage != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;

                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImagePath = "/images/profiles/" + fileName;
            }

            
            user.FullName = model.FullName;
            user.StudentNumber = model.StudentNumber;
            user.Faculty = model.Faculty;
            user.Email = model.Email;
            user.UserName = model.Email;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var user = await _userManager.GetUserAsync(User);

            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}