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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(700).HasDefaultValueSql("'This book has no description yet'");
            builder.HasMany(x => x.AuthorBooks).WithOne(x => x.Book).OnDelete(DeleteBehavior.SetNull);
            builder.Property(x => x.IsAvailable).HasDefaultValueSql("1");
            builder.Property(x => x.BorrowerId).IsRequired(false);
            builder.Property(x => x.BorrowedTimes).IsRequired(false).HasDefaultValueSql("0");
            builder.Property(x=>x.PublishedYear).IsRequired(false);
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");

        }
    }
}
