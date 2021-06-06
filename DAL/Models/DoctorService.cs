﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("DoctorService")]
    public class DoctorService
    {
        public int ID { get; set; }

        [Required, MinLength(3, ErrorMessage = "Name should be more than 2 character")]
        public string Name { get; set; }
        public bool ByAdmin { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
