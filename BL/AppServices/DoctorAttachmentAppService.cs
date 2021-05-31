using AutoMapper;
using BL.Bases;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs;

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
        public void acceptAttachments(string doctorId)
        {
            TheUnitOfWork.DoctorAttachmentRepo.acceptAttchments(doctorId);
        }
    }
}
