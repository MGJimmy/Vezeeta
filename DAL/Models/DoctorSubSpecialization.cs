using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DoctorSubSpecialization
    {
        [ForeignKey("doctor"),Required]
        public string DoctorId { get; set; }

        [ForeignKey("supSpecialization")]
        public int subSpecializeId { get; set; }
        public Doctor doctor { get; set; }
        public SupSpecialization supSpecialization { get; set; }
    }
}
