using LibraryManagementApplication.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Core.Configurations
{
    public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");
            builder.HasOne(x=> x.Book).WithMany(x=>x.AuthorBooks).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x=> x.Author).WithMany(x=>x.AuthorBooks).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
