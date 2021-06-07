﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ClinicClinicService
    {
        [ForeignKey("Clinic")]
        public string ClinicId { get; set; }
        public Clinic Clinic { get; set; }
        
        [ForeignKey("ClinicService")]
        public int ClinicServiceId { get; set; }
        public Clinicservice ClinicService { get; set; }
    }
}
