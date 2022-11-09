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
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder
               .HasOne(s => s.DishCategroy)
               .WithMany(s => s.DishList)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
