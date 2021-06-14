using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class Doctor_DoctorServiceRepository: BaseRepository<Doctor_DoctorService>
    {
        public Doctor_DoctorServiceRepository(DbContext dbcontext) : base(dbcontext)
        {
        }

        public virtual IEnumerable<Doctor_DoctorService> GetAllWherewithService(Expression<Func<Doctor_DoctorService, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Doctor_DoctorService> query = DbSet;
                    
            if (filter != null)
            {
                query = query.Where(filter).Include(ds => ds.service);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query;
        }
       
    }
}
