﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(VezeetaContext))]
    partial class VezeetaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.ApplicationUserIdentity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDoctor")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "b4efe154-78e6-47d3-9301-417214588196",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "21bc0356-f0cc-470e-856f-12f3bdef6a95",
                            Email = "example.gmail.com",
                            EmailConfirmed = false,
                            IsDoctor = false,
                            LockoutEnabled = false,
                            PasswordHash = "123456",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "31c6d41b-4191-4ab2-a806-80d327afbf57",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("DAL.Models.Area", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ByAdmin")
                        .HasColumnType("bit");

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Area");
                });

            modelBuilder.Entity("DAL.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("City");
                });

            modelBuilder.Entity("DAL.Models.Clinic", b =>
                {
                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("ExaminationTime")
                        .HasColumnType("int");

                    b.Property<int>("Fees")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WatingTime")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.HasIndex("AreaId");

                    b.HasIndex("CityId");

                    b.ToTable("Clinic");
                });

            modelBuilder.Entity("DAL.Models.ClinicClinicService", b =>
                {
                    b.Property<string>("ClinicId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ClinicServiceId")
                        .HasColumnType("int");

                    b.HasKey("ClinicId", "ClinicServiceId");

                    b.HasIndex("ClinicServiceId");

                    b.ToTable("ClinicClinicService");
                });

            modelBuilder.Entity("DAL.Models.ClinicImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClinicId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.ToTable("ClinicImage");
                });

            modelBuilder.Entity("DAL.Models.Clinicservice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ByAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clinicservices");
                });

            modelBuilder.Entity("DAL.Models.DayShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("From")
                        .HasColumnType("time");

                    b.Property<int>("MaxNumOfReservation")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("To")
                        .HasColumnType("time");

                    b.Property<int>("WorkingDayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkingDayId");

                    b.ToTable("DayShift");
                });

            modelBuilder.Entity("DAL.Models.Doctor", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("TitleDegree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("doctorInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("specialtyId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("specialtyId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("DAL.Models.DoctorAttachment", b =>
                {
                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoctorSyndicateIdImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OpenClinicPermissionImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalIdImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isBinding")
                        .HasColumnType("bit");

                    b.HasKey("DoctorId");

                    b.ToTable("DoctorAttachment");
                });

            modelBuilder.Entity("DAL.Models.DoctorService", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ByAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("DoctorUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DoctorUserId");

                    b.ToTable("DoctorService");
                });

            modelBuilder.Entity("DAL.Models.DoctorSubSpecialization", b =>
                {
                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("subSpecializeId")
                        .HasColumnType("int");

                    b.HasKey("DoctorId", "subSpecializeId");

                    b.HasIndex("subSpecializeId");

                    b.ToTable("DoctorSubSpecialization");
                });

            modelBuilder.Entity("DAL.Models.Doctor_DoctorService", b =>
                {
                    b.Property<string>("doctorID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("serviceID")
                        .HasColumnType("int");

                    b.HasKey("doctorID", "serviceID");

                    b.HasIndex("serviceID");

                    b.ToTable("Doctor_DoctorServices");
                });

            modelBuilder.Entity("DAL.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("DAL.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<string>("Symptoms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("dayShiftId")
                        .HasColumnType("int");

                    b.Property<string>("doctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("gender")
                        .HasColumnType("int");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("dayShiftId");

                    b.HasIndex("doctorId");

                    b.HasIndex("userId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("DAL.Models.Specialty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Specialty");
                });

            modelBuilder.Entity("DAL.Models.SubOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("SubOffers");
                });

            modelBuilder.Entity("DAL.Models.SupSpecialization", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ByAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("specialtyId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("specialtyId");

                    b.ToTable("supSpecializations");
                });

            modelBuilder.Entity("DAL.Models.WorkingDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClinicId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Day")
                        .HasMaxLength(6)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.ToTable("WorkingDay");
                });

            modelBuilder.Entity("DoctorDoctorService", b =>
                {
                    b.Property<string>("DoctorsUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("doctorServicesID")
                        .HasColumnType("int");

                    b.HasKey("DoctorsUserId", "doctorServicesID");

                    b.HasIndex("doctorServicesID");

                    b.ToTable("DoctorDoctorService");
                });

            modelBuilder.Entity("DoctorSupSpecialization", b =>
                {
                    b.Property<string>("doctorsUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("supSpecializationsID")
                        .HasColumnType("int");

                    b.HasKey("doctorsUserId", "supSpecializationsID");

                    b.HasIndex("supSpecializationsID");

                    b.ToTable("DoctorSupSpecialization");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DAL.Models.Area", b =>
                {
                    b.HasOne("DAL.Models.City", "City")
                        .WithMany("Areas")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("DAL.Models.Clinic", b =>
                {
                    b.HasOne("DAL.Models.Area", "Area")
                        .WithMany("clinics")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Models.City", "City")
                        .WithMany("Clinics")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DAL.Models.Doctor", "Doctor")
                        .WithOne("clinic")
                        .HasForeignKey("DAL.Models.Clinic", "DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("City");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("DAL.Models.ClinicClinicService", b =>
                {
                    b.HasOne("DAL.Models.Clinic", "Clinic")
                        .WithMany("ClinicClinicServices")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Clinicservice", "ClinicService")
                        .WithMany("ClinicClinicServices")
                        .HasForeignKey("ClinicServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("ClinicService");
                });

            modelBuilder.Entity("DAL.Models.ClinicImage", b =>
                {
                    b.HasOne("DAL.Models.Clinic", "Clinic")
                        .WithMany("ClinicImages")
                        .HasForeignKey("ClinicId");

                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("DAL.Models.DayShift", b =>
                {
                    b.HasOne("DAL.Models.WorkingDay", "WorkingDay")
                        .WithMany("DayShifts")
                        .HasForeignKey("WorkingDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkingDay");
                });

            modelBuilder.Entity("DAL.Models.Doctor", b =>
                {
                    b.HasOne("DAL.ApplicationUserIdentity", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("DAL.Models.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Specialty", "specialty")
                        .WithMany("doctors")
                        .HasForeignKey("specialtyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("specialty");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.DoctorAttachment", b =>
                {
                    b.HasOne("DAL.Models.Doctor", "Doctor")
                        .WithOne("DoctorAttachment")
                        .HasForeignKey("DAL.Models.DoctorAttachment", "DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("DAL.Models.DoctorService", b =>
                {
                    b.HasOne("DAL.Models.Doctor", null)
                        .WithMany("doctorServices")
                        .HasForeignKey("DoctorUserId");
                });

            modelBuilder.Entity("DAL.Models.DoctorSubSpecialization", b =>
                {
                    b.HasOne("DAL.Models.Doctor", "doctor")
                        .WithMany("DoctorSubSpecialization")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.SupSpecialization", "supSpecialization")
                        .WithMany("DoctorSubSpecialization")
                        .HasForeignKey("subSpecializeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("supSpecialization");
                });

            modelBuilder.Entity("DAL.Models.Doctor_DoctorService", b =>
                {
                    b.HasOne("DAL.Models.Doctor", "doctor")
                        .WithMany("doctor_doctorServices")
                        .HasForeignKey("doctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.DoctorService", "service")
                        .WithMany("doctor_doctorServices")
                        .HasForeignKey("serviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("service");
                });

            modelBuilder.Entity("DAL.Models.Reservation", b =>
                {
                    b.HasOne("DAL.Models.DayShift", "dayShift")
                        .WithMany("reservations")
                        .HasForeignKey("dayShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Doctor", "Doctor")
                        .WithMany("Reservations")
                        .HasForeignKey("doctorId");

                    b.HasOne("DAL.ApplicationUserIdentity", "User")
                        .WithMany("reservations")
                        .HasForeignKey("userId");

                    b.Navigation("dayShift");

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.SubOffer", b =>
                {
                    b.HasOne("DAL.Models.Offer", "Offer")
                        .WithMany("SubOffers")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("DAL.Models.SupSpecialization", b =>
                {
                    b.HasOne("DAL.Models.Specialty", "specialty")
                        .WithMany()
                        .HasForeignKey("specialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("specialty");
                });

            modelBuilder.Entity("DAL.Models.WorkingDay", b =>
                {
                    b.HasOne("DAL.Models.Clinic", "Clinic")
                        .WithMany("WorkingDays")
                        .HasForeignKey("ClinicId");

                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("DoctorDoctorService", b =>
                {
                    b.HasOne("DAL.Models.Doctor", null)
                        .WithMany()
                        .HasForeignKey("DoctorsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.DoctorService", null)
                        .WithMany()
                        .HasForeignKey("doctorServicesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorSupSpecialization", b =>
                {
                    b.HasOne("DAL.Models.Doctor", null)
                        .WithMany()
                        .HasForeignKey("doctorsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.SupSpecialization", null)
                        .WithMany()
                        .HasForeignKey("supSpecializationsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DAL.ApplicationUserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DAL.ApplicationUserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.ApplicationUserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DAL.ApplicationUserIdentity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.ApplicationUserIdentity", b =>
                {
                    b.Navigation("Doctor");

                    b.Navigation("reservations");
                });

            modelBuilder.Entity("DAL.Models.Area", b =>
                {
                    b.Navigation("clinics");
                });

            modelBuilder.Entity("DAL.Models.City", b =>
                {
                    b.Navigation("Areas");

                    b.Navigation("Clinics");
                });

            modelBuilder.Entity("DAL.Models.Clinic", b =>
                {
                    b.Navigation("ClinicClinicServices");

                    b.Navigation("ClinicImages");

                    b.Navigation("WorkingDays");
                });

            modelBuilder.Entity("DAL.Models.Clinicservice", b =>
                {
                    b.Navigation("ClinicClinicServices");
                });

            modelBuilder.Entity("DAL.Models.DayShift", b =>
                {
                    b.Navigation("reservations");
                });

            modelBuilder.Entity("DAL.Models.Doctor", b =>
                {
                    b.Navigation("clinic");

                    b.Navigation("doctor_doctorServices");

                    b.Navigation("DoctorAttachment");

                    b.Navigation("doctorServices");

                    b.Navigation("DoctorSubSpecialization");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("DAL.Models.DoctorService", b =>
                {
                    b.Navigation("doctor_doctorServices");
                });

            modelBuilder.Entity("DAL.Models.Offer", b =>
                {
                    b.Navigation("SubOffers");
                });

            modelBuilder.Entity("DAL.Models.Specialty", b =>
                {
                    b.Navigation("doctors");
                });

            modelBuilder.Entity("DAL.Models.SupSpecialization", b =>
                {
                    b.Navigation("DoctorSubSpecialization");
                });

            modelBuilder.Entity("DAL.Models.WorkingDay", b =>
                {
                    b.Navigation("DayShifts");
                });
#pragma warning restore 612, 618
        }
    }
}
