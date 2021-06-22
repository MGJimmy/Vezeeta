using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("DoctorAttachment")]
    public class DoctorAttachment
    {
        [Key,ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        [Required]
        public string PersonalIdImage { get; set; }
        [Required]
        public string DoctorSyndicateIdImage { get; set; }
        [Required]
        public string OpenClinicPermissionImage { get; set; }
        public bool Rejected { get; set; } = false;
        public bool isBinding { get; set; }
        public Doctor Doctor { get; set; }
    }
}
