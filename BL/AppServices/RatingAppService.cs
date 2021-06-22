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

            double AverageRate = getPublicRateForDoctor(DoctorId);

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

            var reserve=TheUnitOfWork.ReservationRepo.GetById(rateDto.ReservationId);

            reserve.IsRated = true;
            TheUnitOfWork.ReservationRepo.Update(reserve);

            var doctor=TheUnitOfWork.DoctorRepo.GetById(reserve.doctorId);
            doctor.CountOfRating++;
            doctor.SumOfRating += rateDto.Rate;
            doctor.AverageRate = doctor.SumOfRating / doctor.CountOfRating;

            TheUnitOfWork.DoctorRepo.Update(doctor);

            rateDto.DoctorId = reserve.doctorId;
            Rating Rate = Mapper.Map<Rating>(rateDto);
       
            TheUnitOfWork.RatingRepo.Insert(Rate);
            TheUnitOfWork.SaveChanges();
        
            return rateDto;
        }

        public double getPublicRateForDoctor(string DoctorId)
        {
            var doctor = TheUnitOfWork.DoctorRepo.GetById(DoctorId);
            return doctor.AverageRate;
        }
    }
}
