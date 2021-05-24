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
    public class ClincServicesRepositry : BaseRepository<Clinicservice>
    {
        public ClincServicesRepositry(DbContext dbcontext) : base(dbcontext)
        {
        }

        public bool CheckExixt(Clinicservice clinicservice)
        {
            return GetAny(cs => cs.ID == clinicservice.ID);
        }
    }
}
