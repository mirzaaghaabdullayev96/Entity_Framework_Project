using LibraryManagementApplication.Core.Models;
using LibraryManagementApplication.Core.Repositories;
using LibraryManagementApplication.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table => _context.Set<T>();
        public AppDbContext _context;
        public GenericRepository()
        {
            _context = new AppDbContext();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public T GetById(int id)
        {
            return Table.Find(id);
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression=null)
        {
            var query = Table.AsNoTracking().AsQueryable();
            return expression is not null ? query.Where(expression) : query;
        }

        
        public void Insert(T entity)
        {
            Table.Add(entity);
        }

       
    }
}
