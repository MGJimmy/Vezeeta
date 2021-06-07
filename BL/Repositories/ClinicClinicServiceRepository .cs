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
    public class ClinicClinicServiceRepository : BaseRepository<ClinicClinicService>
    {
        public ClinicClinicServiceRepository(DbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
