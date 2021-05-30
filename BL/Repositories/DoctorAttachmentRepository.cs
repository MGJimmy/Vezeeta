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
        public IEnumerable<DoctorAttachment> GetDoctorAttachment(bool isAccepted)
        {
            return DbSet.Where(doctorAttchament => doctorAttchament.Doctor.IsAccepted == isAccepted);
        }
    }
}
