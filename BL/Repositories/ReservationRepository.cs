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
    public class ReservationRepository:BaseRepository<Reservation>
    {
        public ReservationRepository(DbContext db):base(db)
        {
        }

        //public ICollection<Reservation> GetWherewithDayShift(Expression<Func<Reservation, bool>> filter)
        //{
        //    return DbSet.Where(filter).Include(i=>i.dayShift.WorkingDay).ToList();
        //}

        public  ICollection<string> Getlast3doctorsIDfromReservationforSuggestion(Expression<Func<Reservation, bool>> filter = null, string includeProperties = "")
        {
            //IQueryable<Reservation> query = DbSet;

            if (filter != null)
            {
                return DbSet.Where(filter).OrderByDescending(r=>r.Date).Select(r => r.doctorId).Distinct().Take(3).ToList();
            }
            
            return null;
        }

    }
}
