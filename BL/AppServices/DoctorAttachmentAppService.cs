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
        public DoctorAttachmentGetOneDtO GetById(string id)
        {
            return Mapper.Map<DoctorAttachmentGetOneDtO>(TheUnitOfWork.DoctorAttachmentRepo.GetById(id));
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
        public void changeBindingAndRejectedStatus(string doctorId,bool rejectState)
        {
           
            TheUnitOfWork.DoctorAttachmentRepo.changeBindingAndRejectedStatus(doctorId,rejectState);
            TheUnitOfWork.SaveChanges();
           
        }
        public DoctorAttachmentDto Insert(DoctorAttachmentDto doctorDto)
        {
            if (doctorDto == null)
                throw new ArgumentNullException();
            DoctorAttachment doctor = Mapper.Map<DoctorAttachment>(doctorDto);
            doctor.isBinding = true;
            TheUnitOfWork.DoctorAttachmentRepo.Insert(doctor);
            TheUnitOfWork.SaveChanges();
            doctorDto.DoctorId = doctor.DoctorId;
            return doctorDto;
        }
        
        public bool Update(DoctorAttachmentDto attachmentDto)
        {
            if (attachmentDto == null)
                throw new ArgumentNullException();
            bool result = false;
            DoctorAttachment doctorAttachment = Mapper.Map<DoctorAttachment>(attachmentDto);
            doctorAttachment.isBinding = true;
            TheUnitOfWork.DoctorAttachmentRepo.Update(doctorAttachment);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

    }
}
