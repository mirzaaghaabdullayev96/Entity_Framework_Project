using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Core.Models
{
    public class LoanItem : BaseEntity
    {
        public Book Book { get; set; }
        public int BookId { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}
