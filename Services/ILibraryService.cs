using Sashiel_ST10028058_PROG7311_ICETask3.Models;

namespace Sashiel_ST10028058_PROG7311_ICETask3.Services
{
    public interface ILibraryService
    {
        List<Book> Books { get; set; }
        List<User> Users { get; set; }
        List<BorrowRecord> BorrowRecords { get; set; }

        bool BorrowBook(int userId, int bookId);
        bool ReturnBook(int bookId);
        List<Book> SearchBooks(string query);
    }
}
