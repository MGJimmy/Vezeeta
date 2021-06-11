using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.SubOfferDto
{
    public class GetSubOfferWithOfferDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int OfferId { get; set; }
        public string OfferName { get; set; }

    }
}
