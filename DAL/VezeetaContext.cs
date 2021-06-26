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
            
            builder.Entity<City>()
                .HasMany(ci => ci.Clinics)
                .WithOne(cl => cl.City)
                .HasForeignKey(cl => cl.CityId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Area>()
                .HasMany(a => a.clinics)
                .WithOne(cl => cl.Area)
                .HasForeignKey(cl => cl.AreaId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ClinicClinicService>()
                .HasKey(c => new { c.ClinicId, c.ClinicServiceId });
            builder.Entity<Specialty>()
                .HasMany(s => s.doctors)
                .WithOne(d => d.specialty)
                .HasForeignKey(d => d.specialtyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Doctor_DoctorService>().HasKey(dds => new { dds.doctorID, dds.serviceID });

            builder.Entity<DoctorSubSpecialization>()
                .HasKey(ds => new { ds.DoctorId, ds.subSpecializeId });


            builder.Entity<Offer>()
                .HasMany(o => o.MakeOffers)
                .WithOne(m => m.Offer)
                .HasForeignKey(m => m.OfferId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<SubOffer>()
                .HasMany(s => s.MakeOffers)
                .WithOne(m => m.SubOffer)
                .HasForeignKey(m => m.SubOfferId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Doctor>()
                .HasMany(d => d.Rates)
                .WithOne(r => r.Doctor)
                .HasForeignKey(r => r.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<OfferRating>()
                .HasOne(o => o.ReserveOffer)
                .WithOne(r => r.OfferRating)
                .OnDelete(DeleteBehavior.NoAction);

        }


        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<SupSpecialization> supSpecializations { get; set; }
        public DbSet<Clinicservice> Clinicservices{ get; set; }
        public DbSet<Doctor> Doctors{ get; set; }
        public DbSet<Clinic> Clinics{ get; set; }
        public DbSet<Doctor_DoctorService> Doctor_DoctorServices { get; set; }
        public DbSet<DoctorSubSpecialization> DoctorSubSpecialization { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<SubOffer> SubOffers { get; set; }
        public DbSet<MakeOffer> MakeOffers { get; set; }

        public DbSet<MakeOfferImage> MakeOfferImages { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<OfferRating> OfferRatings { get; set; }

    }
}
