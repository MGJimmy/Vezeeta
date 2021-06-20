using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MakeOfferImage
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [ForeignKey("MakeOffer")]
        public int MakeOfferId { get; set; }
        public MakeOffer MakeOffer { get; set; }
    }
}
