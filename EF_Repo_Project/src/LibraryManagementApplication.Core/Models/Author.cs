using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Core.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }
    }
}
