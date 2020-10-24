using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YarnsAndMobile.Models;

namespace YarnsAndMobile.Data
{
    public class YamDbContext : IdentityDbContext
    {
        public YamDbContext(DbContextOptions<YamDbContext> options) : base(options)
        {            
        }

        public DbSet<Book> Books { get; set; }
    }
}
