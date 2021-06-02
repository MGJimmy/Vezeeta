using BL.DTOs.DayShiftDTO;
using DAL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.WorkingDayDTO
{
    public class CreateWorkingDayDTO
    {
        public CreateWorkingDayDTO()
        {
            DayShifts = new List<CreateDayShiftDTO>();
        }
        public int Id { get; set; }
        public string ClinicId { get; set; }
        public Day Day { get; set; }
        public ICollection<CreateDayShiftDTO> DayShifts { get; set; }
    }
}
