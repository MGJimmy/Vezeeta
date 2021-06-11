using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class SubOfferRepository:BaseRepository<SubOffer>
    {
        public SubOfferRepository(DbContext context):base(context)
        {

        }
        public override IEnumerable<SubOffer> GetPageRecords(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet.Skip(pageNumber * pageSize).Take(pageSize).Include(a => a.Offer);
        }
    }
}
