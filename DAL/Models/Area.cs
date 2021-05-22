using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Area
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int CityID { get; set; }

        public City City { get; set; } 
    }
}
