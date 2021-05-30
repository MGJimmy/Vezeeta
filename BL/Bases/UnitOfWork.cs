using BL.Interfaces;
using BL.Repositories;
using DAL;
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
        public UnitOfWork(VezeetaContext context )
        {
            Context = context;
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


        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
