using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ClinicClinicServiceDTO
{
    public class GetClinicClinicServiceDTO
    {
        public string ClinicId { get; set; }
        public int ClinicServiceId { get; set; }
        public ClinicServiceDto ClinicService { get; set; }
    }
}
