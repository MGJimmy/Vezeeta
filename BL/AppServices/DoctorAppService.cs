﻿using AutoMapper;
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
        public List<DoctorSubSpecialtyDTO> GetSubSpecialtyByDoctorId(string id)
        {
            Doctor doctor = TheUnitOfWork.DoctorRepo.GetSubSpecialtyByDoctorId(id);
            List<DoctorSubSpecialtyDTO> doctorSubSpecialty = Mapper.Map<List<DoctorSubSpecialtyDTO>>(doctor);
            return doctorSubSpecialty;
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

        public bool UpdateServicesList(List<DoctorServiceDto> _doctorservice, string docID)
        {
            if (_doctorservice == null)
                throw new ArgumentNullException();

            bool result = false;

            Doctor doctor=TheUnitOfWork.DoctorRepo.GetByStringId(docID);
            doctor.doctorServices=Mapper.Map<List<DoctorService>>(_doctorservice);
            TheUnitOfWork.DoctorRepo.Update(doctor);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;

        }
        public bool DeleteServiceList(string docID)
        {
            bool result = false;
            Doctor doctor = TheUnitOfWork.DoctorRepo.GetByStringId(docID);
            doctor.doctorServices = null;
            TheUnitOfWork.DoctorRepo.Update(doctor);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public IEnumerable<DoctorServiceDto> GetDoctorServices(string doctorId)
        {
            Doctor doctor = TheUnitOfWork.DoctorRepo.GetByStringId(doctorId);
            return Mapper.Map<IEnumerable<DoctorServiceDto>>(doctor.doctorServices);
        }
        public void InsertSpecialtyToDoctor(string doctorId, SpecialtyDTO speiatyDto)
        {
            var specialty = Mapper.Map<Specialty>(speiatyDto);
            TheUnitOfWork.DoctorRepo.InsertSpecialtyToDoctor(doctorId, specialty);
            TheUnitOfWork.SaveChanges();
        }
        public void InsertSubSpecialtyToDoctor(string doctorId, List<SupSpecailization> subSpeiatyDto)
        {
            List<SupSpecialization> subSpecializations = Mapper.Map<List<SupSpecialization>>(subSpeiatyDto);
            TheUnitOfWork.DoctorRepo.InsertSubSpecialtyToDoctor(doctorId,subSpecializations);
            TheUnitOfWork.SaveChanges();
        }
        public void EmptySubSpecialtyInDoctor(string doctorId)
        {
            TheUnitOfWork.DoctorRepo.EmptySubSpecialtyInDoctor(doctorId);
            TheUnitOfWork.SaveChanges();
        }
    }
}
