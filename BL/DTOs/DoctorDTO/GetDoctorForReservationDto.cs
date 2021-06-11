using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class GetDoctorForReservationDto
    {
        public string Image { get; set; }
        public string TitleDegree { get; set; }
        public string doctorInfo { get; set; }
        public string UserFullName { get; set; }
        public string UserUserName { get; set; }
        public string UserId { get; set; }
    }
}
