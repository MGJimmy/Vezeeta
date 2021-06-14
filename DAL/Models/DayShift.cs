using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DayShift
    {
        public int Id { get; set; }
        public TimeSpan From { get; set; } 
        public TimeSpan To { get; set; }
        public int MaxNumOfReservation { get; set; }

        [ForeignKey("WorkingDay")]
        public int WorkingDayId { get; set; }
        public WorkingDay WorkingDay { get; set; }

        public List<Reservation> reservations { get; set; }
        public List<ReserveOffer> reserveOffer { get; set; }
    }
}
