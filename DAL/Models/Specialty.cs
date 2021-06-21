using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Specialty")]
    public class Specialty
    {
        public int ID { get; set; }

        [Required, MinLength(3, ErrorMessage = "name should be at least 3 characters")]

        public string Name { get; set; }
        public string Image { get; set; }

        public List<Doctor> doctors{ get; set; }
    }
}
