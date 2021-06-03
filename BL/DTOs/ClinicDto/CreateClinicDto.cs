using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ClinicDto
{
    public class CreateClinicDto
    {
        
        public string DoctorId { get; set; }
        public string Street { get; set; }
        public int Fees { get; set; }
        //[MinLength(5)]
        public int ExaminationTime { get; set; }
        //[MinLength(0)]
        public int WatingTime { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
    }
}
