using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.DTOs.ClinicClinicServiceDTO;
using BL.DTOs.ClinicDto;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class ClinicClinicServiceAppService : BaseAppService
    {
        public ClinicClinicServiceAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public void AddOrUpdateClinicClinicService(string clinicId, IEnumerable<ClinicServiceDto> clinicServiceDtos)
        {
            var clinicClinicServices = TheUnitOfWork.ClinicClinicServiceRepo.GetWhere(c => c.ClinicId == clinicId);
            TheUnitOfWork.ClinicClinicServiceRepo.DeleteList(clinicClinicServices);
            if (clinicClinicServices.Count == 0)
            {
                foreach (var clinicService in clinicServiceDtos)
                {
                    var newClinicClinicService = new ClinicClinicService
                    {
                        ClinicId = clinicId,
                        ClinicServiceId = clinicService.ID
                    };
                    TheUnitOfWork.ClinicClinicServiceRepo.Insert(newClinicClinicService);
                }
            }
            else
            {
                bool inserted;
                foreach (var clinicService in clinicServiceDtos) // 1
                {
                    inserted = false;
                    foreach (var clinicClinicService in clinicClinicServices) // 1 2
                    {
                        if (clinicService.ID == clinicClinicService.ClinicServiceId && inserted == false)
                        {
                            TheUnitOfWork.ClinicClinicServiceRepo.Update(clinicClinicService);
                            inserted = true;
                        }
                        if (clinicService.ID != clinicClinicService.ClinicServiceId && inserted == false)
                        {
                            var newClinicClinicService = new ClinicClinicService
                            {
                                ClinicId = clinicId, //c1
                                ClinicServiceId = clinicService.ID // 1
                            };
                            TheUnitOfWork.ClinicClinicServiceRepo.Insert(newClinicClinicService);
                            inserted = true;
                        }
                    }
                }
            }
            TheUnitOfWork.SaveChanges();
        }

        public IEnumerable<ClinicServiceDto> GetClinicServices(string doctorId)
        {
            IEnumerable<Clinicservice> clinicClinicServices = TheUnitOfWork.ClinicClinicServiceRepo.GetClinicServices(doctorId);
            return Mapper.Map<IEnumerable<ClinicServiceDto>>(clinicClinicServices);
        }
    }
}
