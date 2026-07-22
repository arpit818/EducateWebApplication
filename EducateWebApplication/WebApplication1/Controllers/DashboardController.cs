using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        public readonly ApplicationDbContext _db;
        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
                return RedirectToAction("Login");

            if (role == "Admin")
                return RedirectToAction("AdminDashboard");

            return RedirectToAction("StudentDashboard");
        }
        public async Task<IActionResult> StudentDashboard(int? moduleId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UsersID== userId);
            if (user == null) return RedirectToAction("Login");

            // Assumes the user's purchased course is looked up by their ModuleName/CourseName link.
            // Adjust this query once Users has a proper CourseId FK.
            var course = await _db.Courses
                .Include(c => c.Modules.OrderBy(m => m.SortOrder))
                .FirstOrDefaultAsync();

            if (course == null) return View("StudentDashboard", null);

            var selectedModuleId = moduleId ?? course.Modules.FirstOrDefault()?.ModuleId;

            var topics = new List<Topic>();
            var progressMap = new Dictionary<int, UserProgress>();

            if (selectedModuleId != null)
            {
                topics = await _db.Topics
                    .Where(t => t.ModuleId == selectedModuleId)
                    .OrderBy(t => t.SortOrder)
                    .ToListAsync();

                var topicIds = topics.Select(t => t.TopicId).ToList();
                var progressList = await _db.Userprogress
                    .Where(p => p.UserId == userId && topicIds.Contains(p.TopicId))
                    .ToListAsync();

                progressMap = progressList.ToDictionary(p => p.TopicId, p => p);
            }

            ViewBag.Course = course;
            ViewBag.SelectedModuleId = selectedModuleId;
            ViewBag.Topics = topics;
            ViewBag.Progress = progressMap;
            ViewBag.Username = user.Username;

            return View();
        }

        // Called when the student opens a PDF, so the Quiz unlocks afterwards
        public async Task<IActionResult> MarkPdfViewed(int topicId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            var progress = await _db.Userprogress
                .FirstOrDefaultAsync(p => p.UserId == userId && p.TopicId == topicId);

            if (progress == null)
            {
                progress = new UserProgress { UserId = userId.Value, TopicId = topicId };
                _db.Userprogress.Add(progress);
            }

            progress.PdfViewed = true;
            progress.UpdatedOn = DateTime.Now;
            await _db.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> AdminDashboard()
        {
            var role = HttpContext.Session.GetString("RoleType");
            if (role != "Admin")
            {
                return RedirectToAction("Login");
            }

            ViewBag.TotalUsers = await _db.Users.CountAsync();
            ViewBag.TotalModules = await _db.Users.Select(u => u.ModuleName).Distinct().CountAsync();
            ViewBag.ExpiringSoon = await _db.Users.Where(u => u.ToDate <= DateTime.Now.AddDays(7) && u.ToDate >= DateTime.Now).CountAsync();
            ViewBag.Expired = await _db.Users.Where(u => u.ToDate <= DateTime.Now).CountAsync();
            var AllUsers = await _db.Users.OrderByDescending(u => u.CreatedOn).ToListAsync();
            return View(AllUsers);

        }
    }
}
