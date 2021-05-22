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
    class UnitOfWork : IUnitOfWork
    {
        DbContext Context;
        public UnitOfWork(VezeetaContext context )
        {
            Context = context;
        }

        public int Commit()
        {
            return Context.SaveChanges();
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
