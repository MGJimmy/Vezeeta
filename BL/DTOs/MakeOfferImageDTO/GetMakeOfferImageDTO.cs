using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.MakeOfferImageDTO
{
    public class GetMakeOfferImageDTO
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int MakeOfferId { get; set; }
    }
}
