using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ReserveOfferDTO
{
    public class CreateReserveOfferDTO
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int dayShiftId { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }
        public string DoctorId { get; set; }
        public int MakeOfferId { get; set; }
    }
}
