using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorSubSpecialization
{
    public class GetDoctorSubSpecialtyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ByAdmin { get; set; }
        public int SpecialtyId { get; set; }
    }
}
