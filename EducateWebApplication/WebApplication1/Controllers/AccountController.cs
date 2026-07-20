using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Username and Password are required.";
                //ViewBag.Error = "Username and Password are required.";
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Password", password);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                //ViewBag.Error = "Invalid username or password.";
                return View();
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string email, string moduleName, DateTime toValidity)
        {
            if(!ModelState.IsValid || toValidity < new DateTime(1753, 1, 1))
            {
                TempData["ErrorMessage"] = "Please provide a valid date.";
                //ViewBag.Error = "Please provide a valid date.";
                return View();
            }

            var existingUser = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "UserName already exits";
                //ViewBag.Error = "Username already exists.";
                return View();
            }

            var user = new Users
            {
                Username = username,
                Password = password, // see note below about hashing
                Email = email,
                ModuleName = moduleName,
                ToDate = toValidity,
                CreatedOn = DateTime.Now
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            TempData["SuucessMessage"] = "Registration successful!";
            return RedirectToAction("Login");
        }

        //Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
