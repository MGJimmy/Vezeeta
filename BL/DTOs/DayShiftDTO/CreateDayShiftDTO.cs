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
        //[System.Text.Json.Serialization.JsonConverterAttribute(typeof(CustomTimeSpanConverter))]
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public int WorkingDayId { get; set; }
    }
}
