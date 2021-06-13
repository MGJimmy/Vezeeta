using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ReversationDto
{
    public class CreateReservationDTO
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int dayShiftId { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }
        public System.Nullable<int> Age { get; set; } = null;
        public System.Nullable<Gender> gender { get; set; } = null;
        public string Symptoms { get; set; } = null;
        public string DoctorId { get; set; }
    }
}
