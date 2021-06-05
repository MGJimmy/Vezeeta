using BL.DTOs.DayShiftDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.WorkingDayDTO
{
    public class GetWorkingDayDTO
    {
        public GetWorkingDayDTO()
        {
            DayShifts = new List<CreateDayShiftDTO>();
        }
        public int Id { get; set; }
        public string ClinicId { get; set; }
        public Day Day { get; set; }
        public ICollection<CreateDayShiftDTO> DayShifts { get; set; }
    }
}
