using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Core.Models
{
    public class Borrower : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Book> Books { get; set; }
        public bool LateReturned { get; set; }

    }

}
