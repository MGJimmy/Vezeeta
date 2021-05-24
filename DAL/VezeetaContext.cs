using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VezeetaContext: IdentityDbContext<ApplicationUserIdentity>
    {
        public VezeetaContext()
        {

        }
        public VezeetaContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<City>()
                .HasIndex(u => u.Name)
                .IsUnique();
            builder.Entity<Area>()
                .HasIndex(u => u.Name)
                .IsUnique();
            builder.Entity<Specialty>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }

     
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Clinicservice> Clinicservices{ get; set; }

    }
}
