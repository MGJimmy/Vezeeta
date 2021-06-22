using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.OfferRatingDTO
{
    public class GetOfferRatingDto
    {
        public int ReserveOfferId { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public int MakeOfferId { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
