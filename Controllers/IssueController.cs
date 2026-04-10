using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartCampusMVC.Data;
using SmartCampusMVC.Models;
using SmartCampusMVC.ViewModels;

namespace SmartCampusMVC.Controllers
{
    public class IssueController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IssueController(AppDbContext context, UserManager<Users> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IssueViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string? filePath = null;

            // ✅ HANDLE FILE UPLOAD
            if (model.File != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
                string fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                filePath = "/uploads/" + fileName;
            }

            var issue = new Issues
            {
                UserId = user.Id,
                IssueType = model.IssueType,
                CustomIssue = model.IssueType == "Other" ? model.CustomIssue : null,
                Description = model.Description,
                FilePath = filePath // ✅ SAVE PATH
            };

            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var issues = _context.Issues.ToList();
            return View(issues);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _context.Issues.FindAsync(id);

            if (issue != null)
            {
                _context.Issues.Remove(issue);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET
        public async Task<IActionResult> Edit(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return View(issue);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Edit(Issues model)
        {
            var existingIssue = await _context.Issues.FindAsync(model.Id);

            if (existingIssue == null)
            {
                return NotFound();
            }

            // ✅ Only update editable fields
            existingIssue.IssueType = model.IssueType;
            existingIssue.CustomIssue = model.CustomIssue;
            existingIssue.Description = model.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
