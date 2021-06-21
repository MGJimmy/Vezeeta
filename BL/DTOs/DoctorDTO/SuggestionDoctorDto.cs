using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class SuggestionDoctorDto
    {
        public string Image { get; set; }
        public string UserFullName { get; set; }
        public string SpecialtyName { get; set; }
        public string TitleDegree { get; set; }
        public double AverageRate { get; set; }
        public string clinicCityName { get; set; }

        public string UserId { get; set; }
    }
}
