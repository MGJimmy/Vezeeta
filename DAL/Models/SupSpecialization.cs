using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SupSpecialization
    {
        public int ID { get; set; }

        [Required, MinLength(3, ErrorMessage = "name should be at least 3 characters")]
        public string Name { get; set; }
        public bool ByAdmin { get; set; }


        [ForeignKey("specialty")]
         public int specialtyId { get; set; }
         public Specialty specialty { get; set; }
        public List<DoctorSubSpecialization> DoctorSubSpecialization { get; set; }
    }
}
