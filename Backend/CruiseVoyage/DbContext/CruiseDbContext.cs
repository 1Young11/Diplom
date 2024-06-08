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
            modelBuilder.Entity<Ship>(x =>
            {
                x.HasKey(x => x.idShip);
                x.HasMany<Cabin>(c => c.Cabins).WithOne().HasForeignKey(c => c.idShip);
                x.HasMany<CabinType>(c => c.CabinTypes).WithOne().HasForeignKey(c => c.idShip);
            });
            modelBuilder.Entity<Cabin>(x =>
            {
                x.HasKey(x => x.idCabin);
                x.HasOne<CabinType>(c => c.CabinType).WithMany().HasForeignKey(c => c.idCabintype);
                x.HasMany<CabinBed>(c => c.CabinBeds).WithOne().HasForeignKey(c => c.idCabin);
            });
            modelBuilder.Entity<CabinBed>().HasKey(x => x.idCabinbed);
            modelBuilder.Entity<CabinType>().HasKey(x => x.idCabinType);
        }

        public virtual DbSet<Customer> Customer { get; set; } = default!;
        public virtual DbSet<Role> Role { get; set; } = default!;
        public virtual DbSet<Cruises> Cruises { get; set; } = default!;
        public virtual DbSet<Photo> Photo { get; set; } = default!;
        public virtual DbSet<CruisePoint> CruisePoint { get; set; } = default!;
        
        public virtual DbSet<Ship> Ship { get; set; } = default!;
        public virtual DbSet<Cabin> Cabin { get; set; } = default!;
        public virtual DbSet<CabinBed> CabinBed { get; set; } = default!;
        public virtual DbSet<CabinType> CabinType { get; set; } = default!;
    }
}