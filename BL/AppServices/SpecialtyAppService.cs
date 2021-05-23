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
    public class SpecialtyAppService:BaseAppService
    {
        public SpecialtyAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CRUD
        public IEnumerable<SpecialtyDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<SpecialtyDTO>>(TheUnitOfWork.SpecialtyRepo.GetAll());
        }
        public SpecialtyDTO Get(int id)
        {
            return Mapper.Map<SpecialtyDTO>(TheUnitOfWork.SpecialtyRepo.GetById(id));
        }
        public CreateSpecialtyDTO Insert(CreateSpecialtyDTO createSpecialtyDTO)
        {
            if (createSpecialtyDTO == null)
                throw new ArgumentNullException();

            Specialty specialty = Mapper.Map<Specialty>(createSpecialtyDTO);
            TheUnitOfWork.SpecialtyRepo.Insert(specialty);
            TheUnitOfWork.SaveChanges();
            createSpecialtyDTO.ID = specialty.ID;
            return createSpecialtyDTO;
        }
        public bool Update(UpdateSpecialtyDTO updateSpecialtyDTO)
        {
            if (updateSpecialtyDTO == null)
                throw new ArgumentNullException();

            bool result = false;
            var specialty = Mapper.Map<Specialty>(updateSpecialtyDTO);
            TheUnitOfWork.SpecialtyRepo.Update(specialty);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            TheUnitOfWork.SpecialtyRepo.Delete(id);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        #endregion

        #region pagination
        public int CountEntity()
        {
            return TheUnitOfWork.SpecialtyRepo.CountEntity();
        }
        public IEnumerable<SpecialtyDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<SpecialtyDTO>>(TheUnitOfWork.SpecialtyRepo.GetPageRecords(pageSize, pageNumber));
        }
        #endregion
        public bool CheckCityExists(Specialty specialty)
        {
            return TheUnitOfWork.SpecialtyRepo.CheckExixt(specialty);
        }
    }
}
