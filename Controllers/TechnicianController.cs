using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusMVC.Data;
using SmartCampusMVC.Models;

namespace SmartCampusMVC.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly AppDbContext _context;

        public TechnicianController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult TechnicianDashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewReportedIssues()
        {
            var issues = await _context.Issues
                .OrderByDescending(i => i.DateCreated)
                .ToListAsync();

            return View(issues);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateIssueStatus(int id)
        {
            var issue = await _context.Issues.FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateIssueStatus(Issues issue)
        {
            var issueInDb = await _context.Issues.FindAsync(issue.Id);

            if (issueInDb == null)
            {
                return NotFound();
            }

            issueInDb.Status = issue.Status;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Issue status updated successfully.";

            return RedirectToAction("ViewReportedIssues");
        }

        public IActionResult ManageProfile()
        {
            return View();
        }
    }
}