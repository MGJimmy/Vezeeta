using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Area")]
    public class Area
    {
        public int ID { get; set; }

        [Required,MinLength(3,ErrorMessage ="Name should be more than 2 character")]
        public string Name { get; set; }
        public bool ByAdmin { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }

        public City City { get; set; }

        public List<Clinic> clinics { get; set; }
    }
}
