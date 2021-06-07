using BL.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DayShiftDTO
{
    public class CreateDayShiftDTO
    {
        public int Id { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public int MaxNumOfReservation { get; set; }
        public int WorkingDayId { get; set; }
    }
}
