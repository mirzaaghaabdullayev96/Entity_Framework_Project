using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Core.Models
{
    public class LateReturnedBorrower:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
