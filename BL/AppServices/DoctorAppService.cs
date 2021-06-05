using AutoMapper;
using BL.Bases;
using BL.DTOs.DoctorDTO;
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
        public Doctor Create(string userId, CreateDoctorDTO createDoctorDTO)
        {
            Doctor doctor = Mapper.Map<Doctor>(createDoctorDTO);
            doctor.UserId = userId;
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

        public bool UpdateServicesList(List<DoctorSercive> _doctorservice, string docID)
        {
            if (_doctorservice == null)
                throw new ArgumentNullException();

            bool result = false;

            Doctor doctor=TheUnitOfWork.DoctorRepo.GetByStringId(docID);

            doctor.DoctorSercives = _doctorservice;
        
            TheUnitOfWork.DoctorRepo.Update(doctor);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;

        }
    }
}
