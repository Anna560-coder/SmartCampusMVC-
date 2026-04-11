using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SmartCampusMVC.Models;
using SmartCampusMVC.Data;

namespace SmartCampusMVC.Controllers
{
    public class LectureController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _context;

        public LectureController(UserManager<Users> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // ✅ View all submitted consultation requests
        // ✅ View all submitted consultation requests
        public async Task<IActionResult> ViewConsultations()
        {
            var user = await _userManager.GetUserAsync(User);
            var faculty = user?.Faculty ?? "Unknown Faculty";

            var requests = new List<ConsultationRequest>
    {
        new ConsultationRequest { Id = 1, StudentName = "Lerato Mokoena", StudentNumber = "STU2024001", Faculty = faculty, Description = "Struggling with recursive tree traversal, in-order and post-order.", RequestedDate = new DateTime(2026, 4, 14), RequestedTime = "10:00 AM", Status = "Pending", CreatedAt = DateTime.Now },
        new ConsultationRequest { Id = 2, StudentName = "Sipho Dlamini", StudentNumber = "STU2024042", Faculty = faculty, Description = "Would like to discuss time complexity of merge sort vs quick sort.", RequestedDate = new DateTime(2026, 4, 15), RequestedTime = "02:00 PM", Status = "Pending", CreatedAt = DateTime.Now },
        new ConsultationRequest { Id = 3, StudentName = "Ayanda Nkosi", StudentNumber = "STU2023018", Faculty = faculty, Description = "Need clarification on abstract classes vs interfaces.", RequestedDate = new DateTime(2026, 4, 12), RequestedTime = "11:00 AM", Status = "Approved", CreatedAt = DateTime.Now },
        new ConsultationRequest { Id = 4, StudentName = "Thabo Khumalo", StudentNumber = "STU2023055", Faculty = faculty, Description = "Needs help with factory and observer patterns.", RequestedDate = new DateTime(2026, 4, 11), RequestedTime = "03:30 PM", Status = "Rejected", CreatedAt = DateTime.Now },
    };

            return View(requests);
        }

        // ✅ View only pending requests for approval/rejection
        public async Task<IActionResult> ApproveRequests()
        {
            var user = await _userManager.GetUserAsync(User);
            var faculty = user?.Faculty ?? "Unknown Faculty";

            var pending = new List<ConsultationRequest>
    {
        new ConsultationRequest { Id = 1, StudentName = "Lerato Mokoena", StudentNumber = "STU2024001", Faculty = faculty, Description = "Struggling with recursive tree traversal, in-order and post-order.", RequestedDate = new DateTime(2026, 4, 14), RequestedTime = "10:00 AM", Status = "Pending", CreatedAt = DateTime.Now },
        new ConsultationRequest { Id = 2, StudentName = "Sipho Dlamini", StudentNumber = "STU2024042", Faculty = faculty, Description = "Would like to discuss time complexity of merge sort vs quick sort.", RequestedDate = new DateTime(2026, 4, 15), RequestedTime = "02:00 PM", Status = "Pending", CreatedAt = DateTime.Now },
    };

            return View(pending);
        }

        // ✅ POST: Approve a request
        [HttpPost]
        public IActionResult Approve(int id, string? lecturerNote)
        {
            // 🔁 HARDCODED — replace body with real DB logic below:
            // var request = _context.ConsultationRequests.Find(id);
            // if (request == null) return NotFound();
            // request.Status = "Approved";
            // request.LecturerNote = lecturerNote;
            // request.UpdatedAt = DateTime.UtcNow;
            // _context.SaveChanges();

            TempData["Success"] = "Request approved successfully.";
            return RedirectToAction("ApproveRequests");
        }

        // ✅ POST: Reject a request
        [HttpPost]
        public IActionResult Reject(int id, string? rejectionReason)
        {
            // 🔁 HARDCODED — replace body with real DB logic below:
            // var request = _context.ConsultationRequests.Find(id);
            // if (request == null) return NotFound();
            // request.Status = "Rejected";
            // request.RejectionReason = rejectionReason;
            // request.UpdatedAt = DateTime.UtcNow;
            // _context.SaveChanges();

            TempData["Danger"] = "Request rejected.";
            return RedirectToAction("ApproveRequests");
        }

        // ✅ GET: Lecturer Profile
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