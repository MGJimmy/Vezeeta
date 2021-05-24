using AutoMapper;
using BL.DTOs;
using BL.DTOs.AreaDTO;
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




        }
    }
}
