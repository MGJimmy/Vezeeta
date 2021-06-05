using BL.Interfaces;
using BL.Repositories;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        DbContext Context;
        UserManager<ApplicationUserIdentity> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(VezeetaContext context,
            UserManager<ApplicationUserIdentity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            Context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            BeginTransaction();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
        public void BeginTransaction()
        {

            Context.Database.CloseConnection();

            if (Context.Database.CurrentTransaction == null)
                Context.Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (Context.Database.CurrentTransaction != null)
                Context.Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            if (Context.Database.CurrentTransaction != null)
                Context.Database.RollbackTransaction();
        }

        private CityRepository cityRepo;
        public CityRepository CityRepo
        {
            get
            {
                if (cityRepo == null) 
                    cityRepo = new CityRepository(Context);
                return cityRepo;
            }
        }

        private AreaRepositories areaRepo;
        public AreaRepositories AreaRepo
        {
            get
            {
                if (areaRepo == null)
                    areaRepo = new AreaRepositories(Context);
                return areaRepo;
            }
        }

        private SpecialtyRepository specialtyRepo;
        public SpecialtyRepository SpecialtyRepo
        {
            get
            {
                if (specialtyRepo == null)
                    specialtyRepo = new SpecialtyRepository(Context);
                return specialtyRepo;
            }
        }
        private SupSpecializationRepository sup_SpecializationRepo;
        public SupSpecializationRepository SupSpecializationRepo
        {
            get
            {
                if (sup_SpecializationRepo == null)
                    sup_SpecializationRepo = new SupSpecializationRepository(Context);
                return sup_SpecializationRepo;
            }
        }


        private ClincServicesRepositry clincServicesRepositry;
        public ClincServicesRepositry ClincServicesRepo
        {
            get
            {
                if (clincServicesRepositry == null)
                    clincServicesRepositry = new ClincServicesRepositry(Context);
                return clincServicesRepositry;
            }
        }
        private DoctorRepository doctorRepo;
        public DoctorRepository DoctorRepo
        {
            get
            {
                if (doctorRepo == null)
                    doctorRepo = new DoctorRepository(Context);
                return doctorRepo;
            }
        }
        private DoctorAttachmentRepository doctorAttachmentRepo;
        public DoctorAttachmentRepository DoctorAttachmentRepo
        {
            get
            {
                if (doctorAttachmentRepo == null)
                    doctorAttachmentRepo = new DoctorAttachmentRepository(Context);
                return doctorAttachmentRepo;
            }
        }
        private AccountRepository accountRepo;
        public AccountRepository AccountRepo
        {
            get
            {
                if (accountRepo == null)
                    accountRepo = new AccountRepository(Context,_userManager, _roleManager);
                return accountRepo;
            }
        }
        private WorkingDayRepository workingDayRepo;
        public WorkingDayRepository WorkingDayRepo
        {
            get
            {
                if (workingDayRepo == null)
                    workingDayRepo = new WorkingDayRepository(Context);
                return workingDayRepo;
            }
        }
        private DayShiftRepository dayShiftRepo;
        public DayShiftRepository DayShiftRepo
        {
            get
            {
                if (dayShiftRepo == null)
                    dayShiftRepo = new DayShiftRepository(Context);
                return dayShiftRepo;
            }
        }

        private ClinicRepository clinicRepo;
        public ClinicRepository ClinicRepo
        {
            get
            {
                if (clinicRepo == null)
                    clinicRepo = new ClinicRepository(Context);
                return clinicRepo;
            }
        }

        private ClinicImagesRepository clinicImagesRepo;
        public ClinicImagesRepository ClinicImagesRepo
        {
            get
            {
                if (clinicImagesRepo == null)
                    clinicImagesRepo = new ClinicImagesRepository(Context);
                return clinicImagesRepo;
            }
        }
        private RoleRepository roleRepo;
        public RoleRepository RoleRepo
        {
            get
            {
                if (roleRepo == null)
                    roleRepo = new RoleRepository(Context,_roleManager);
                return roleRepo;
            }
        }



        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
