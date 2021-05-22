using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class CityAppService:BaseAppService
    {
        public CityAppService(IUnitOfWork theUnitOfWork, IMapper mapper) :base(theUnitOfWork, mapper)
        {

        }
        
        public IEnumerable<CityDTO> GetAllCities()
        {    
            return Mapper.Map<IEnumerable<CityDTO>>(TheUnitOfWork.CityRepo.GetAll());
        }
        public CityDTO GetCity(int id)
        {
            return Mapper.Map<CityDTO>(TheUnitOfWork.CityRepo.GetById(id));
        }
        public bool InsertCity(City city)
        {
            if (city == null)
                throw new ArgumentNullException();

            bool result = false;
            TheUnitOfWork.CityRepo.Insert(city);
            result = TheUnitOfWork.Commit() > new int();
            return result;
        }
        public bool UpdateCity(City city)
        {
            if (city == null)
                throw new ArgumentNullException();

            bool result = false;
            TheUnitOfWork.CityRepo.Update(city);
            result = TheUnitOfWork.Commit() > new int();
            return result;
        }
        public bool DeleteCity(int id)
        {
            bool result = false;
            TheUnitOfWork.CityRepo.Delete(id);
            result = TheUnitOfWork.Commit() > new int();
            return result;
        }
    }
}
