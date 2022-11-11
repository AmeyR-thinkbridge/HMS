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
    public class InvoiceRecordConfiguration : IEntityTypeConfiguration<InvoiceRecords>
    {
        public void Configure(EntityTypeBuilder<InvoiceRecords> builder)
        {
            builder
               .HasOne(s => s.Dish)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
