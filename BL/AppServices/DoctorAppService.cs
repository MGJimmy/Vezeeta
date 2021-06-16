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

        public GetDoctorForReservationDto GetByName(string name)
        {
            var doctorDTO = Mapper.Map<GetDoctorForReservationDto>(TheUnitOfWork.DoctorRepo.GetByName(name));
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

        public List<GetDoctorDto> GetAllDoctorWhere(int SpecailtyId)
        {
            List<GetDoctorDto> doctors = Mapper.Map<List<GetDoctorDto>>(TheUnitOfWork.DoctorRepo.Get_All_Doctors_Where(d => d.specialtyId == SpecailtyId && d.IsAccepted == true).ToList());
            return doctors;
        }
        public GetDoctorDto GetDoctorDetails(string Doctor_ID)
        {
            GetDoctorDto doctor = Mapper.Map<GetDoctorDto>(TheUnitOfWork.DoctorRepo.GetDoctorDetailswithID(Doctor_ID));
            return doctor;
        }

        public List<GetDoctorDto> filterDoctors(FilterDoctorDto filterdoctorDto)
        {
            //if (filterdoctorDto.title.Count > 0)
            //{
            //    List<GetDoctorDto> doctors = Mapper.Map<List<GetDoctorDto>>(TheUnitOfWork.DoctorRepo.Get_All_Doctors_Where(d => d.specialtyId == filterdoctorDto.specailtyid && d.IsAccepted == true && filterdoctorDto.title.Contains(d.TitleDegree))).ToList();//10 3 


            //    return doctors;
            //}
            ////title fee subspecail
            //else
            //{
            //    List<GetDoctorDto> doctors = Mapper.Map<List<GetDoctorDto>>(TheUnitOfWork.DoctorRepo.Get_All_Doctors_Where(d => d.specialtyId == filterdoctorDto.specailtyid && d.IsAccepted == true)).ToList();
            //    return doctors;
            //}
            //return doctors;

            /************************************************************/
            List<int> filt = new List<int>();
            filterdoctorDto.subspecails.ForEach(element =>
            {
                filt.Add(TheUnitOfWork.SupSpecializationRepo.GetWhere(i => i.Name == element).Select(i=>i.ID).FirstOrDefault());
            });
            
        
            var doctors = TheUnitOfWork.DoctorRepo.Get_All_Doctors_Where(d => d.specialtyId == filterdoctorDto.specailtyid && d.IsAccepted == true && 
            
            (filterdoctorDto.title.Count>0? filterdoctorDto.title.Contains(d.TitleDegree) : true)
            ).ToList();


            var doct = doctors.Where(d =>
                  (filterdoctorDto.fee.Count > 0 ? filterdoctorDto.fee.Contains(new feelimit { MiniMoney = d.clinic.Fees, MaxMoney = d.clinic.Fees }) : true)
                  &&
                  (filterdoctorDto.subspecails.Count>0? filt.Any(i=>d.DoctorSubSpecialization.Any(dsup=>dsup.subSpecializeId==i)) :true)
            ).ToList();


            //var x = (filterdoctorDto.fee.Count > 0 ? filterdoctorDto.fee.Contains(new feelimit { MiniMoney = 51, MaxMoney = 80 }) : true);

            return Mapper.Map<List<GetDoctorDto>>(doct);
        }

    }
}
