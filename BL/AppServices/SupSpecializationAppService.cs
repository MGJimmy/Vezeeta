using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.Bases;
using BL.Interfaces;
using BL.DTOs;
using System.Linq.Expressions;
using DAL.Models;

namespace BL.AppServices
{
    public class SupSpecializationAppService : BaseAppService
    {
        public SupSpecializationAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }

        public IEnumerable<SupSpecailization> GetAll()
        {
            return Mapper.Map<IEnumerable<SupSpecailization>>(TheUnitOfWork.SupSpecializationRepo.GetAll());
        }

        public IEnumerable<SupSpecailization> GetAllNotAccepted()
        {
            return Mapper.Map<IEnumerable<SupSpecailization>>(TheUnitOfWork.SupSpecializationRepo.GetWhere(s => s.ByAdmin == false));
        }

        public SupSpecailization Get(int id)
        {
            return Mapper.Map<SupSpecailization>(TheUnitOfWork.SupSpecializationRepo.GetById(id));
        }
        public IEnumerable<SupSpecailization> GetWhere(Expression<Func<SupSpecialization, bool>> filter)
        {
            return Mapper.Map<IEnumerable<SupSpecailization>>(TheUnitOfWork.SupSpecializationRepo.GetWhere(filter));
        }

        public SupSpecailization Insert(SupSpecailization SupSDTO, bool byAdmin)
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
        public List<SupSpecailization> InsertList(List<SupSpecailization> supSpecailizationsDto,bool byAdmin)
        {
            if (supSpecailizationsDto == null)
                throw new ArgumentNullException();
            
            foreach (var item in supSpecailizationsDto)
            {
                SupSpecialization sup = Mapper.Map<SupSpecialization>(item);
                sup.ByAdmin = byAdmin;
                var insertedItem = TheUnitOfWork.SupSpecializationRepo.Insert(sup);
                TheUnitOfWork.SaveChanges();
                item.ID = insertedItem.ID;
                item.ByAdmin = insertedItem.ByAdmin;
            }
            return supSpecailizationsDto;
        }
        public bool Update(SupSpecailization S_Specailize)
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
        public bool CheckExistsByName(SupSpecailization SupSDTO)
        {
            SupSpecialization SupSpecial = Mapper.Map<SupSpecialization>(SupSDTO);
            return TheUnitOfWork.SupSpecializationRepo.CheckExistByName(SupSpecial);
        }
        public IEnumerable<SupSpecailization> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<SupSpecailization>>(TheUnitOfWork.SupSpecializationRepo.GetPageRecords(pageSize, pageNumber));
        }
        public int CountEntity()
        {
            return TheUnitOfWork.SupSpecializationRepo.CountEntity();
        }
        public IEnumerable<SupSpecailization> GetNotAcceptPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<SupSpecailization>>(TheUnitOfWork.SupSpecializationRepo.GetNotAcceptPageRecords(pageSize, pageNumber));
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
