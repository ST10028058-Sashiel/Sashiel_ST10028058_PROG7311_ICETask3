using System;

namespace Sashiel_ST10028058_PROG7311_ICETask3.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate.HasValue;

        // Automatically calculate fine if overdue
        public double CalculateFine()
        {
            if (!ReturnDate.HasValue || ReturnDate.Value <= DueDate)
                return 0;

            int daysLate = (ReturnDate.Value - DueDate).Days;
            return daysLate * 2.50; // $2.50 per late day
        }

        // ✅ Property to expose fine amount
        public double FineAmount => IsReturned ? CalculateFine() : 0;
    }
}
