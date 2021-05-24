using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AreaDTO
{
    public class GetAreaDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool ByAdmin { get; set; }
        public int CityID { get; set; }
    }
}
