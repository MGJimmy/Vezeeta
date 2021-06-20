using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
   public class DoctorSearchDto
    {
        public string UserId { get; set; }
        public string DoctorName { get; set; }
        public string TitleDegree { get; set; }
        public string specialtyName { get; set; }

      // public List<DoctorSubSpecialization> DoctorSubSpecialization { get; set; }

        public string CityName { get; set; }
        public string AreaName { get; set; }

        public string Image { get; set; }
        public string Street { get; set; }
        public int ExmantionTime { get; set; }

        public int Fees { get; set; }
        public int WatingTime { get; set; }

        public string doctorInfo { get; set; }
        public bool IsAccepted { get; set; }

        public int specialtyId { get; set; }

        public int CityId { get; set; }
        public int AreaId { get; set; }



    }
}
