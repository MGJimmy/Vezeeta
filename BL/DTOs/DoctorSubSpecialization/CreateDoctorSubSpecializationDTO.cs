using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorSubSpecialization
{
    public class CreateDoctorSubSpecializationDTO
    {
        [Required]
        public string DoctorId { get; set; }

        [Required]
        public int subSpecializeId { get; set; }
    }
}
