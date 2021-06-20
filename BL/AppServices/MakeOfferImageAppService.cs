using AutoMapper;
using BL.Bases;
using BL.DTOs.MakeOfferImageDTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class MakeOfferImageAppService: BaseAppService
    {
        public MakeOfferImageAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public void CreateList(List<CreateMakeOfferImageDTO> dto)
        {
            var images = Mapper.Map<List<MakeOfferImage>>(dto);
            TheUnitOfWork.MakeOfferImageRepo.InsertList(images);
        } 


    }
}
