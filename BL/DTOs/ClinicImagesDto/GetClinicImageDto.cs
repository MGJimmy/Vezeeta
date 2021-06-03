using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.ClinicImagesDto
{
    public class GetClinicImageDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ClinicId { get; set; }
    }
}
