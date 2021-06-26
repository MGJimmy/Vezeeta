using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.OfferRatingDTO
{
    public class GetOfferRatingWithAverageRateDto
    {
        public List<GetOfferRatingDto> GetOfferRatingDtos { get; set; }

        public double AverageRate { get; set; }
    }
}
