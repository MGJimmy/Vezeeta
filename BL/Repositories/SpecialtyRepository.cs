using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using DAL.Models;

namespace BL.Repositories
{
   public class SpecialtyRepository:BaseRepository<Specialty>
    {
        public SpecialtyRepository(DbContext dbcontext) : base(dbcontext)
        {
        }

        public bool CheckExixt(Specialty specialty)
        {
            return GetAny(c => c.ID == specialty.ID);
        }
    }
}
