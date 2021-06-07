using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.DTOs.DoctorDTO;
using BL.DTOs.DoctorSubSpecialization;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class DoctorSubSpecializationAppService: BaseAppService
    {
        public DoctorSubSpecializationAppService(IUnitOfWork theUnitOfWork, IMapper mapper):base(theUnitOfWork,mapper)
        {

        }
        public List<GetDoctorSubSpecialtyDTO> GetSubSpecialtyByDoctorId(string doctorId)
        {
            List<SupSpecialization> doctorSub = TheUnitOfWork.DoctorSubSpecializationRepo.GetSubSpecialtyByDoctorId(doctorId);
            List<GetDoctorSubSpecialtyDTO> subSpecialty = Mapper.Map<List<GetDoctorSubSpecialtyDTO>>(doctorSub);
            return subSpecialty;
        }

        public void create(string doctorId, List<SupSpecailization> subSpecialtiesDto)
        {
            if (subSpecialtiesDto != null)
            {
                List<CreateDoctorSubSpecializationDTO> createListDto = new List<CreateDoctorSubSpecializationDTO>();
                subSpecialtiesDto.ForEach(item =>
                {
                    createListDto.Add(new CreateDoctorSubSpecializationDTO { DoctorId = doctorId, subSpecializeId = item.ID });
                });
                var doctorSubSpecialty = Mapper.Map<List<DoctorSubSpecialization>>(createListDto);
                TheUnitOfWork.DoctorSubSpecializationRepo.InsertList(doctorSubSpecialty);
                TheUnitOfWork.SaveChanges();
            }

        }

        public void UpdateList(string doctorId, List<SupSpecailization> subSpecialtiesDto)
        {
            List<DoctorSubSpecialization> inputFromUserList = new List<DoctorSubSpecialization>();
            subSpecialtiesDto.ForEach(item =>
            {
                inputFromUserList.Add(new DoctorSubSpecialization { DoctorId = doctorId, subSpecializeId = item.ID });
            });
            var InsertListtoDatabase = new List<DoctorSubSpecialization>();
            var indatabase = TheUnitOfWork.DoctorSubSpecializationRepo.GetByDoctorId(doctorId);
            inputFromUserList.ForEach(item =>
            {
                if (indatabase.Find(x => x.DoctorId == item.DoctorId && x.subSpecializeId == item.subSpecializeId) == null)
                {
                    InsertListtoDatabase.Add(new DoctorSubSpecialization { DoctorId = doctorId, subSpecializeId = item.subSpecializeId });
                }
            });
            var deletedListFromDatabase = new List<DoctorSubSpecialization>();
            indatabase.ForEach(item =>
            {
                if (inputFromUserList.Find(x => x.DoctorId == item.DoctorId && x.subSpecializeId == item.subSpecializeId) == null)
                {
                    deletedListFromDatabase.Add(item);
                }
            });

            TheUnitOfWork.DoctorSubSpecializationRepo.InsertList(InsertListtoDatabase);
            TheUnitOfWork.DoctorSubSpecializationRepo.DeleteList(deletedListFromDatabase);
            TheUnitOfWork.SaveChanges();
        }
    }
}
