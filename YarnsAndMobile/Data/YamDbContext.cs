using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YarnsAndMobile.Models;

namespace YarnsAndMobile.Data
{
    public class YamDbContext : IdentityDbContext<Member>
    {
        public YamDbContext(DbContextOptions<YamDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PhoneType>().HasData(
                    new PhoneType { Id = 1, Description = "Mobile"},
                    new PhoneType { Id = 2, Description = "Home"},
                    new PhoneType { Id = 3, Description = "Work"},
                    new PhoneType { Id = 4, Description = "School"},
                    new PhoneType { Id = 5, Description = "Emergency Contact"}
                );
            base.OnModelCreating(builder);
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Sale> Sales { get; set; }

    }
}
