using Hms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.ModelConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder
                .HasOne(s => s.Table)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(s => s.User)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(s => s.InvoiceRecords)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
