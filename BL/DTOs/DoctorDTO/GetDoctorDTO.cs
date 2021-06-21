using BL.DTOs.ClinicDto;
using BL.DTOs.ClinicImagesDto;
using BL.DTOs.Doctor_DoctorServiceDto;
using BL.DTOs.DoctorServiceDtos;
using BL.DTOs.DoctorSubSpecialization;
using BL.DTOs.UserDto;
using BL.DTOs.WorkingDayDTO;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.DoctorDTO
{
    public class GetDoctorDto
    {
        public string UserId { get; set; }
        public string Image { get; set; }
        public string TitleDegree { get; set; }
        public string doctorInfo { get; set; }
        public bool IsAccepted { get; set; }
        public int specialtyId { get; set; }
        public GetUserDto User { get; set; }
        public SpecialtyDTO specialty { get; set; }

        //public List<GetDoctor_DoctorService> doctor_doctorServices { get; set; }
        //public List<CreateDoctorSubSpecializationDTO> doctor_SubSpecialization { get; set; }

        public IEnumerable<DoctorServiceDto> services { get; set; }
        public IEnumerable<GetDoctorSubSpecialtyDTO> subspecails { get; set; }

        public GetClinicDto clinic { get; set; }

        public string clinicCityName { get; set; }
        public string clinicAreaName { get; set; }
        public List<GetWorkingDayDTO> workingDays { get; set; }
        public List<GetClinicImageDto> Clinic_Images { get; set; }

        public double averageRate { get; set; }
        

    }
}
