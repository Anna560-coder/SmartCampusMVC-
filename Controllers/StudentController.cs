using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusMVC.Data;
using SmartCampusMVC.Models;
using System.Security.Claims;
using System.Security.Claims;

namespace SmartCampusMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult RequestService()
        {
            return View("~/Views/Student/RequestService.cshtml");
        }

        public IActionResult ReportIssue()
        {
            return View("~/Views/Student/ReportIssue.cshtml");
        }

        // ✅ GET: Book Consultation Page
        public IActionResult BookConsultation()
        {
            ViewBag.ErrorMessage = null; // ✅ CLEAR ERROR
            ViewBag.SuccessMessage = null;

            return View("~/Views/Student/BookConsultation.cshtml");
        }

        // ✅ POST: Save Consultation to DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookConsultation(string Lecturer, DateTime ConsultationDate, TimeSpan ConsultationTime, string Reason)
        {
            if (ConsultationTime == default)
            {
                //TempData["ErrorMessage"] = "Time is required!";
                return RedirectToAction("BookConsultation");
            }

            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);

            var consultation = new Consultation
            {
                StudentId = userId,
                Lecturer = Lecturer,
                ConsultationDate = ConsultationDate,
                ConsultationTime = ConsultationTime,
                Reason = Reason,
                Status = "Pending"
            };

            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Consultation booked successfully!";

            // 👇 IMPORTANT: redirect to SAME page first
            return RedirectToAction("BookConsultation");
        }

        // ✅ VIEW HISTORY
        public async Task<IActionResult> ViewConsultationHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var consultations = await _context.Consultations
                .Where(c => c.StudentId == userId)
                .ToListAsync();

            return View(consultations);
        }

        public IActionResult ManageAccount()
        {
            return View("~/Views/Student/ManageAccount.cshtml");
        }

        public async Task<IActionResult> EditConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
            {
                return NotFound();
            }

            if (consultation.Status != "Pending")
                return RedirectToAction("ViewConsultationHistory");

            return View(consultation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConsultation(Consultation model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Model is invalid!";

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(model);
            }

            var consultation = await _context.Consultations.FindAsync(model.Id);

            if (consultation == null)
            {
                return NotFound();
            }

            consultation.Lecturer = model.Lecturer;
            consultation.ConsultationDate = model.ConsultationDate;
            consultation.ConsultationTime = model.ConsultationTime;
            consultation.Reason = model.Reason;

            await _context.SaveChangesAsync();

            return RedirectToAction("ViewConsultationHistory");
        }

        public async Task<IActionResult> DeleteConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
            {
                return NotFound();
            }

            if (consultation.Status != "Pending")
                return RedirectToAction("ViewConsultationHistory");


            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewConsultationHistory");
        }
    }
}