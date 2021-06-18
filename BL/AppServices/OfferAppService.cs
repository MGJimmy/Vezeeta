using AutoMapper;
using BL.Bases;
using BL.DTOs.OfferDto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class OfferAppService:BaseAppService
    {
        public OfferAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }

        public IEnumerable<OfferDTO> GetAll()
        {
            var dto = Mapper.Map<IEnumerable<OfferDTO>>(TheUnitOfWork.OfferRepo.GetAll());
            return dto;
        }

        public IEnumerable<OfferWithSubOfferDTO> GetAllWithSubOffer()
        {
            var dto = Mapper.Map<IEnumerable<OfferWithSubOfferDTO>>(TheUnitOfWork.OfferRepo.GetAllWithSubOffer());
            return dto;
        }

        public IEnumerable<OfferWithMakeOfferCountDTO> GetAllWithCountOfMakeOfferRelated()
        {
            var offers = TheUnitOfWork.OfferRepo.GetAll().ToList();

            var offerDto = new List<OfferWithMakeOfferCountDTO>();


            offers.ForEach(element =>
            {
                var count = TheUnitOfWork.MakeOfferRepo.GetWhere(i => i.State == true && i.OfferId == element.Id).Count;
                var dto = new OfferWithMakeOfferCountDTO
                {
                    Id = element.Id,
                    Name = element.Name,
                    Image = element.Image,
                    MakeOfferCount = count
                };
                offerDto.Add(dto);
            });
            return offerDto;
        }

        public OfferDTO GetById(int id)
        {
            var dto = Mapper.Map<OfferDTO>(TheUnitOfWork.OfferRepo.GetById(id));
            return dto;
        }
        public OfferDTO Insert(OfferDTO createDto)
        {
            if (createDto == null)
                throw new ArgumentNullException();
            var offer = Mapper.Map<Offer>(createDto);
            var inserted = TheUnitOfWork.OfferRepo.Insert(offer);
            TheUnitOfWork.SaveChanges();
            return Mapper.Map<OfferDTO>(inserted);
        }

        public bool Update(OfferDTO offerDto)
        {
            if (offerDto == null)
                throw new ArgumentNullException();
            var result = false;
            var offer = Mapper.Map<Offer>(offerDto);
            TheUnitOfWork.OfferRepo.Update(offer);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public bool Delete(int id)
        {
            TheUnitOfWork.OfferRepo.Delete(id);
            return TheUnitOfWork.SaveChanges()>new int();
        }

        public IEnumerable<OfferDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<OfferDTO>>(TheUnitOfWork.OfferRepo.GetPageRecords(pageSize, pageNumber));
        }
        public int CountEntity()
        {
            return TheUnitOfWork.OfferRepo.CountEntity();
        }
    }
}
