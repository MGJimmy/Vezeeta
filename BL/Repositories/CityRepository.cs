using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using BL.DTOs;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class CityRepository: BaseRepository<City>
    {
        public CityRepository(DbContext dbcontext):base(dbcontext)
        {
        }

        public bool CheckCityExistByName(string cityName)
        {
            return GetAny(c => c.Name == cityName);
        }
    }
}
