using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.Bases;
using BL.Interfaces;
using BL.DTOs;
using DAL.Models;

namespace BL.AppServices
{
    public class SupSpecializationAppService : BaseAppService
    {
        public SupSpecializationAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }

        public IEnumerable<SupSpecailizationDto> GetAll()
        {
            return Mapper.Map<IEnumerable<SupSpecailizationDto>>(TheUnitOfWork.SupSpecializationRepo.GetAll());
        }

        public IEnumerable<SupSpecailizationDto> GetAllNotAccepted()
        {
            return Mapper.Map<IEnumerable<SupSpecailizationDto>>(TheUnitOfWork.SupSpecializationRepo.GetWhere(s => s.ByAdmin == false));
        }

        public SupSpecailizationDto Get(int id)
        {
            return Mapper.Map<SupSpecailizationDto>(TheUnitOfWork.SupSpecializationRepo.GetById(id));
        }

        public SupSpecailizationDto Insert(SupSpecailizationDto SupSDTO, bool byAdmin)
        {
            if (SupSDTO == null)
                throw new ArgumentNullException();

            SupSpecialization sup_specilize = Mapper.Map<SupSpecialization>(SupSDTO);
            sup_specilize.ByAdmin = byAdmin;
            TheUnitOfWork.SupSpecializationRepo.Insert(sup_specilize);
            TheUnitOfWork.SaveChanges();
            SupSDTO.ID = sup_specilize.ID;
            return SupSDTO;
        }
        
        public bool Update(SupSpecailizationDto S_Specailize)
        {
            if (S_Specailize == null)
                throw new ArgumentNullException();

            bool result = false;
            var SupSpecial = Mapper.Map<SupSpecialization>(S_Specailize);
            TheUnitOfWork.SupSpecializationRepo.Update(SupSpecial);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public bool Delete(int id)
        {
            bool result = false;
            TheUnitOfWork.SupSpecializationRepo.Delete(id);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        public bool CheckExistsByName(SupSpecailizationDto SupSDTO)
        {
            SupSpecialization SupSpecial = Mapper.Map<SupSpecialization>(SupSDTO);
            return TheUnitOfWork.SupSpecializationRepo.CheckExistByName(SupSpecial);
        }
        public IEnumerable<SupSpecailizationDto> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<SupSpecailizationDto>>(TheUnitOfWork.SupSpecializationRepo.GetPageRecords(pageSize, pageNumber));
        }
        public int CountEntity()
        {
            return TheUnitOfWork.SupSpecializationRepo.CountEntity();
        }
        public IEnumerable<SupSpecailizationDto> GetNotAcceptPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<SupSpecailizationDto>>(TheUnitOfWork.SupSpecializationRepo.GetNotAcceptPageRecords(pageSize, pageNumber));
        }
        public int CountOfAccept()
        {
            return TheUnitOfWork.SupSpecializationRepo.CountOfAccept();
        }
        public int CountOfNotAccept()
        {
            return TheUnitOfWork.SupSpecializationRepo.CountOfNotAccept();
        }
    }
}
