using AutoMapper;
using BL.Bases;
using BL.DTOs.ReserveOfferDTO;
using BL.Interfaces;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class ReserveOfferAppService : BaseAppService
    {
        public ReserveOfferAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public List<GetAllReserveOfferToPatientDTO> GetAllReservationToPationt(string userId)
        {
            List<GetAllReserveOfferToPatientDTO> dto = new List<GetAllReserveOfferToPatientDTO>();

            List<ReserveOffer> reservation = TheUnitOfWork.ReserveOfferRepo.GetWhere(i => i.userId == userId).OrderByDescending(i=>i.Date).ToList();

            reservation.ForEach(reserve =>
            {
                var dayShift=TheUnitOfWork.DayShiftRepo.GetById(reserve.dayShiftId);
                string doctorId = reserve.doctorId;
                ApplicationUserIdentity user = TheUnitOfWork.AccountRepo.GetAccountById(doctorId);
                Clinic clinic = TheUnitOfWork.ClinicRepo.GetByIdWithArea(doctorId);
                var makeOffer = TheUnitOfWork.MakeOfferRepo.GetById(reserve.MakeOfferId);


                GetAllReserveOfferToPatientDTO insertDto = new GetAllReserveOfferToPatientDTO();
                insertDto.MakeOfferTitle = makeOffer.Title;
                insertDto.ClinicArea = clinic.Area.Name;
                insertDto.Date = reserve.Date;
                insertDto.ClinicStreeet = clinic.Street;
                insertDto.DoctorName = user.FullName;
                insertDto.ReserveOfferId = reserve.Id;
                insertDto.State = reserve.State;
                insertDto.IsRated = reserve.IsRated;
                insertDto.DayShiftFrom = dayShift.From;
                insertDto.DayShiftTo = dayShift.To;


                dto.Add(insertDto);


            });

            return dto;
        }

        public List<GetAllReserveOfferToDoctorDTO> GetAllReservationToDoctor(string userId)
        {
            List<GetAllReserveOfferToDoctorDTO> dto = Mapper.Map<List<GetAllReserveOfferToDoctorDTO>>(TheUnitOfWork.ReserveOfferRepo.GetWhere(i => i.doctorId == userId).OrderByDescending(i => i.Date).ThenBy(i => i.MakeOfferId)).ToList();
            dto.ForEach(element =>
                {
                    var dayShift = TheUnitOfWork.DayShiftRepo.GetById(element.dayShiftId);
                    element.MakeOfferTitle = TheUnitOfWork.MakeOfferRepo.GetById(element.MakeOfferId).Title;
                    element.DayShiftFrom = dayShift.From;
                    element.DayShiftTo = dayShift.To;
                }
            );

            return dto;
        }

        public ReserveOffer CreateReservation(string userId, CreateReserveOfferDTO createDto)
        {
            createDto.Date = createDto.Date.Date;
            ReserveOffer reservation = Mapper.Map<ReserveOffer>(createDto);
            reservation.State = true;
            reservation.IsRated = false;
            reservation.userId = userId;

            var reseve = TheUnitOfWork.ReserveOfferRepo.Insert(reservation);
            TheUnitOfWork.SaveChanges();
            return reseve;
        }


        public void CancelReservation(int reserveId)
        {
            ReserveOffer reservation = TheUnitOfWork.ReserveOfferRepo.GetById(reserveId);
            reservation.State = false;
            TheUnitOfWork.ReserveOfferRepo.Update(reservation);
            TheUnitOfWork.SaveChanges();
        }



        //public List<string> GetLast3doctorOfferIdsReservedbyPatientforSuggestion(string userId)
        //{


        //    List<string> doctorsIdList = TheUnitOfWork.ReserveOfferRepo.Getlast3doctorOffersIDfromReservationforSuggestion(i => i. == userId).ToList();

        //    if (doctorsIdList == null)
        //    {
        //        return null;
        //    }

        //    return doctorsIdList;
        //}
    }
}
