using AutoMapper;
using BL.Bases;
using BL.DTOs.ClinicImagesDto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
   public class ClinicImagesAppService:BaseAppService
    {
        public ClinicImagesAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }

        public GetClinicImageDto Get(int id)
        {
            return Mapper.Map<GetClinicImageDto>(TheUnitOfWork.ClinicImagesRepo.GetById(id));
        }
        public IEnumerable<GetClinicImageDto> GetAllWhere(string clinicId)
        {
            return Mapper.Map<IEnumerable<GetClinicImageDto>>(TheUnitOfWork.ClinicImagesRepo.GetWhere(a =>a.ClinicId == clinicId));
        }
        public CreateClinicImagesDto Insert(CreateClinicImagesDto clinicImagesDTO)
        {
            if (clinicImagesDTO == null)
                throw new ArgumentNullException();

            ClinicImage clinicImgs = Mapper.Map<ClinicImage>(clinicImagesDTO);

            TheUnitOfWork.ClinicImagesRepo.Insert(clinicImgs);
            TheUnitOfWork.SaveChanges();
            clinicImagesDTO.Id = clinicImgs.Id;
            return clinicImagesDTO;
            
        }

        public List<CreateClinicImagesDto> InsertList(List<CreateClinicImagesDto> clinicImagesDTOs,string clinicID)
        {
            if (clinicImagesDTOs == null)
                throw new ArgumentNullException();

            for (int i = 0; i < clinicImagesDTOs.Count; i++)
            {
                clinicImagesDTOs[i].ClinicId = clinicID;
            }

            List<ClinicImage> clinicImgs = Mapper.Map<List<ClinicImage>>(clinicImagesDTOs);

            TheUnitOfWork.ClinicImagesRepo.InsertList(clinicImgs);
            TheUnitOfWork.SaveChanges();
            for(int i = 0; i < clinicImgs.Count; i++)
            {
                clinicImagesDTOs[i].Id = clinicImgs[i].Id;
            }
            return clinicImagesDTOs;

        }
        public bool Delete(int id)
        {
            bool result = false;
            TheUnitOfWork.ClinicImagesRepo.Delete(id);
            result = TheUnitOfWork.SaveChanges() > new int();
            return result;
        }
    }
}
