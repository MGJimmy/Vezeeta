using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class DoctorAttachmentDto
    {
        public string DoctorId { get; set; }
        public string PersonalIdImage { get; set; }
        [Required]
        public string DoctorSyndicateIdImage { get; set; }
        [Required]
        public string OpenClinicPermissionImage { get; set; }
    }
}
