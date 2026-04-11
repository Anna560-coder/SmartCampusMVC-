using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusMVC.Data;
using SmartCampusMVC.Models;

namespace SmartCampusMVC.Controllers
{
    public class LectureController : Controller
    {
        private readonly AppDbContext _context;

        public LectureController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewConsultations()
        {
            return View();
        }

        public IActionResult ApproveRequests()
        {
            return View();
        }
        
        
        // ✅ LOAD PROFILE PAGE
        [HttpGet]
        public async Task<IActionResult> ManageProfile()
        {
            // Get logged-in user's email
            var email = User.Identity.Name;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLecturerProfile(Users model, IFormFile ProfileImage)
        {
            var user = await _context.Users.FindAsync(model.Id);

            if (user == null)
                return NotFound();

            // ✅ Allowed updates
            user.FullName = model.FullName;

            // ❌ DO NOT update Faculty
            // user.Faculty = model.Faculty;

            // ✅ Image Upload
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImagePath = fileName;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("ManageProfile");
        }

        public async Task<IActionResult> ViewConsultations()
        {
            var consultations = await _context.Consultations
                .OrderByDescending(c => c.ConsultationDate)
                .ToListAsync();

            return View(consultations);
        }

        //Approve
        public async Task<IActionResult> ApproveConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
                return NotFound();

            consultation.Status = "Approved";

            await _context.SaveChangesAsync();

            return RedirectToAction("ViewConsultations");
        }

        //Reject
        public async Task<IActionResult> RejectConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
                return NotFound();

            return View(consultation);
        }

        //Post Reject with Reason
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectConsultation(int id, string rejectionReason)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
                return NotFound();

            consultation.Status = "Rejected";
            consultation.RejectionReason = rejectionReason;

            await _context.SaveChangesAsync();

            return RedirectToAction("ViewConsultations");
        }
    }
}