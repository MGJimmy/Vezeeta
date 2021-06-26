using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.OfferRatingDTO
{
    public class CreateOfferRatingDto
    {
        public int ReserveOfferId { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public int MakeOfferId { get; set; }

        [Required, Range(1, 5)]
        public int Rate { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
