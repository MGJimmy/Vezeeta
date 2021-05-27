using BL.Bases;
using BL.DTOs.AreaDTO;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class AreaRepositories : BaseRepository<Area>
    {
        public AreaRepositories(DbContext dbContext) : base(dbContext)
        {
        }
        public IEnumerable GetAllWithCity()
        {
            return DbSet.Include(i => i.City);
        }
        public IEnumerable GetWhereWithCity(Expression<Func<Area, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Area> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter).Include(a=>a.City);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }


        public override IEnumerable<Area> GetPageRecords(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet.Where(i => i.ByAdmin == true).Skip(pageNumber * pageSize).Take(pageSize).Include(a => a.City);
        }

        public IEnumerable<Area> GetNotAcceptPageRecords(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;

            return DbSet.Where(i => i.ByAdmin == false).Skip(pageNumber * pageSize).Take(pageSize).Include(a => a.City);
        }
        public bool CheckExistByName(string areaName)
        {
            return GetAny(ar => ar.Name == areaName);
        }
        public int CountOfAccept()
        {
            return DbSet.Where(i => i.ByAdmin == true).Count();
        }
        public int CountOfNotAccept()
        {
            return DbSet.Where(i => i.ByAdmin == false).Count();
        }
    }
}
