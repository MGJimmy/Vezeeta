using AutoMapper;
using BL.Bases;
using BL.DTOs.OfferRatingDTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class OfferRatingAppService : BaseAppService
    {
        public OfferRatingAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public GetOfferRatingWithAverageRateDto GetRatingForDoctorOffer(int DoctorOfferId, int commentNumber)
        {
            var rates = TheUnitOfWork.OfferRatingRepo.GetWhereWithPaging(commentNumber, d => d.MakeOfferId == DoctorOfferId);

            double AverageRate = getPublicRateForDoctorOffer(DoctorOfferId);

            GetOfferRatingWithAverageRateDto Avr = new GetOfferRatingWithAverageRateDto
            {
                GetOfferRatingDtos = Mapper.Map<List<GetOfferRatingDto>>(rates),
                AverageRate = AverageRate

            };

            return Avr;
        }

        public CreateOfferRatingDto Insert(CreateOfferRatingDto rateDto)
        {
            if (rateDto == null)
                throw new ArgumentNullException();

            var ReserveOffer = TheUnitOfWork.ReserveOfferRepo.GetById(rateDto.ReserveOfferId);

            var doctorOffer = TheUnitOfWork.MakeOfferRepo.GetById(ReserveOffer.MakeOfferId);
            doctorOffer.CountOfRating++;
            doctorOffer.SumOfRating += rateDto.Rate;
            doctorOffer.AverageRate = doctorOffer.SumOfRating / doctorOffer.CountOfRating;
            TheUnitOfWork.MakeOfferRepo.Update(doctorOffer);

            ReserveOffer.IsRated = true;
            TheUnitOfWork.ReserveOfferRepo.Update(ReserveOffer);

            rateDto.MakeOfferId = ReserveOffer.MakeOfferId;
            OfferRating Rate = Mapper.Map<OfferRating>(rateDto);

            TheUnitOfWork.OfferRatingRepo.Insert(Rate);
            TheUnitOfWork.SaveChanges();

            return rateDto;
        }

        public double getPublicRateForDoctorOffer(int DoctorOfferId)
        {
            var doctorOffer = TheUnitOfWork.MakeOfferRepo.GetById(DoctorOfferId);
            return doctorOffer.AverageRate;
        }

    }
}
