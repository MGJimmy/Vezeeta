using BL.DTOs.AccountDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class CreateDoctorDTO:RegisterAccountDTO
    {
        public string Image { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string TitleDegree { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string DoctorInfo { get; set; }
        public bool IsAccepted { get; set; }
        public int specialtyId { get; set; }
    }
}
