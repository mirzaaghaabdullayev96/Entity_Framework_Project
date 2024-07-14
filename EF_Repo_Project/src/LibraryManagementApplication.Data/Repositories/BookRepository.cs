using LibraryManagementApplication.Core.Models;
using LibraryManagementApplication.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository {}
}
