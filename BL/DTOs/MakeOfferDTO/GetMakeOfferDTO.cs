using BL.DTOs.MakeOfferImageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.MakeOfferDTO
{
    public class GetMakeOfferDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberOfSession { get; set; }
        public double Fees { get; set; }
        public double Discount { get; set; }
        public string Details { get; set; }
        public string Information { get; set; }
        public bool State { get; set; }
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public int SubOfferId { get; set; }
        public string DoctorId { get; set; }

        public List<GetMakeOfferImageDTO> OfferImages { get; set; }
    }
}
