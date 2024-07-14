using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Core.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? PublishedYear { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }
        public bool IsAvailable { get; set; }
        public Borrower? Borrower { get; set; }
        public int? BorrowerId { get; set; }
        public int? BorrowedTimes { get; set; }

    }
}
