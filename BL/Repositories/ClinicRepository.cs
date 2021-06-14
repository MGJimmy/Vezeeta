using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class ClinicRepository : BaseRepository<Clinic>
    {
        public ClinicRepository(DbContext dbcontext) : base(dbcontext)
        {
        }
        public Clinic GetByStringId(string doctorID)
        {
            return GetFirstOrDefault(c => c.DoctorId == doctorID);
        }
        public Clinic GetByIdWithArea(string doctorID)
        {
            return DbSet.Where(i => i.DoctorId == doctorID).Include(i => i.Area).FirstOrDefault();
        }

        //internal Clinic GetClinicWithClinicServices(string doctorId)
        //{
        //    return DbSet.Where(c => c.DoctorId == doctorId).Include(c=>c.ClinicServices).FirstOrDefault();
        //}
        
    }
}
