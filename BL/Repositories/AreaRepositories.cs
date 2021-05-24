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
    public class AreaRepositories : BaseRepository<Area>
    {
        public AreaRepositories(DbContext dbContext) : base(dbContext)
        {
        }
        public bool CheckExistByName(Area area)
        {
            return GetAny(ar => ar.Name == area.Name);
        }
    }
}
