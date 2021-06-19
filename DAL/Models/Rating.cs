using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class Rating
    {
       
        [Key, ForeignKey("Reservation"), Required]
        public int ReservationId { get; set; }

        public string Comment { get; set; }

        [ForeignKey("User"),Required]
        public string UserId { get; set; }


        [ForeignKey("Doctor"), Required]
        public string DoctorId { get; set; }
        
        [Required,Range(1,5)]
        public int Rate { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ApplicationUserIdentity User { get; set; }
        public Doctor Doctor { get; set; }
        public Reservation Reservation { get; set; }

    }
}
