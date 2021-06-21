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
    public class ClinicServicesAppServices : BaseAppService
    {
        public ClinicServicesAppServices(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        #region CRUD
        public IEnumerable<ClinicServiceDto> GetAll()
        {
            return Mapper.Map<IEnumerable<ClinicServiceDto>>(TheUnitOfWork.ClincServicesRepo.GetAll());
        }
        public ClinicServiceDto Get(int id)
        {
            return Mapper.Map<ClinicServiceDto>(TheUnitOfWork.ClincServicesRepo.GetById(id));
        }

        
        public ClinicServiceDto Insert(ClinicServiceDto clinicServiceDto)
        {
            if (clinicServiceDto == null)
                throw new ArgumentNullException();

            Clinicservice clinicservice = Mapper.Map<Clinicservice>(clinicServiceDto);
            TheUnitOfWork.ClincServicesRepo.Insert(clinicservice);
            TheUnitOfWork.SaveChanges();
            clinicServiceDto.ID = clinicservice.ID;
            return clinicServiceDto;
        }

        public IEnumerable<ClinicServiceDto> GetServicesAcceptedByAdmin()
        {
            return Mapper.Map<IEnumerable<ClinicServiceDto>>(TheUnitOfWork.ClincServicesRepo.GetServicesAcceptedByAdmin());
        }

        public bool Update(ClinicServiceDto clinicServiceDto)
        {
            if (clinicServiceDto == null)
                throw new ArgumentNullException();

            bool result = false;
            Clinicservice clinicservice = Mapper.Map<Clinicservice>(clinicServiceDto);
            TheUnitOfWork.ClincServicesRepo.Update(clinicservice);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        } 
        public bool Delete(int id)
        {
            bool result = false;
            TheUnitOfWork.ClincServicesRepo.Delete(id);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        #endregion

        #region pagination
        public int CountEntity()
        {
            return TheUnitOfWork.ClincServicesRepo.CountEntity();
        }
        public IEnumerable<ClinicServiceDto> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<IEnumerable<ClinicServiceDto>>(TheUnitOfWork.ClincServicesRepo.GetPageRecords(pageSize, pageNumber));
        }
        #endregion
        public bool CheckClinicServicesExists(Clinicservice clinicservice)
        {
            return TheUnitOfWork.ClincServicesRepo.CheckExixt(clinicservice);
        }

        public bool CheckClinicServicesExistsByName(string clinicserviceName)
        {
            return TheUnitOfWork.ClincServicesRepo.CheckExixtByName(clinicserviceName);
        }

        public List<ClinicServiceDto> InsertList(List<ClinicServiceDto> clinicServiceDtos)
        {
            List<Clinicservice> clinicservices = Mapper.Map<List<Clinicservice>>(clinicServiceDtos);
            TheUnitOfWork.ClincServicesRepo.InsertList(clinicservices);
            TheUnitOfWork.SaveChanges();
            return Mapper.Map<List<ClinicServiceDto>>(clinicservices);
        }
    }
}
