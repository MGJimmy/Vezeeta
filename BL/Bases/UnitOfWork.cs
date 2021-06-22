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
        IMailService _mailService;

        public UnitOfWork(VezeetaContext context,
            UserManager<ApplicationUserIdentity> userManager, IMailService mailService,
            RoleManager<IdentityRole> roleManager)
        {
            Context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _mailService = mailService;
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
                    accountRepo = new AccountRepository(Context,_userManager,_mailService, _roleManager);
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
                    roleRepo = new RoleRepository(Context, _roleManager);
                return roleRepo;
            }
        }

        private DoctorServiceRepository doctorServiceRepository;
        public DoctorServiceRepository DoctorServiceRepo
        {
            get
            {
                if (doctorServiceRepository == null)
                    doctorServiceRepository = new DoctorServiceRepository(Context);
                return doctorServiceRepository;
            }
        }
        private ClinicClinicServiceRepository clinicClinicServiceRepo;
        public ClinicClinicServiceRepository ClinicClinicServiceRepo
        {
            get
            {
                if (clinicClinicServiceRepo == null)
                    clinicClinicServiceRepo = new ClinicClinicServiceRepository(Context);
                return clinicClinicServiceRepo;
            }
        }

        private DoctorSubSpecializationRepository doctorSubSpecializationRepository;
        public DoctorSubSpecializationRepository DoctorSubSpecializationRepo
        {
            get
            {
                if (doctorSubSpecializationRepository == null)
                    doctorSubSpecializationRepository = new DoctorSubSpecializationRepository(Context);
                return doctorSubSpecializationRepository;
            }
        }

        private ReservationRepository reservationRepository;
        public ReservationRepository ReservationRepo
        {
            get
            {
                if (reservationRepository == null)
                    reservationRepository = new ReservationRepository(Context);
                return reservationRepository;
            }
        }

        private OfferRepository offerRepository;
        public OfferRepository OfferRepo
        {
            get
            {
                if (offerRepository == null)
                    offerRepository = new OfferRepository(Context);
                return offerRepository;
            }
        }
        private SubOfferRepository subOfferRepository;
        public SubOfferRepository SubOfferRepo
        {
            get
            {
                if (subOfferRepository == null)
                    subOfferRepository = new SubOfferRepository(Context);
                return subOfferRepository;
            }
        }




        private Doctor_DoctorServiceRepository doctor_DoctorServiceRepo;
        public Doctor_DoctorServiceRepository Doctor_DoctorServiceRepo
        {
            get
            {
                if (doctor_DoctorServiceRepo == null)
                    doctor_DoctorServiceRepo = new Doctor_DoctorServiceRepository(Context);
                return doctor_DoctorServiceRepo;
            }
        }
        private MakeOfferRepository makeOfferRepo;
        public MakeOfferRepository MakeOfferRepo
        {
            get
            {
                if (makeOfferRepo == null)
                    makeOfferRepo = new MakeOfferRepository(Context);
                return makeOfferRepo;
            }
        }
        private MakeOfferImageRepository makeOfferImageRepo;
        public MakeOfferImageRepository MakeOfferImageRepo
        {
            get
            {
                if (makeOfferImageRepo == null)
                    makeOfferImageRepo = new MakeOfferImageRepository(Context);
                return makeOfferImageRepo;
            }
        }

        private ReserveOfferRepository reserveOfferRepo;
        public ReserveOfferRepository ReserveOfferRepo
        {
            get
            {
                if (reserveOfferRepo == null)
                    reserveOfferRepo = new ReserveOfferRepository(Context);
                return reserveOfferRepo;
            }
        }

        private RatingRepository ratingRepo;
        public RatingRepository RatingRepo
        {
            get
            {
                if (ratingRepo == null)
                    ratingRepo = new RatingRepository(Context);
                return ratingRepo;
            }
        }

        private OfferRatingRepository offerRatingRepo;
        public OfferRatingRepository OfferRatingRepo
        {
            get
            {
                if (offerRatingRepo == null)
                    offerRatingRepo = new OfferRatingRepository(Context);
                return offerRatingRepo;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
