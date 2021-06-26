using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OfferRating
    {
        [Key, ForeignKey("ReserveOffer"), Required]
        public int ReserveOfferId { get; set; }

        public string Comment { get; set; }

        [ForeignKey("User"), Required]
        public string UserId { get; set; }


        [ForeignKey("MakeOffer"), Required]
        public int MakeOfferId { get; set; }

        [Required, Range(1, 5)]
        public int Rate { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ApplicationUserIdentity User { get; set; }
        public MakeOffer MakeOffer { get; set; }
        public ReserveOffer ReserveOffer { get; set; }
    }
}
