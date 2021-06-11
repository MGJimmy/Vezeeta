using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class GetDoctorWithClinicForReservetionCartDTO
    {
        public string UserFullName { get; set; }
        public int ClinicFees { get; set; }
        public int ClinicPhone { get; set; }

        public int WatingTime { get; set; }
        public string AreaName { get; set; }

    }
}
