using AutoMapper;
using BL.Bases;
using BL.DTOs.RatingDtos;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
   public class RatingAppService: BaseAppService
    {
        public RatingAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public GetRatingWithAverageRateDto GetRatingForDoctor(string DoctorId,int commentNumber)
        {
            var rates = TheUnitOfWork.RatingRepo.GetWhereWithPaging(commentNumber, d => d.DoctorId == DoctorId);

            var Allrates = TheUnitOfWork.RatingRepo.GetWhere(d => d.DoctorId == DoctorId);

            int sumRate =0; 
            foreach (var rate in Allrates)
            {
                sumRate += rate.Rate;
            }

            double AverageRate = sumRate / (double)(Allrates.Count);

            GetRatingWithAverageRateDto Avr = new GetRatingWithAverageRateDto
            {
                GetRatingDtos = Mapper.Map<List<GetRatingDto>>(rates),
                AverageRate = AverageRate

            };

            return Avr;
        }

        public CreateRatingDto Insert(CreateRatingDto rateDto)
        {
            if (rateDto == null)
                throw new ArgumentNullException();

            var doctorId=TheUnitOfWork.ReservationRepo.GetById(rateDto.ReservationId).doctorId;
            rateDto.DoctorId = doctorId;
            Rating Rate = Mapper.Map<Rating>(rateDto);
       
            TheUnitOfWork.RatingRepo.Insert(Rate);
            TheUnitOfWork.SaveChanges();
        
            return rateDto;
        }
    }
}
