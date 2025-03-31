using Sashiel_ST10028058_PROG7311_ICETask3.Models;

namespace Sashiel_ST10028058_PROG7311_ICETask3.Services
{
    public class LibraryService : ILibraryService // 🔄 Implement interface here
    {
        public List<Book> Books { get; set; }
        public List<User> Users { get; set; }
        public List<BorrowRecord> BorrowRecords { get; set; }

        public LibraryService()
        {
            // Initialize mock data
            Books = new List<Book>
            {
                new Book { Id = 1, Title = "1984", Author = "George Orwell", IsAvailable = true },
                new Book { Id = 2, Title = "The Hobbit", Author = "J.R.R. Tolkien", IsAvailable = true },
                new Book { Id = 3, Title = "C# in Depth", Author = "Jon Skeet", IsAvailable = true }
            };

            // ✅ Fix: Use the UserRole enum instead of strings
            Users = new List<User>
            {
                new User { Id = 1, Name = "Alice", Role = UserRole.User },
                new User { Id = 2, Name = "Bob", Role = UserRole.Admin }
            };

            BorrowRecords = new List<BorrowRecord>();
        }

        public bool BorrowBook(int userId, int bookId)
        {
            var book = Books.FirstOrDefault(b => b.Id == bookId && b.IsAvailable);
            var user = Users.FirstOrDefault(u => u.Id == userId);

            if (book == null || user == null)
                return false;

            book.IsAvailable = false;

            BorrowRecords.Add(new BorrowRecord
            {
                Id = BorrowRecords.Count + 1,
                BookId = bookId,
                UserId = userId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            });

            return true;
        }

        public bool ReturnBook(int bookId)
        {
            var record = BorrowRecords.FirstOrDefault(r => r.BookId == bookId && !r.IsReturned);
            if (record == null) return false;

            record.ReturnDate = DateTime.Now;
            var book = Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
                book.IsAvailable = true;

            return true;
        }

        public List<Book> SearchBooks(string query)
        {
            return Books
                .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
