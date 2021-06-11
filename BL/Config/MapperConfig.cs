﻿using AutoMapper;
using BL.DTOs;
using BL.DTOs.AccountDTO;
using BL.DTOs.AreaDTO;
using BL.DTOs.DayShiftDTO;
using BL.DTOs.DoctorDTO;
using BL.DTOs.ClinicDto;
using BL.DTOs.WorkingDayDTO;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.ClinicImagesDto;
using BL.DTOs.DoctorServiceDtos;
using BL.DTOs.Doctor_DoctorServiceDto;
using BL.DTOs.DoctorSubSpecialization;
using BL.DTOs.ReversationDto;
using BL.DTOs.OfferDto;
using BL.DTOs.SubOfferDto;
using BL.DTOs.UserDto;

namespace BL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        { 
          
            CreateMap<City, CityDTO>()
                .ReverseMap();
            CreateMap<City, CreateCityDTO>()
                .ReverseMap();
            CreateMap<City, UpdateCityDTO>()
                .ReverseMap();

            CreateMap<Area, CreateAreaDTO>()
                .ReverseMap()
                .ForMember(m => m.City, m => m.Ignore());
            CreateMap<Area, UpdateAreaDTO>()
                .ReverseMap()
                .ForMember(m => m.City, m => m.Ignore());
            CreateMap<Area, GetAreaDTO>()
                .ReverseMap()
                .ForMember(m => m.City, m => m.Ignore());
            CreateMap<Area, GetAreaWithCityDTO>()
                .ReverseMap()
                .ForMember(m => m.City, m => m.Ignore());

            CreateMap<Specialty, SpecialtyDTO>()
               .ReverseMap();
            CreateMap<Specialty, CreateSpecialtyDTO>()
              .ReverseMap();
            CreateMap<Specialty, UpdateSpecialtyDTO>()
           .ReverseMap();
            CreateMap<SupSpecialization, SupSpecailization>()
              .ReverseMap();
              //.ForMember(m => m.specialty, m => m.Ignore());

            CreateMap<Clinicservice, ClinicServiceDto>()
                .ReverseMap();

            CreateMap<DoctorAttachment, DoctorAttachmentDto>()
              .ReverseMap();
            CreateMap<DoctorAttachment, DoctorAttachmentGetOneDtO>()
              .ReverseMap();

            CreateMap<Doctor, CreateDoctorDTO>()
            .ReverseMap()
            .ForMember(m => m.User, m => m.Ignore())
            .ForMember(m => m.DoctorAttachment, m => m.Ignore());

            CreateMap<DoctorSubSpecialization, CreateDoctorSubSpecializationDTO>()
                .ReverseMap();
            CreateMap<SupSpecialization, GetDoctorSubSpecialtyDTO>()
                .ReverseMap();



            CreateMap<ApplicationUserIdentity, RegisterAccountDTO>()
            .ReverseMap()
            .ForMember(m => m.Doctor, m => m.Ignore());

            CreateMap<Clinic, CreateClinicDto>()
            .ReverseMap()
            .ForMember(m => m.Area, m => m.Ignore())
            .ForMember(m => m.City, m => m.Ignore())
            .ForMember(m => m.Doctor, m => m.Ignore())
            .ForMember(m => m.ClinicImages, m => m.Ignore());

            CreateMap<Clinic, UpdateClinicDto>()
            .ReverseMap()
            .ForMember(m => m.Area, m => m.Ignore())
            .ForMember(m => m.City, m => m.Ignore())
            .ForMember(m => m.Doctor, m => m.Ignore())
            .ForMember(m => m.ClinicImages, m => m.Ignore());


            CreateMap<Clinic, GetClinicDto>()
           .ReverseMap()
           .ForMember(m => m.Area, m => m.Ignore())
           .ForMember(m => m.City, m => m.Ignore())
           .ForMember(m => m.Doctor, m => m.Ignore())
           .ForMember(m => m.ClinicImages, m => m.Ignore());


            CreateMap<ClinicImage, CreateClinicImagesDto>()
            .ReverseMap()
            .ForMember(m => m.Clinic, m => m.Ignore());
            CreateMap<ClinicImage, GetClinicImageDto>()
           .ReverseMap()
           .ForMember(m => m.Clinic, m => m.Ignore());

            CreateMap<WorkingDay, CreateWorkingDayDTO>()
            .ReverseMap()
            .ForMember(m => m.Clinic, m => m.Ignore());
            CreateMap<WorkingDay, GetWorkingDayDTO>()
            .ReverseMap()
            .ForMember(m => m.Clinic, m => m.Ignore());


            CreateMap<DayShift, CreateDayShiftDTO>()
            .ReverseMap()
            .ForMember(m => m.WorkingDay, m => m.Ignore());


            CreateMap<ApplicationUserIdentity, LoginDto>().ReverseMap();

            CreateMap<DoctorService, DoctorServiceDto>().ReverseMap();

            CreateMap<Doctor_DoctorService,GetDoctor_DoctorService>()
            .ReverseMap()
            .ForMember(m => m.doctor, m => m.Ignore())
            .ForMember(m => m.service, m => m.Ignore());

            CreateMap<Doctor_DoctorService, CreateDoctor_DoctorService>()
            .ReverseMap()
            .ForMember(m => m.doctor, m => m.Ignore())
            .ForMember(m => m.service, m => m.Ignore());

            CreateMap<Reservation, CreateReservationDTO>().ReverseMap();
            
            CreateMap<Doctor, GetDoctorDTO>().ReverseMap();

            CreateMap<Doctor, GetDoctorWithClinicForReservetionCartDTO>().ReverseMap();

            CreateMap<Reservation, GetAllReservationToDoctorDTO>().ReverseMap();

            CreateMap<Offer, OfferDTO>().ReverseMap();
            CreateMap<SubOffer, SubOfferDto>().ReverseMap();
            CreateMap<SubOffer, GetSubOfferWithOfferDto>().ReverseMap();
            CreateMap<Doctor_DoctorService,GetDoctor_DoctorServiceWithService>()
            .ReverseMap()
            .ForMember(m => m.doctor, m => m.Ignore());
           
            CreateMap<CreateDoctor_DoctorService, GetDoctor_DoctorServiceWithService>()
            .ReverseMap();



            CreateMap<Doctor, GetDoctorDto>()
           .ReverseMap()
           .ForMember(m => m.DoctorAttachment, m => m.Ignore())
           .ForMember(m => m.doctor_doctorServices, m => m.Ignore())
           .ForMember(m => m.DoctorSubSpecialization, m => m.Ignore());
           
            
          CreateMap<ApplicationUserIdentity, GetUserDto>()
          .ReverseMap()
          .ForMember(m => m.Doctor, m => m.Ignore());

            


        }
    }
}
