using AutoMapper;
using BL.DTOs;
using BL.DTOs.AccountDTO;
using BL.DTOs.AreaDTO;
using BL.DTOs.DoctorDTO;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<SupSpecialization, SupSpecailizationDto>()
              .ReverseMap()
              .ForMember(m => m.specialty, m => m.Ignore());

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

            CreateMap<ApplicationUserIdentity, RegisterAccountDTO>()
            .ReverseMap()
            .ForMember(m => m.Doctor, m => m.Ignore());







        }
    }
}
