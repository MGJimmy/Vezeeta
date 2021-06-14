using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ReversationDto
{
    public class GetAllReservationToDoctorDTO
    {
        public int reservetionId { get; set; }
        public string UserId { get; set; }
        public bool State { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }

        public int? Age { get; set; }
        public Gender? gender { get; set; } 
        public string Symptoms { get; set; }

        public TimeSpan DayShiftFrom { get; set; }
        public TimeSpan DayShiftTo { get; set; }
        public int dayShiftId { get; set; }
    }
}
