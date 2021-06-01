using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ClinicDto
{
    public class GetClinicDto
    {
        public string DoctorId { get; set; }
        public string Street { get; set; }
        public int Fees { get; set; }
        public int ExaminationTime { get; set; }
        public int WatingTime { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
    }
}
