using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.RatingDtos
{
    public class GetRatingWithAverageRateDto
    {
        public List<GetRatingDto> GetRatingDtos { get; set; }

        public double AverageRate { get; set; } 
    }
}
