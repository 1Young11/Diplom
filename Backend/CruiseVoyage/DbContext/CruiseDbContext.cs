using CruiseVoyage.Models;
using Microsoft.EntityFrameworkCore;

namespace CruiseVoyage.DbContext
{
    public class CruiseDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CruiseDbContext(DbContextOptions<CruiseDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(x => x.idCustomer);
            modelBuilder.Entity<Role>().HasKey(x => x.idRole);
        }

        public virtual DbSet<Customer> Customer { get; set; } = default!;
        public virtual DbSet<Role> Role { get; set; } = default!;
    }
}