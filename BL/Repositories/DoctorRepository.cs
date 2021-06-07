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
    public class DoctorRepository:BaseRepository<Doctor>
    {
        public DoctorRepository(DbContext db):base(db)
        {
        }
        public Doctor GetById(string id)
        {
            Doctor doctor = DbSet.FirstOrDefault(i => i.UserId == id);
            return doctor;
        }
        //public Doctor GetSubSpecialtyByDoctorId(string id)
        //{
        //    Doctor doctor = DbSet.Where(i => i.UserId == id).Include(i=>i.supSpecializations).FirstOrDefault();
        //    return doctor;
        //}
        public void activateDoctor(string doctorID)
        {
            Doctor doctor = DbSet.FirstOrDefault(d => d.UserId == doctorID);
            doctor.IsAccepted = true;
        }
        public void deactivateDoctor(string doctorID)
        {
            Doctor doctor = DbSet.FirstOrDefault(d => d.UserId == doctorID);
            doctor.IsAccepted = false;
        }
        //public Doctor GetByStringId(string id)
        //{
        //    return DbSet.Include(i => i.doctorServices).FirstOrDefault(d=>d.UserId==id);
        //}


        //public void InsertSpecialtyToDoctor(string doctorId, Specialty specialty)
        //{
        //    var doctor = DbSet.Find(doctorId);
        //    if (doctor != null)
        //    {
        //        doctor.specialtyId = specialty.ID;
        //        DbSet.Update(doctor);
        //    }
        //}
        
    }
}
