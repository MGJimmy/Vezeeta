using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.OfferDto
{
    public class OfferWithSubOfferDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<SubOffer2Dto> SubOffers { get; set; }

    }
    public class SubOffer2Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
