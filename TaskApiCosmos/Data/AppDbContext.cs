using Microsoft.EntityFrameworkCore;
using TaskApiCosmos.Models;

namespace TaskApiCosmos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToContainer("Users");

        }

        public DbSet<User>? Users { get; set; }
    }
}
