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
        [ForeignKey("specialty")]
        public int specialtyId { get; set; }

        public ApplicationUserIdentity User { get; set; }
        public DoctorAttachment DoctorAttachment { get; set; }
        public Specialty specialty { get; set; }
        
        public List<Doctor_DoctorService> doctor_doctorServices { get; set; }
        public List<DoctorSubSpecialization> DoctorSubSpecialization { get; set; }
        public List<DoctorService> doctorServices { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<ReserveOffer> ReserveOffer { get; set; }


        public Clinic Clinic { get; set;  }

    }
}
