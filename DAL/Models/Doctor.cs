using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [ForeignKey("User"),Key]
        public string UserId { get; set; }
        public string Image { get; set; }
        public string TitleDegree { get; set; }
        public string doctorInfo { get; set; }
        public bool IsAccepted { get; set; }
        public ApplicationUserIdentity User { get; set; }
        public DoctorAttachment DoctorAttachment { get; set; }
        public List<DoctorService> doctorServices { get; set; }

    }
}
