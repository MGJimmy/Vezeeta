using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class City
    {
        public City()
        {
            Areas = new List<Area>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Area> Areas { get; set; }  
    }
    
}
