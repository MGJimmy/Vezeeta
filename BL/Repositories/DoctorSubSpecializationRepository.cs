using BL.Bases;
using BL.DTOs.DoctorSubSpecialization;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class DoctorSubSpecializationRepository: BaseRepository<DoctorSubSpecialization>
    {
        public DoctorSubSpecializationRepository(DbContext dbContext):base(dbContext)
        {

        }

        public List<DoctorSubSpecialization> GetByDoctorId(string doctorId)
        {
            List<DoctorSubSpecialization> doctorSub = DbSet.Where(i => i.DoctorId == doctorId).ToList();
            return doctorSub;
        }
        public List<SupSpecialization> GetSubSpecialtyByDoctorId(string doctorId)
        {
            List<SupSpecialization> DoctorWithSubSpecialty = DbSet.Where(i => i.DoctorId == doctorId).Include(i => i.supSpecialization).Select(i=>i.supSpecialization).ToList();
            return DoctorWithSubSpecialty;
        }


        

        //public void EmptySubSpecialtyInDoctor(string doctorId)
        //{
        //    var doctor = DbSet.Where(x => x.UserId == doctorId).Include(x => x.supSpecializations).FirstOrDefault();
        //    if (doctor != null)
        //    {

        //        doctor.supSpecializations = null;
        //        //Context.Entry<List<SupSpecialization>>(doctor.supSpecializations).State = EntityState.Detached;

        //        //DbSet.Update(doctor);
        //    }

        //}
    }
}
