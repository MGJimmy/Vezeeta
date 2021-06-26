using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [ForeignKey("dayShift")]
        public int dayShiftId { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }

        [ForeignKey("Doctor")]
        public string doctorId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public System.Nullable<Gender> gender { get; set; }
        public string Symptoms { get; set; }
        public bool State { get; set; }
        public bool IsRated { get; set; } = false;



        public DayShift dayShift { get; set; }
        public ApplicationUserIdentity User { get; set; }
        public Doctor Doctor { get; set; }
        public Rating Rate { get; set; }




    }
    public enum Gender
    {
        Male,
        Female
    }
}
