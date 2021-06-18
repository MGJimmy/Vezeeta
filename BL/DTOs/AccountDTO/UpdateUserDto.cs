using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AccountDTO
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Name should not be empty")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Name should not be empty"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter phone number")]
        public string PhoneNumber { get; set; }

        ////////////////////////////////////////////////////////////////
        public string TitleDgree { get; set; }

        public string DoctorInfo { get; set; }

        public int? SpecialtyId { get; set; }

       public bool? isDoctor { get; set; }

    }
}
