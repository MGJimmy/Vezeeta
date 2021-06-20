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
    public class RatingRepository: BaseRepository<Rating>
    {
        public RatingRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override ICollection<Rating> GetWhere(Expression<Func<Rating, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Rating> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter).Include(r => r.User);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.ToList();
        }
        public  ICollection<Rating> GetWhereWithPaging(int CommentNumber, Expression<Func<Rating, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Rating> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter).Take(CommentNumber).Include(r => r.User);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.ToList();
        }
    }
}
