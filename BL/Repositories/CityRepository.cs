using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class CityRepository: BaseRepository<City>
    {
        public CityRepository(DbContext dbcontext):base(dbcontext)
        {
        }
        
        public bool CheckExixt(City city)
        {
            return GetAny(c => c.ID == city.ID);
        }
    }
}
