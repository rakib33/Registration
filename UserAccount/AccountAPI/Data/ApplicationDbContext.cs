using AccountAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure unique constraint for ICNumber
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.ICNumber)
                .IsUnique();
        }
    }
}
