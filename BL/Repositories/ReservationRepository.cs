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


    }
}
