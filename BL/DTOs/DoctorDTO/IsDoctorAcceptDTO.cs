using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class IsDoctorAcceptDTO
    {
        public bool AcceptState { get; set; }
        public List<string> ErrorDetails { get; set; }
    }
}
