using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using BL.DTOs;
using BL.DTOs.DayShiftDTO;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class DayShiftRepository: BaseRepository<DayShift>
    {
        public DayShiftRepository(DbContext dbcontext):base(dbcontext)
        {
        }
        public DayShift getDay()
        {
            return GetWhere(d => d.Id == 7).FirstOrDefault();
        } 
    }
}
