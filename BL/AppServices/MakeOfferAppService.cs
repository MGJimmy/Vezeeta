using AutoMapper;
using BL.Bases;
using BL.DTOs.MakeOfferDTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class MakeOfferAppService:BaseAppService
    {
        public MakeOfferAppService(IUnitOfWork unitOfWork,IMapper mapper):base(unitOfWork,mapper)
        {
        }
        public List<GetMakeOfferWithDoctorInfoDTO> GetAll()
        {
            var x = Mapper.Map<List<GetMakeOfferWithDoctorInfoDTO>>(TheUnitOfWork.MakeOfferRepo.GetAll().Where(i => i.State == true).OrderByDescending(i => i.Id));
            return x;
        }
        public List<GetMakeOfferWithDoctorInfoDTO> GetAllRelatedToOfferId(int id,int pageSize,int pageNumber)
        {
            var dto = Mapper.Map<List<GetMakeOfferWithDoctorInfoDTO>>(TheUnitOfWork.MakeOfferRepo.GetWherePaging(i => i.State == true && i.OfferId == id, pageSize, pageNumber));
            return dto;
        }

        public List<GetMakeOfferWithDoctorInfoDTO> GetAllRelatedToSubOfferId(int id, int pageSize, int pageNumber)
        {
            
            var dto = Mapper.Map<List<GetMakeOfferWithDoctorInfoDTO>>(TheUnitOfWork.MakeOfferRepo.GetWherePaging(i => i.State == true && i.SubOfferId == id,pageSize,pageNumber));
            return dto;
        }

        public List<GetMakeOfferDTO> GetAllByDoctorId(string id)
        {
            return Mapper.Map<List<GetMakeOfferDTO>>(TheUnitOfWork.MakeOfferRepo.GetAllByDoctorId(id));
        }
        public GetMakeOfferWithDoctorInfoDTO GetById(int id)
        {
            return Mapper.Map<GetMakeOfferWithDoctorInfoDTO>(TheUnitOfWork.MakeOfferRepo.GetById(id));
        }

        public CreateMakeOfferDTO create(string doctorId, CreateMakeOfferDTO offerDTO)
        {
            offerDTO.DoctorId = doctorId;
            var makeOffer = Mapper.Map<MakeOffer>(offerDTO);
            var inserted = TheUnitOfWork.MakeOfferRepo.Insert(makeOffer);
            TheUnitOfWork.SaveChanges();
            offerDTO.Id = inserted.Id;
            return offerDTO;
        }

        //public CreateMakeOfferDTO update(CreateMakeOfferDTO offerDTO)
        //{
        //    var makeOffer = Mapper.Map<MakeOffer>(offerDTO);
        //    TheUnitOfWork.MakeOfferRepo.Update(makeOffer);
        //    TheUnitOfWork.SaveChanges();
        //    return offerDTO;
        //}
        public CreateMakeOfferDTO update(CreateMakeOfferDTO offerDTO)
        {
            var offer = TheUnitOfWork.MakeOfferRepo.GetById(offerDTO.Id);
            offer.OfferImages = null;
           

            var makeOffer = Mapper.Map<MakeOffer>(offerDTO);
            offer.OfferImages = makeOffer.OfferImages;
            offer.Details = offerDTO.Details;
            offer.Discount = offerDTO.Discount;    
            offer.Fees = offerDTO.Fees;
            offer.Information = offerDTO.Information;
            offer.NumberOfSession = offerDTO.NumberOfSession;
            offer.State = offerDTO.State;
            offer.Title = offerDTO.Title;
            



            TheUnitOfWork.MakeOfferRepo.Update(offer);
            TheUnitOfWork.SaveChanges();
            return offerDTO;
        }
        //public CreateMakeOfferDTO update(CreateMakeOfferDTO offerDTO)
        //{
        //    var makeOffer = Mapper.Map<MakeOffer>(offerDTO);
        //    TheUnitOfWork.MakeOfferRepo.Update(makeOffer);
        //    TheUnitOfWork.SaveChanges();
        //    return offerDTO;
        //}


        public int CountOfMakeOfferRelatedTo(Expression<Func<MakeOffer, bool>> filter = null)
        {
            return TheUnitOfWork.MakeOfferRepo.CountOfMakeOfferRelatedTo(filter);
        }

        public List<GetMakeOfferWithDoctorInfoDTO> GetSuggestiondoctorsTopRated()
        {
            return Mapper.Map<List<GetMakeOfferWithDoctorInfoDTO>>(TheUnitOfWork.MakeOfferRepo.suggestiondoctorOffersTopRated(12));
        }
    }
}
