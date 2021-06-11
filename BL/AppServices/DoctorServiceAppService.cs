using AutoMapper;
using BL.Bases;
using BL.DTOs.DoctorDTO;
using BL.DTOs.DoctorServiceDtos;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class DoctorServiceAppService : BaseAppService
    {
        public DoctorServiceAppService(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork,mapper)
        {
        }
        public IEnumerable<DoctorServiceDto> GetAll()
        {
            return Mapper.Map<IEnumerable<DoctorServiceDto>>(TheUnitOfWork.DoctorServiceRepo.GetAll());
        }
     
    

        public DoctorServiceDto GetById(int id)
        {
            return Mapper.Map<DoctorServiceDto>(TheUnitOfWork.DoctorServiceRepo.GetById(id));
        }
       
        public bool Update(DoctorServiceDto doctorServiceDto)
        {
            if (doctorServiceDto == null)
                throw new ArgumentNullException();

            bool result = false;
            doctorServiceDto.ByAdmin = true;
            DoctorService doctorService = Mapper.Map<DoctorService>(doctorServiceDto);
            TheUnitOfWork.DoctorServiceRepo.Update(doctorService);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
        public bool Delete(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id not agree");

            bool result = false;
            TheUnitOfWork.DoctorServiceRepo.Delete(id);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }

        public bool CheckExistsByName(string serviceName)
        {
            return TheUnitOfWork.DoctorServiceRepo.CheckDoctorServiceExistByName(serviceName);
        }
        public DoctorServiceDto Create(DoctorServiceDto doctorServiceDto, bool byAdmin)
        {
            if (doctorServiceDto == null)
                throw new ArgumentNullException();

            DoctorService doctorService = Mapper.Map<DoctorService>(doctorServiceDto);
            doctorService.ByAdmin = byAdmin;
            TheUnitOfWork.DoctorServiceRepo.Insert(doctorService);
            TheUnitOfWork.SaveChanges();
            doctorServiceDto.ID = doctorService.ID;
            doctorServiceDto.ByAdmin = byAdmin;
            return doctorServiceDto;
        }
        public int CountEntity(bool byAdmin)
        {
            return TheUnitOfWork.DoctorServiceRepo.CountEntity(byAdmin);
        }
        public IEnumerable<DoctorServiceDto> GetPageRecords(int pageSize, int pageNumber,bool byAdmin)
        {
            return Mapper.Map<IEnumerable<DoctorServiceDto>>(TheUnitOfWork.DoctorServiceRepo.GetPageRecords(pageSize, pageNumber,byAdmin));
        }

       public void acceptDoctorService(int id)
        {
            TheUnitOfWork.DoctorServiceRepo.acceptDoctorService(id);
            TheUnitOfWork.SaveChanges();
        }
        public void rejectDoctorService(int id)
        {
            TheUnitOfWork.DoctorServiceRepo.rejectDoctorService(id);
            TheUnitOfWork.SaveChanges();
        }

        public List<DoctorServiceDto> InsertList(List<DoctorServiceDto> ServicesDto, bool byAdmin)
        {
            if (ServicesDto == null)
                throw new ArgumentNullException();

            foreach (var item in ServicesDto)
            {
                item.ByAdmin = byAdmin;
            }
           List<DoctorService> doctorServices = Mapper.Map<List<DoctorService>>(ServicesDto);
            
            TheUnitOfWork.DoctorServiceRepo.InsertList(doctorServices);
            TheUnitOfWork.SaveChanges();

            for (int i = 0; i < doctorServices.Count; i++)
            {
                ServicesDto[i].ID = doctorServices[i].ID;
            }
            
            return ServicesDto;
        }

    }
}
