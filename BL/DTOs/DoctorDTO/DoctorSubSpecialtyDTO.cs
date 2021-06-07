using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class DoctorSubSpecialtyDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool byAdmin { get; set; }
        public int specialtyId { get; set; }
    }
}
