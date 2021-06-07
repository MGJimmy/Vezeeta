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
            builder.Entity<ApplicationUserIdentity>().HasData(
             new ApplicationUserIdentity { UserName = "admin", Email = "example.gmail.com",PasswordHash="123456" }
            );
            builder.Entity<City>()
                .HasMany(ci => ci.Clinics)
                .WithOne(cl => cl.City)
                .HasForeignKey(cl=>cl.CityId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Area>()
                .HasMany(a => a.clinics)
                .WithOne(cl => cl.Area)
                .HasForeignKey(cl => cl.AreaId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Specialty>()
                .HasMany(s => s.doctors)
                .WithOne(d=>d.specialty)
                .HasForeignKey(d=>d.specialtyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Doctor_DoctorService>().HasKey(dds => new { dds.doctorID, dds.serviceID });

        }

     
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<SupSpecialization> supSpecializations { get; set; }
        public DbSet<Clinicservice> Clinicservices{ get; set; }
        public DbSet<Doctor> Doctors{ get; set; }
        public DbSet<Clinic> Clinics{ get; set; }
        public DbSet<Doctor_DoctorService> Doctor_DoctorServices { get; set; }
        

    }
}
