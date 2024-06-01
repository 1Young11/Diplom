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
            modelBuilder.Entity<Cruises>(e =>
            {
                e.HasKey(x => x.idCruises);
                e.HasMany<CruisePoint>(x => x.CruisePoints).WithOne().HasForeignKey(x=>x.idCruise);
                e.HasOne(x => x.Ship).WithMany().HasForeignKey("idShip");
            });
            modelBuilder.Entity<Photo>().HasKey(x => x.idPhoto);
            modelBuilder.Entity<CruisePoint>(e =>
            {
                e.HasKey(x => x.idCruisePoint);
            });
            modelBuilder.Entity<Ship>().HasKey(x => x.idShip);
        }

        public virtual DbSet<Customer> Customer { get; set; } = default!;
        public virtual DbSet<Role> Role { get; set; } = default!;
        public virtual DbSet<Cruises> Cruises { get; set; } = default!;
        public virtual DbSet<Photo> Photo { get; set; } = default!;
        public virtual DbSet<CruisePoint> CruisePoint { get; set; } = default!;
        public virtual DbSet<Ship> Ship { get; set; } = default!;
    }
}