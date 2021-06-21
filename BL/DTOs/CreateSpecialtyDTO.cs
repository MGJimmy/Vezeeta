using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CreateSpecialtyDTO
    {
        public int ID { get; set; }
        [Required, MinLength(3, ErrorMessage = "name should be at least 3 characters")]

        public string Name { get; set; }
        public string Image { get; set; }
    }
}
