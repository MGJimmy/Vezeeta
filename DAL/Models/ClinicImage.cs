using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ClinicImage
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [ForeignKey("Clinic")]
        public string ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }
}
