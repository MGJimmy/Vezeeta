using AutoMapper;
using BL.Bases;
using BL.DTOs.SubOfferDto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class SubOfferAppService:BaseAppService
    {
        public SubOfferAppService(IMapper mapper,IUnitOfWork unitOfWork):base(unitOfWork,mapper)
        {

        }
        public IEnumerable<SubOfferDto> GetAll()
        {
            var dto = Mapper.Map<IEnumerable<SubOfferDto>>(TheUnitOfWork.SubOfferRepo.GetAll());
            return dto;
        }
        public SubOfferDto GetById(int id)
        {
            var dto = Mapper.Map<SubOfferDto>(TheUnitOfWork.SubOfferRepo.GetById(id));
            return dto;
        }
        public SubOfferDto Insert(SubOfferDto createDto)
        {
            if (createDto == null)
                throw new ArgumentNullException();
            var subOffer = Mapper.Map<SubOffer>(createDto);
            var inserted = TheUnitOfWork.SubOfferRepo.Insert(subOffer);
            TheUnitOfWork.SaveChanges();
            return Mapper.Map<SubOfferDto>(inserted);
        }

        public bool Update(SubOfferDto subOfferDto)
        {
            if (subOfferDto == null)
                throw new ArgumentNullException();
            var result = false;
            var offer = Mapper.Map<SubOffer>(subOfferDto);
            TheUnitOfWork.SubOfferRepo.Update(offer);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public bool Delete(int id)
        {
            TheUnitOfWork.SubOfferRepo.Delete(id);
            return TheUnitOfWork.SaveChanges() > new int();
        }

        public IEnumerable<GetSubOfferWithOfferDto> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<GetSubOfferWithOfferDto>>(TheUnitOfWork.SubOfferRepo.GetPageRecords(pageSize, pageNumber));
        }
        public int CountEntity()
        {
            return TheUnitOfWork.SubOfferRepo.CountEntity();
        }
    }
}
