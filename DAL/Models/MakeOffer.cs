using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MakeOffer
    {
        public MakeOffer()
        {
            OfferImages = new List<MakeOfferImage>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberOfSession { get; set; }
        public double Fees { get; set; }
        public double Discount { get; set; }
        public string Details { get; set; }
        public string Information { get; set; }
        public bool State { get; set; }

        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        [ForeignKey("SubOffer")]
        public int SubOfferId { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }

        public Offer Offer { get; set; }
        public SubOffer SubOffer { get; set; }
        public Doctor Doctor { get; set; }
        public List<MakeOfferImage> OfferImages { get; set; }
        public List<ReserveOffer> ReserveOffer { get; set; }
        public List<OfferRating> OfferRating { get; set; }



        public double SumOfRating { get; set; } = 0;
        public double CountOfRating { get; set; } = 0;
        public double AverageRate { get; set; } = 0;

    }
}
