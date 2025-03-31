using Microsoft.AspNetCore.Mvc;
using Sashiel_ST10028058_PROG7311_ICETask3.Services;
using Sashiel_ST10028058_PROG7311_ICETask3.Models;

namespace Sashiel_ST10028058_PROG7311_ICETask3.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LoginController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        // GET: Login page with user list
        [HttpGet]
        public IActionResult Index()
        {
            var users = _libraryService.Users;
            return View(users); // pass list of users to view
        }

        // POST: Perform login
        [HttpPost]
        public IActionResult Login(int userId)
        {
            var user = _libraryService.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());

                return RedirectToAction("Index", "Library");
            }

            TempData["Message"] = "Invalid user selection.";
            return RedirectToAction("Index");
        }

        // GET: Logout user
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
