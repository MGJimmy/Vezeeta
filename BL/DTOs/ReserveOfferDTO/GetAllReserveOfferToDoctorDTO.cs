using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ReserveOfferDTO
{
    public class GetAllReserveOfferToDoctorDTO
    {
        public int reservetionId { get; set; }
        public string MakeOfferTitle { get; set; }
        public int MakeOfferId { get; set; }
        public string UserId { get; set; }
        public bool State { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }


        public TimeSpan DayShiftFrom { get; set; }
        public TimeSpan DayShiftTo { get; set; }
        public int dayShiftId { get; set; }


    }
}
