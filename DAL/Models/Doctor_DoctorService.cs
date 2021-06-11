using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Doctor_DoctorService
    {

        [ForeignKey("doctor")]
        public string doctorID { get; set; }
        public Doctor doctor { get; set; }

        [ForeignKey("service")]
        public int serviceID { get; set; }
        public DoctorService service { get; set; }
    }
}
