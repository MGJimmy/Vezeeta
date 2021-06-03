using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum Day
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }
    public class WorkingDay
    {
        public WorkingDay()
        {
            DayShifts = new List<DayShift>();
        }
        public int Id { get; set; }
        [ForeignKey("Clinic")]
        public string ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        [MinLength(0),MaxLength(6)]
        public Day Day { get; set; }
        public ICollection<DayShift> DayShifts { get; set; }
    }
}
