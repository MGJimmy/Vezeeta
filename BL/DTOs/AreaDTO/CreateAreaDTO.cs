using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AreaDTO
{
    public class CreateAreaDTO
    {
        public int ID { get; set; }

        [Required, MinLength(3, ErrorMessage = "Name should be more than 2 character")]
        public string Name { get; set; }
        public bool ByAdmin { get; set; }
        public int CityID { get; set; }
    }
}
