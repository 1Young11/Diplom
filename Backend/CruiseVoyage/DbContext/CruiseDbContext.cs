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
            modelBuilder.Entity<Cruises>().HasKey(x => x.idCruises);
            modelBuilder.Entity<Photo>().HasKey(x => x.idPhoto);
        }

        public virtual DbSet<Customer> Customer { get; set; } = default!;
        public virtual DbSet<Role> Role { get; set; } = default!;
        public virtual DbSet<Cruises> Cruises { get; set; } = default!;
        public virtual DbSet<Photo> Photo { get; set; } = default!;
    }
}