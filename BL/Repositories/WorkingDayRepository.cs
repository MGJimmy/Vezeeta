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
    public class WorkingDayRepository: BaseRepository<WorkingDay>
    {
        public WorkingDayRepository(DbContext dbcontext):base(dbcontext)
        {
        }

        internal IEnumerable<WorkingDay> GetWorkingDaysForDoctor(string doctorId)
        {
            return DbSet.Where(w => w.ClinicId == doctorId).Include(w => w.DayShifts).ToList();
        }
    }
}
