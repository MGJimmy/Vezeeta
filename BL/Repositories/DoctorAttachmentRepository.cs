using BL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class DoctorAttachmentRepository: BaseRepository<DoctorAttachment>
    {
        public DoctorAttachmentRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public DoctorAttachment GetById(string id)
        {
            return DbSet.Where(i => i.DoctorId == id).Include(i => i.Doctor).FirstOrDefault();
        }
        public IEnumerable<DoctorAttachment> GetDoctorAttachment(bool isAccepted)
        {
            var t= DbSet.Where(doctorAttchament => doctorAttchament.Doctor.IsAccepted == isAccepted);
            return t;
        }
        public override int CountEntity()
        {
            return DbSet.Where(doctorAttchament => doctorAttchament.isBinding == true).Count();
        }
        public override IEnumerable<DoctorAttachment> GetPageRecords(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet
                .Where(doctorAttchament => doctorAttchament.isBinding == true)
                .Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
      
        public void changeBindingAndRejectedStatus(string doctorId,bool rejectState)
        {
            //accept attchment
            DoctorAttachment doctorAttachment= DbSet.FirstOrDefault(attachment => attachment.DoctorId == doctorId);
            doctorAttachment.isBinding = false;
            doctorAttachment.Rejected = rejectState;
           
          
        }
     
    }
}
