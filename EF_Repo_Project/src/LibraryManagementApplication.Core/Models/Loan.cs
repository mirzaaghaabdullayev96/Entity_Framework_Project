using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Core.Models
{
    public class Loan : BaseEntity
    {
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime LoanTime { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime MustReturnDate { get; set; } = DateTime.UtcNow.AddDays(15);
        public DateTime? ReturnDate { get; set; } 
        public List<LoanItem> LoanItems { get; set; }

    }
}
