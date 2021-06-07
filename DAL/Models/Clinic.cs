using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Clinic")]
    public class Clinic
    {
        public Clinic()
        {
            ClinicImages = new List<ClinicImage>();
            ClinicClinicServices = new List<ClinicClinicService>();
            WorkingDays = new List<WorkingDay>();
        }
        [Key, ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public string Street { get; set; }
        public int Fees { get; set; }
        //[MinLength(5)]
        public int ExaminationTime { get; set; }
        //[MinLength(0)]
        public int WatingTime { get; set; }
        public Doctor Doctor { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }
        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public ICollection<ClinicImage> ClinicImages { get; set; }
        public ICollection<WorkingDay> WorkingDays { get; set; }
        public ICollection<ClinicClinicService> ClinicClinicServices { get; set; }
    }
}
