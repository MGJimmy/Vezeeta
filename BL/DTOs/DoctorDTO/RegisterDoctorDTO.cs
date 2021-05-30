using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class RegisterDoctorDTO
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "Username is required"), Compare("PasswordHash",ErrorMessage ="Mis match confirm password")]
        public string ConfirmPassword { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string TitleDegree { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string doctorInfo { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDoctor { get; set; }
    }
}
