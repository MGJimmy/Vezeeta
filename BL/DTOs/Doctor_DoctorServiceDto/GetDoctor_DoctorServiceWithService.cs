using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Doctor_DoctorServiceDto
{
   public class GetDoctor_DoctorServiceWithService
    {
        public string doctorID { get; set; }
        public int serviceID { get; set; }
        public DoctorService service { get; set; }
    }
}
