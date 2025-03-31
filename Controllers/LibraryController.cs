using Microsoft.AspNetCore.Mvc;
using Sashiel_ST10028058_PROG7311_ICETask3.Services;
using Sashiel_ST10028058_PROG7311_ICETask3.Models;

namespace Sashiel_ST10028058_PROG7311_ICETask3.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        // 📚 Display all books
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Index", "Login");

            var books = _libraryService.Books;
            return View(books);
        }

        // 📖 Borrow a book
        public IActionResult Borrow(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Login");

            var success = _libraryService.BorrowBook(userId.Value, id);
            TempData["Message"] = success ? "Book borrowed!" : "Borrow failed.";
            return RedirectToAction("Index");
        }

        // 🔙 Return a book
        public IActionResult Return(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Index", "Login");

            var success = _libraryService.ReturnBook(id);
            TempData["Message"] = success ? "Book returned!" : "Return failed.";
            return RedirectToAction("Index");
        }

        // 🔎 Search for books
        public IActionResult Search(string query)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Index", "Login");

            var results = _libraryService.SearchBooks(query ?? "");
            return View("Index", results);
        }

        // 📄 View borrowed records & fines
        public IActionResult BorrowedBooks()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Login");

            var user = _libraryService.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return RedirectToAction("Index", "Login");

            var records = _libraryService.BorrowRecords;

            // ✅ Filter to current user if role is User
            if (user.Role == UserRole.User)
                records = records.Where(r => r.UserId == userId).ToList();

            return View(records);
        }
    }
}
