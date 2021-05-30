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

        public IEnumerable<DoctorAttachmentDto> GetNotAcceptedDoctorAttachment()
        {
            return Mapper.Map<IEnumerable<DoctorAttachmentDto>>(TheUnitOfWork.DoctorAttachmentRepo.GetNotAcceptedDoctorAttachment());
        }
    }
}
