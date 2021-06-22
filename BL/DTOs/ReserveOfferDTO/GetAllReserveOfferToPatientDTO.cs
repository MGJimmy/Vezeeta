using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ReserveOfferDTO
{
    public class GetAllReserveOfferToPatientDTO
    {
        public int ReserveOfferId { get; set; }
        public string MakeOfferTitle { get; set; }
        public string DoctorName { get; set; }
        public string ClinicArea { get; set; }
        public string ClinicStreeet { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }
        public bool IsRated { get; set; }

        public TimeSpan DayShiftFrom { get; set; }
        public TimeSpan DayShiftTo { get; set; }
    }
}
