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
            //Context.Database.CloseConnection();

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

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
