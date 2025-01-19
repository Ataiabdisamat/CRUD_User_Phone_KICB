using Microsoft.EntityFrameworkCore;
using PhoneUserKICB.DAL.Entities;

namespace PhoneUserKICB.DAL.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
