using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Clinicservice
    {
        public int ID { get; set; }
        [Required , MinLength(5)]
        public string Name { get; set; }
        public bool ByAdmin { get; set; }
        public ICollection<ClinicClinicService> ClinicClinicServices { get; set; }
    }
}
