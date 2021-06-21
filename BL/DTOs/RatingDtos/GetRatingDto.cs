using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.RatingDtos
{
   public class GetRatingDto
    {
        public int ReservationId { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string DoctorId { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }

    }
}
