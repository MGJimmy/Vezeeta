using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using BL.DTOs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class DoctorServiceRepository : BaseRepository<DoctorService>
    {
        public DoctorServiceRepository(DbContext dbcontext):base(dbcontext)
        {
        }

        public bool CheckDoctorServiceExistByName(string serviceName)
        {
            return GetAny(c => c.Name == serviceName);
        }
        public override ICollection<DoctorService> GetAll()
        {
            return DbSet.Where(doctorService => doctorService.ByAdmin == true).ToList();
        }
        public int CountEntity(bool byAdmin)
        {
            return DbSet.Where(doctorService => doctorService.ByAdmin == byAdmin).Count();
        }
        public   IEnumerable<DoctorService> GetPageRecords(int pageSize, int pageNumber, bool byAdmin)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet
                .Where(doctorService => doctorService.ByAdmin == byAdmin)
                .Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
        public void acceptDoctorService(int id)
        {
           
            DoctorService doctorService = DbSet.FirstOrDefault(doctorService => doctorService.ID == id);
            doctorService.ByAdmin = true;


        }
        public void rejectDoctorService(int id)
        {
          
            DoctorService doctorService = DbSet.FirstOrDefault(doctorService => doctorService.ID == id);
            this.Delete(id);

        }
    }
}
