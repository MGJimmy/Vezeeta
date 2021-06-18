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
    public class MakeOfferRepository:BaseRepository<MakeOffer>
    {
        public MakeOfferRepository(DbContext context):base(context)
        {
        }
        public override List<MakeOffer> GetAll()
        {
            return DbSet.Include(i => i.SubOffer).Include(i=>i.Doctor.User).Include(i => i.OfferImages).ToList();
        }
        public override MakeOffer GetById(int id)
        {
            return DbSet.Where(i=>i.Id==id).Include(i => i.Doctor.User).Include(i => i.OfferImages).FirstOrDefault();
        }
        public List<MakeOffer> GetAllByDoctorId(string id)
        {
            return DbSet.Where(i=>i.DoctorId==id).Include(i => i.SubOffer).Include(i => i.OfferImages).ToList();
        }
        public override ICollection<MakeOffer> GetWhere(Expression<Func<MakeOffer, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<MakeOffer> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter).Include(i=>i.OfferImages);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.ToList();
        }


    }
}
