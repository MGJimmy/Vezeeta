using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class SupSpecializationRepository: BaseRepository<SupSpecialization>
    {
            public SupSpecializationRepository(DbContext dbcontext) : base(dbcontext)
            {
            }
        public bool CheckExistByName(SupSpecialization SupSpecail)
        {
            return GetAny(ar => ar.Name == SupSpecail.Name);
        }

        public int CountOfAccept()
        {
            return DbSet.Where(i => i.ByAdmin == true).Count();
        }
        public int CountOfNotAccept()
        {
            return DbSet.Where(i => i.ByAdmin == false).Count();
        }
        public override IEnumerable<SupSpecialization> GetPageRecords(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet.Where(i => i.ByAdmin == true).Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<SupSpecialization> GetNotAcceptPageRecords(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet.Where(i => i.ByAdmin == false).Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

    }
}
