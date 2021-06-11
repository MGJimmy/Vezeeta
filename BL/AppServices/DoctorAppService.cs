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
        //public List<DoctorSubSpecialtyDTO> GetSubSpecialtyByDoctorId(string id)
        //{
        //    Doctor doctor = TheUnitOfWork.DoctorRepo.GetSubSpecialtyByDoctorId(id);
        //    List<DoctorSubSpecialtyDTO> doctorSubSpecialty = Mapper.Map<List<DoctorSubSpecialtyDTO>>(doctor);
        //    return doctorSubSpecialty;
        //}
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

        /// 
        public List<GetDoctorDto> GetAllDoctorWhere(int SpecailtyId)
        {
            List<GetDoctorDto> doctors =Mapper.Map<List<GetDoctorDto>>( TheUnitOfWork.DoctorRepo.Get_All_Doctors_Where(d=>d.specialtyId == SpecailtyId && d.IsAccepted==true).ToList()); 
            return doctors;
        }

    }
}
