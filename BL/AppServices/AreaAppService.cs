using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.DTOs.AreaDTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class AreaAppService : BaseAppService
    {
        public AreaAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        
        public IEnumerable<GetAreaDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<GetAreaDTO>>(TheUnitOfWork.AreaRepo.GetAll());
        }
        public IEnumerable<GetAreaDTO> GetAllWhere(int cityId)
        {
            return Mapper.Map<IEnumerable<GetAreaDTO>>(TheUnitOfWork.AreaRepo.GetWhere(a=>a.CityID==cityId && a.ByAdmin==true));
        }
        public IEnumerable<GetAreaWithCityDTO> GetAllWithCity()
        {
            return Mapper.Map<IEnumerable<GetAreaWithCityDTO>>(TheUnitOfWork.AreaRepo.GetAllWithCity());
        }

        public IEnumerable<GetAreaWithCityDTO> GetAllNotAcceptedWithCity()
        {
            return Mapper.Map<IEnumerable<GetAreaWithCityDTO>>(TheUnitOfWork.AreaRepo.GetWhereWithCity(i => i.ByAdmin == false));
        }

        public GetAreaDTO GetById(int id)
        {
            return Mapper.Map<GetAreaDTO>(TheUnitOfWork.AreaRepo.GetById(id));
        }
        public CreateAreaDTO Insert(CreateAreaDTO areaDTO, bool byAdmin)
        {
            if (areaDTO == null)
                throw new ArgumentNullException();

            Area area = Mapper.Map<Area>(areaDTO);
            area.ByAdmin = byAdmin;
            TheUnitOfWork.AreaRepo.Insert(area);
            TheUnitOfWork.SaveChanges();
            areaDTO.ID = area.ID;
            areaDTO.ByAdmin = byAdmin;
            return areaDTO;
        }
        public bool Update(UpdateAreaDTO areaDTO)
        {
            if (areaDTO == null)
                throw new ArgumentNullException();

            bool result = false;
            areaDTO.ByAdmin = true;
            Area area = Mapper.Map<Area>(areaDTO);
            TheUnitOfWork.AreaRepo.Update(area);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        public bool Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id not agree");

            bool result = false;
            TheUnitOfWork.AreaRepo.Delete(id);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public bool CheckExistsByName(string areaName)
        {
            return TheUnitOfWork.AreaRepo.CheckExistByName(areaName);
        }


        
        public IEnumerable<GetAreaWithCityDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<GetAreaWithCityDTO>>(TheUnitOfWork.AreaRepo.GetPageRecords(pageSize, pageNumber));
        }
        public IEnumerable<GetAreaWithCityDTO> GetNotAcceptPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<GetAreaWithCityDTO>>(TheUnitOfWork.AreaRepo.GetNotAcceptPageRecords(pageSize, pageNumber));
        }
        public int CountOfAccept()
        {
            return TheUnitOfWork.AreaRepo.CountOfAccept();
        }
        public int CountOfNotAccept()
        {
            return TheUnitOfWork.AreaRepo.CountOfNotAccept();
        }
    }
}
