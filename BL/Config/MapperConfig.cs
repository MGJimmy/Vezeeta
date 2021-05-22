using AutoMapper;
using BL.Dtos;
using BL.DTOs;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
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
            CreateMap<Area, AreaDTO>()
                .ReverseMap()
                .ForMember(m => m.City, m => m.Ignore());

        }
    }
}
