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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(x=>x.AuthorBooks).WithOne(x=>x.Author).OnDelete(DeleteBehavior.SetNull);
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");
        }
    }
}
