using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationUserIdentity : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsDoctor { get; set; }
        public Doctor Doctor { get; set; }
        public List<Reservation> reservations { get; set; }
        public List<ReserveOffer> ReserveOffer { get; set; }
        public List<Rating> Rates { get; set; }
        public List<OfferRating> OfferRating { get; set; }
    }
    public class ApplicationUserStore : UserStore<ApplicationUserIdentity>
    {
        public ApplicationUserStore() : base(new VezeetaContext())
        {

        }
        public ApplicationUserStore(DbContext db) : base(db)
        {

        }
    }

}
