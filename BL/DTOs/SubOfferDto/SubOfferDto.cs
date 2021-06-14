using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.SubOfferDto
{
    public class SubOfferDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int OfferId { get; set; }

    }
}
