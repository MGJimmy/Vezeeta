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
    public class AreaAppService : BaseAppService
    {
        public AreaAppService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public IEnumerable<AreaDTO> GetAll(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<AreaDTO>>(GetPageRecords(pageSize, pageNumber));
        }

        public IEnumerable<AreaDTO> GetAllNotAccepted()
        {
            return Mapper.Map<IEnumerable<AreaDTO>>(TheUnitOfWork.AreaRepo.GetWhere(i => i.ByAdmin == false));
        }


        public AreaDTO GetById(int id)
        {
            return Mapper.Map<AreaDTO>(TheUnitOfWork.AreaRepo.GetById(id));
        }
        public AreaDTO Insert(AreaDTO areaDTO, bool byAdmin)
        {
            if (areaDTO == null)
                throw new ArgumentNullException();

            Area area = Mapper.Map<Area>(areaDTO);
            area.ByAdmin = byAdmin;
            TheUnitOfWork.AreaRepo.Insert(area);
            TheUnitOfWork.SaveChanges();
            areaDTO.ID = area.ID;
            return areaDTO;
        }
        public bool Update(AreaDTO areaDTO)
        {
            if (areaDTO == null)
                throw new ArgumentNullException();

            bool result = false;
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

        public bool CheckExistsByName(AreaDTO areaDTO)
        {
            Area area = Mapper.Map<Area>(areaDTO);
            return TheUnitOfWork.AreaRepo.CheckExistByName(area);
        }

        public IEnumerable<AreaDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<AreaDTO>>(TheUnitOfWork.AreaRepo.GetPageRecords(pageSize, pageNumber));
        }
    }
}
