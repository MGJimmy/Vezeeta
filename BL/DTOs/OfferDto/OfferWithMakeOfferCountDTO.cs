using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.OfferDto
{
    public class OfferWithMakeOfferCountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int MakeOfferCount { get; set; }
    }
}
