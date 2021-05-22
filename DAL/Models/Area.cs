using System;
using System.Collections.Generic;
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
        public string Name { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }

        public City City { get; set; } 
    }
}
