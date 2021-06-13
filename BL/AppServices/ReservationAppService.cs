﻿using AutoMapper;
using BL.Bases;
using BL.DTOs.ReversationDto;
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
    public class ReservationAppService:BaseAppService
    {
        public ReservationAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        //public void GetAllReservationToDoctor(string doctorId)
        //{
        //    TheUnitOfWork.ReservationRepo.GetWhere(i => i.userId == doctorId);
        //}

        public List<GetAllReservationToPatientDTO> GetAllReservationToPationt(string userId)
        {
            List<GetAllReservationToPatientDTO> dto = new List<GetAllReservationToPatientDTO>();

            List<Reservation> reservation = TheUnitOfWork.ReservationRepo.GetWhere(i => i.userId == userId).ToList();

            reservation.ForEach(reserve =>
            {
                string doctorId = reserve.doctorId;
                ApplicationUserIdentity user = TheUnitOfWork.AccountRepo.GetAccountById(doctorId);
                Clinic clinic = TheUnitOfWork.ClinicRepo.GetByIdWithArea(doctorId);

                GetAllReservationToPatientDTO insertDto = new GetAllReservationToPatientDTO();
                insertDto.reservetionId = reserve.Id;
                insertDto.DoctorName = user.FullName;
                insertDto.Date = reserve.Date;
                insertDto.ClinicStreeet = clinic.Street;
                insertDto.ClinicArea = clinic.Area.Name;
                insertDto.State = reserve.State;

                dto.Add(insertDto);

            });

            return dto;
        }

        public List<GetAllReservationToDoctorDTO> GetAllReservationToDoctor(string userId)
        {
            List<GetAllReservationToDoctorDTO> dto=Mapper.Map<List<GetAllReservationToDoctorDTO>>(TheUnitOfWork.ReservationRepo.GetWhere(i => i.doctorId == userId).OrderBy(i=>i.Date).ThenBy(i=>i.UserName)).ToList();
            return dto;
        }

        public CreateReservationDTO GetToShowInContinuePage(int id)
        {
            Reservation reserve = TheUnitOfWork.ReservationRepo.GetById(id);
            CreateReservationDTO reservationDTO = Mapper.Map<CreateReservationDTO>(reserve);
            return reservationDTO;
        }
        public Reservation CreateReservation(string userId, CreateReservationDTO createDto)
        {
            DayShift dayShift = TheUnitOfWork.DayShiftRepo.GetById(createDto.dayShiftId);

            createDto.Date = createDto.Date.Date;
            Reservation reservation = Mapper.Map<Reservation>(createDto);
            reservation.userId = userId;

            if (CountOfReversationInDate(createDto.dayShiftId, createDto.Date) < dayShift.MaxNumOfReservation)
            {
                var reseve = TheUnitOfWork.ReservationRepo.Insert(reservation);
                TheUnitOfWork.SaveChanges();
                return reseve;
            }
            return null;
        }

        public void updateReservation(CreateReservationDTO createDto)
        {
            
            createDto.Date = createDto.Date.Date;
            Reservation reservation = Mapper.Map<Reservation>(createDto);
            TheUnitOfWork.ReservationRepo.Update(reservation);
            TheUnitOfWork.SaveChanges();
        }

        public void CancelReservation(int reserveId)
        {
            Reservation reservation = TheUnitOfWork.ReservationRepo.GetById(reserveId);
            reservation.State = false;
            TheUnitOfWork.ReservationRepo.Update(reservation);
            TheUnitOfWork.SaveChanges();
        }

        public int CountOfReversationInDate(int dayShiftId,DateTime date) 
        {
            var numberOfReserve = TheUnitOfWork.ReservationRepo.GetWhere(i => i.dayShiftId == dayShiftId && i.Date == date).Count();
            return numberOfReserve;
        }
    }
}