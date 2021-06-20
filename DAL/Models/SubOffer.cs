using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SubOffer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Offer")]
        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public List<MakeOffer> MakeOffers { get; set; }
    }
}
