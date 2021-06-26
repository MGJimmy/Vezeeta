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
        public Doctor GetByName(string name)
        {
            Doctor doctor = DbSet.Where(i => i.User.UserName == name).Include(i => i.User).FirstOrDefault();
            return doctor;
        }

        public Doctor GetWithClinicDetails(string name)
        {
            Doctor doctor = DbSet.Where(i => i.User.UserName == name).Include(i => i.User).FirstOrDefault();
            return doctor;
        }

        //public Doctor GetSubSpecialtyByDoctorId(string id)
        //{
        //    Doctor doctor = DbSet.Where(i => i.UserId == id).Include(i=>i.supSpecializations).FirstOrDefault();
        //    return doctor;
        //}
        //public void activateDoctor(string doctorID)
        //{
        //    Doctor doctor = DbSet.FirstOrDefault(d => d.UserId == doctorID);
        //    doctor.IsAccepted = true;
        //}
        //public void deactivateDoctor(string doctorID)
        //{
        //    Doctor doctor = DbSet.FirstOrDefault(d => d.UserId == doctorID);
        //    doctor.IsAccepted = false;
        //}
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


        public virtual IEnumerable<Doctor> Get_All_Doctors_Where(Expression<Func<Doctor, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Doctor> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter).Include(d => d.User).Include(d => d.DoctorSubSpecialization).Include(d => d.doctor_doctorServices).Include(d => d.specialty).Include(d => d.clinic);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }

        public Doctor GetDoctorDetailswithID(string id)
        {
            Doctor doctor = DbSet.Include(d => d.User).Include(d => d.DoctorSubSpecialization).Include(d => d.doctor_doctorServices).Include(d => d.specialty).FirstOrDefault(i => i.UserId == id);
            return doctor;
        }

        public IEnumerable<Doctor> GetAllDoctorForSearch()
        {
            //pageSize = (pageSize > 10 || pageSize < 0) ? 10 : pageSize;
            //pagNumber = (pagNumber < 0) ? 1 : pagNumber;



            var result = DbSet.Where(d=>d.IsAccepted == true).OrderByDescending(d=>d.AverageRate).Include(d => d.User)
                .Include(d => d.clinic)
                .ThenInclude(d => d.City)
                .Include(d => d.clinic)
                .ThenInclude(d => d.Area)
                .Include(d => d.specialty)
                .Include(d=>d.DoctorSubSpecialization).AsQueryable();
            //.Include(d => d.DoctorSubSpecialization).AsQueryable();

            return result;

            

        }
        public IEnumerable<Doctor> GetAllDoctors(int pageSize, int pagNumber, int? specialtyId, int? cityId, int? areaId, string name)
        {
            pageSize = (pageSize > 10 || pageSize < 0) ? 10 : pageSize;
            pagNumber = (pagNumber < 0) ? 1 : pagNumber;



            var result = DbSet.Include(d => d.User)
                .Include(d => d.clinic)
                .ThenInclude(d => d.City)
                .Include(d => d.clinic)
                .ThenInclude(d => d.Area)
                .Include(d => d.specialty).AsQueryable();
            //.Include(d => d.DoctorSubSpecialization).AsQueryable();

            if (specialtyId != null)
            {
                result = result.Where(d => d.specialtyId == specialtyId);
            }

            if (cityId != null)
            {
                result = result.Where(d => d.clinic.CityId == cityId);
            }

            if (areaId != null)
            {
                result = result.Where(d => d.clinic.AreaId == areaId);
            }

            if (name != null)
            {
                result = result.Where(d => d.User.FullName.Contains(name));
            }


            return result.Skip((pagNumber - 1) * pageSize).Take(pageSize);

        }


        public List<Doctor> suggestiondoctorswithspecailtyid(int specialtyid, int countofreturneddoctors)
        {
           return DbSet.Where(d => d.specialtyId == specialtyid && d.IsAccepted==true).OrderByDescending(d => d.AverageRate).Take(countofreturneddoctors).Include(d=>d.User).Include(d=>d.specialty).
                Include(d=>d.clinic.City).ToList();
        }

        public List<Doctor> suggestiondoctorsTopRated(int countofreturneddoctors)
        {
            return DbSet.Where(d=> d.IsAccepted == true).OrderByDescending(d => d.AverageRate).Take(countofreturneddoctors).Include(d => d.User).Include(d => d.specialty).
                 Include(d => d.clinic.City).ToList();
        }


    }
}