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
    public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
    {
        public void Configure(EntityTypeBuilder<Borrower> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(150);
            builder.Property(x => x.LateReturned).HasDefaultValueSql("0");
            builder.HasMany(x=>x.Books).WithOne(x=>x.Borrower).OnDelete(DeleteBehavior.SetNull);
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");
        }
    }
}
