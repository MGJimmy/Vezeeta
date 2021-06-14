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
    public class DoctorRepository : BaseRepository<Doctor>
    {
        public DoctorRepository(DbContext db) : base(db)
        {
        }
        public Doctor GetById(string id)
        {

            Doctor doctor = DbSet.FirstOrDefault(i => i.UserId == id);
            return doctor;
        }
        public Doctor GetSubSpecialtyByDoctorId(string id)
        {
            Doctor doctor = DbSet.Where(i => i.UserId == id).Include(i => i.supSpecializations).FirstOrDefault();
            return doctor;
        }
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


        public void InsertSpecialtyToDoctor(string doctorId, Specialty specialty)
        {
            var doctor = DbSet.Find(doctorId);
            if (doctor != null)
            {
                doctor.specialtyId = specialty.ID;
                DbSet.Update(doctor);
            }
        }
        public void InsertSubSpecialtyToDoctor(string doctorId, List<SupSpecialization> subSpecialties)
        {
            var doctor = DbSet.Where(d => d.UserId == doctorId).Include(d => d.supSpecializations).FirstOrDefault();
            //var doctor = DbSet.Find(doctorId);
            if (doctor != null && subSpecialties != null)
            {
                doctor.supSpecializations = subSpecialties;
                //foreach (var item in subSpecialties)
                //{
                //    doctor.supSpecializations.Add(item);
                //}

                // DbSet.Update(doctor);
            }
        }

        public void EmptySubSpecialtyInDoctor(string doctorId)
        {
            var doctor = DbSet.Where(x => x.UserId == doctorId).Include(x => x.supSpecializations).FirstOrDefault();
            if (doctor != null)
            {
                //for(var i=doctor.supSpecializations.Count-1;i>= 0; i--)
                //{
                //    Context.Entry(doctor.supSpecializations[i]).State = EntityState.Detached;
                //    doctor.supSpecializations.Remove(doctor.supSpecializations[i]);
                //}

                //doctor.supSpecializations.ForEach(i => {
                //    doctor.supSpecializations.Remove(i);
                //    Context.Entry(i).State = EntityState.Detached;
                //    });

                doctor.supSpecializations = null;
                //Context.Entry<List<SupSpecialization>>(doctor.supSpecializations).State = EntityState.Detached;

                //DbSet.Update(doctor);
            }

        }

        public IEnumerable<Doctor> GetAllDoctors(int pageSize, int pagNumber, int? specialtyId, int? cityId, int? areaId, string name)
        {
            pageSize = (pageSize > 10 || pageSize < 0) ? 10 : pageSize;
            pagNumber = (pagNumber < 0) ? 1 : pagNumber;


            
            var result = DbSet.Include(d => d.User)
                .Include(d => d.Clinic)
                .ThenInclude(d => d.City)
                .Include(d => d.Clinic)
                .ThenInclude(d => d.Area)
                .Include(d => d.specialty)
                .Include(d => d.supSpecializations).AsQueryable();

            if (specialtyId != null)
            {
                result = result.Where(d => d.specialtyId == specialtyId);
            }

            if (cityId != null)
            {
                result = result.Where(d => d.Clinic.CityId == cityId);
            }

            if (areaId != null)
            {
                result = result.Where(d => d.Clinic.AreaId == areaId);
            }

            if (name != null)
            {
                result = result.Where(d => d.User.FullName.Contains(name));
            }


            return result.Skip((pagNumber - 1) * pageSize).Take(pageSize);

        }
    }
}
