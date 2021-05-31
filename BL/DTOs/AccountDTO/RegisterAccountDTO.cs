using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AccountDTO
{
    public class RegisterAccountDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "Username is required"), Compare("PasswordHash", ErrorMessage = "Mis match confirm password")]
        public string ConfirmPassword { get; set; }
        public bool IsDoctor { get; set; }

    }
}
