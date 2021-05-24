using AutoMapper;
using BL.DTOs;
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

            CreateMap<Area, AreaDTO>()
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
