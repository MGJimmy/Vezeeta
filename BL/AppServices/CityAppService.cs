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
        #region CRUD
        public IEnumerable<CityDTO> GetAll()
        {    
            return Mapper.Map<IEnumerable<CityDTO>>(TheUnitOfWork.CityRepo.GetAll());
        }
        public CityDTO Get(int id)
        {
            return Mapper.Map<CityDTO>(TheUnitOfWork.CityRepo.GetById(id));
        }
        public CreateCityDTO Insert(CreateCityDTO cityDTO)
        {
            if (cityDTO == null)
                throw new ArgumentNullException();

            City city = Mapper.Map<City>(cityDTO);
            TheUnitOfWork.CityRepo.Insert(city);
            cityDTO.ID = city.ID;
            TheUnitOfWork.SaveChanges();
            return cityDTO;
        }
        public bool Update(City city)
        {
            if (city == null)
                throw new ArgumentNullException();

            bool result = false;
            TheUnitOfWork.CityRepo.Update(city);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            TheUnitOfWork.CityRepo.Delete(id);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        #endregion
    
        #region pagination
        public int CountEntity()
        {
            return TheUnitOfWork.CityRepo.CountEntity();
        }
        public IEnumerable<CityDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<CityDTO>>(TheUnitOfWork.CityRepo.GetPageRecords(pageSize, pageNumber));
        }
        #endregion
        public bool CheckCityExists(City city)
        {
            return TheUnitOfWork.CityRepo.CheckExixt(city);
        }
    }
}
