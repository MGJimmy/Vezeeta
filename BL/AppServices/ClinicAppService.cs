using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.DTOs.ClinicDto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class ClinicAppService : BaseAppService
    {
        public ClinicAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }

        public GetClinicDto Get(int id)
        {
            return Mapper.Map<GetClinicDto>(TheUnitOfWork.ClinicRepo.GetById(id));
        }
        public GetClinicDto GetByStringId(string doctorID)
        {
            return Mapper.Map<GetClinicDto>(TheUnitOfWork.ClinicRepo.GetFirstOrDefault(c=>c.DoctorId== doctorID));
        }

        public CreateClinicDto Insert(CreateClinicDto clinicDTO,string DoctorId)
        {
            if (clinicDTO == null)
                throw new ArgumentNullException();

            clinicDTO.DoctorId = DoctorId;
            Clinic clinic = Mapper.Map<Clinic>(clinicDTO);
            
            TheUnitOfWork.ClinicRepo.Insert(clinic);
            TheUnitOfWork.SaveChanges();
            clinicDTO.DoctorId = clinic.DoctorId;
            return clinicDTO;
        }
        public bool Update(UpdateClinicDto clinicDto,string docID)
        {
            if (clinicDto == null)
                throw new ArgumentNullException();

            bool result = false;
            //clinicDto.DoctorId = docID;
            Clinic clinic = Mapper.Map<Clinic>(clinicDto);
            TheUnitOfWork.ClinicRepo.Update(clinic);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        //public void AddClinicServicesToClinic(string doctorId, IEnumerable<ClinicServiceDto> clinicServiceDtos)
        //{
        //    var clinic = TheUnitOfWork.ClinicRepo.GetByStringId(doctorId);
        //    var clinicServices = Mapper.Map<IEnumerable<Clinicservice>>(clinicServiceDtos);
        //    foreach (var clinicService in clinicServices)
        //    {
        //        var dbclinicService = TheUnitOfWork.ClincServicesRepo.GetById(clinicService.ID);
        //        clinic.ClinicServices.Add(dbclinicService);
        //    }
        //    TheUnitOfWork.SaveChanges();

        //}

    }
}
