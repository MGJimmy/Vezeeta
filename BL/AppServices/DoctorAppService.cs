using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.DTOs.DoctorDTO;
using BL.DTOs.DoctorServiceDtos;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class DoctorAppService:BaseAppService
    {
        public DoctorAppService(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork,mapper)
        {
        }
        public Doctor GetById(string id)
        {
            Doctor doctor = TheUnitOfWork.DoctorRepo.GetById(id);
            return doctor;
        }

        public GetDoctorDTO GetByName(string name)
        {
            GetDoctorDTO doctorDTO = Mapper.Map<GetDoctorDTO>(TheUnitOfWork.DoctorRepo.GetByName(name));
            return doctorDTO;
        }

        public GetDoctorWithClinicForReservetionCartDTO GetWithClinicForReservetionCart(string name)
        {
            Doctor doctor = TheUnitOfWork.DoctorRepo.GetWithClinicDetails(name);
            Clinic clinic = TheUnitOfWork.ClinicRepo.GetByStringId(doctor.UserId);
            Area area = TheUnitOfWork.AreaRepo.GetById(clinic.AreaId);
            GetDoctorWithClinicForReservetionCartDTO dto = new GetDoctorWithClinicForReservetionCartDTO();

            dto.UserFullName = doctor.User.UserName;
            dto.ClinicFees = clinic.Fees;
            dto.AreaName = area.Name;
            dto.WatingTime = clinic.WatingTime;
            //dto.ClinicPhone = clinic.phone;

            return dto;
        }


        public Doctor Create(string userId, CreateDoctorDTO createDoctorDTO)
        {
            Doctor doctor = Mapper.Map<Doctor>(createDoctorDTO);
            doctor.UserId = userId;
            doctor.specialtyId = 1;
            doctor.IsAccepted = false;
            var createdDoctor = TheUnitOfWork.DoctorRepo.Insert(doctor);
            TheUnitOfWork.SaveChanges();
            return createdDoctor;
        }
        public void activateDoctor(string doctorId)
        {
            TheUnitOfWork.DoctorRepo.activateDoctor(doctorId);
            TheUnitOfWork.SaveChanges();
        }
        public void deactivateDoctor(string doctorId)
        {
            TheUnitOfWork.DoctorRepo.deactivateDoctor(doctorId);
            TheUnitOfWork.SaveChanges();
        }
        //public void InsertSpecialtyToDoctor(string doctorId, SpecialtyDTO speiatyDto)
        //{
        //    var specialty = Mapper.Map<Specialty>(speiatyDto);
        //    TheUnitOfWork.DoctorRepo.InsertSpecialtyToDoctor(doctorId, specialty);
        //    TheUnitOfWork.SaveChanges();
        //}
        //public void InsertSubSpecialtyToDoctor(string doctorId, List<SupSpecailization> subSpeiatyDto)
        //{
        //    List<SupSpecialization> subSpecializations = Mapper.Map<List<SupSpecialization>>(subSpeiatyDto);
        //    TheUnitOfWork.DoctorRepo.InsertSubSpecialtyToDoctor(doctorId,subSpecializations);
        //    TheUnitOfWork.SaveChanges();
        //}
        //public void EmptySubSpecialtyInDoctor(string doctorId)
        //{
        //    TheUnitOfWork.DoctorRepo.EmptySubSpecialtyInDoctor(doctorId);
        //    TheUnitOfWork.SaveChanges();
        //}
    }
}
