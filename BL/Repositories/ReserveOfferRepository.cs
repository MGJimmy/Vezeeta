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
    public class ReserveOfferRepository : BaseRepository<ReserveOffer>
    {
        public ReserveOfferRepository(DbContext db) : base(db)
        {
        }
        //public ICollection<int> Getlast3doctorOffersIDfromReservationforSuggestion(Expression<Func<ReserveOffer, bool>> filter = null, string includeProperties = "")
        //{
        //    if (filter != null)
        //    {
        //        return DbSet.Where(filter).OrderByDescending(r => r.Date).Select(r => r.MakeOfferId).Distinct().Take(3).ToList();
        //    }
        //    return null;
        //}
    }
}
