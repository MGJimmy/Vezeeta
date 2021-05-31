using AutoMapper;
using BL.Bases;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs;
using DAL.Models;

namespace BL.AppServices
{
    public class DoctorAttachmentAppService:BaseAppService
    {
        public DoctorAttachmentAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public IEnumerable<DoctorAttachmentDto> GetAll()
        {
            return Mapper.Map<IEnumerable<DoctorAttachmentDto>>(TheUnitOfWork.DoctorAttachmentRepo.GetAll());
        }
        public IEnumerable<DoctorAttachmentDto> GetDoctorAttachment(bool isAccepted)
        {
            var t= Mapper.Map<IEnumerable<DoctorAttachmentDto>>(TheUnitOfWork.DoctorAttachmentRepo.GetDoctorAttachment(isAccepted));
            return t;
        }

        public int CountEntity()
        {
            return TheUnitOfWork.DoctorAttachmentRepo.CountEntity();
        }

        public object GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<DoctorAttachmentDto>>(TheUnitOfWork.DoctorAttachmentRepo.GetPageRecords(pageSize,pageNumber));

        }
        public void changeBindingStatus(string doctorId)
        {
           
            TheUnitOfWork.DoctorAttachmentRepo.changeBindingStatus(doctorId);
            TheUnitOfWork.SaveChanges();
           
        }
        public DoctorAttachmentDto Insert(DoctorAttachmentDto doctorDto)
        {
            if (doctorDto == null)
                throw new ArgumentNullException();
            DoctorAttachment doctor = Mapper.Map<DoctorAttachment>(doctorDto);
            TheUnitOfWork.DoctorAttachmentRepo.Insert(doctor);
            TheUnitOfWork.SaveChanges();
            doctorDto.DoctorId = doctor.DoctorId;
            return doctorDto;
        }
    }
}
