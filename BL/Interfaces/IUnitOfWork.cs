using BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methods
        int SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        #endregion

        CityRepository CityRepo { get; }
        SpecialtyRepository SpecialtyRepo { get; }
        SupSpecializationRepository SupSpecializationRepo { get; }

        AreaRepositories AreaRepo { get; }
        ClincServicesRepositry ClincServicesRepo { get; }
        DoctorRepository DoctorRepo { get; }
        DoctorAttachmentRepository DoctorAttachmentRepo { get; }
        AccountRepository AccountRepo { get; }
        RoleRepository RoleRepo { get; }
        ClinicRepository ClinicRepo { get; }
        ClinicImagesRepository ClinicImagesRepo { get; }
        WorkingDayRepository WorkingDayRepo { get; }
        DayShiftRepository DayShiftRepo { get; }
        DoctorServiceRepository DoctorServiceRepo { get; }
        ClinicClinicServiceRepository ClinicClinicServiceRepo { get; }

    }
}

