using Hms.Models;
using HMS.Data.ModelConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Data
{
    public class HmsContext : IdentityDbContext<User>
    {
        public HmsContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Table> Table { get; set; }
        //Todo : Change spelling of DishCategroy
        public DbSet<DishCategroy> DishCategroy { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        //ToDo : change name to invoice record since irt represents single entity
        public DbSet<InvoiceRecords> InvoiceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DishConfiguration());
            builder.ApplyConfiguration(new FeedBackConfiguration());
            builder.ApplyConfiguration(new InvoiceRecordConfiguration());
            builder.ApplyConfiguration(new InvoiceConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
