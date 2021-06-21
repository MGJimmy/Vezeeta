using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    //public class FilterDoctorDto
    //{
    //    public int specailtyid { get; set; }
    //    public List<string> title { get; set; }
    //    public List<feelimit> fee { get; set; }
    //    public List<string> subspecails { get; set; }

    //}

    public class FilterDoctorDto
    {
        public int? specailtyid { get; set; }
        public int? CityId { get; set; }
        public int? AreaId { get; set; }
        public string Name { get; set; }
        public List<string> title { get; set; }
        public List<feelimit> fee { get; set; }
        public List<int> subspecails { get; set; }

    }

    public class feelimit
    {
        public int MiniMoney { get; set; }
        public int MaxMoney { get; set; }

        public override bool Equals(object obj)
        {
            var fee = (feelimit)obj;
            return fee.MiniMoney >= this.MiniMoney && fee.MaxMoney < this.MaxMoney;
        }

    } 
}
