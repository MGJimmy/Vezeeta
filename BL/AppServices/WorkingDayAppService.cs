using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.DTOs.WorkingDayDTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class WorkingDayAppService:BaseAppService
    {
        public WorkingDayAppService(IUnitOfWork theUnitOfWork, IMapper mapper) :base(theUnitOfWork, mapper)
        {
        }
        #region CRUD
        public CreateWorkingDayDTO Insert(CreateWorkingDayDTO createWorkingDayDTO)
        {
            WorkingDay workingDay = Mapper.Map<WorkingDay>(createWorkingDayDTO);
            var result = TheUnitOfWork.WorkingDayRepo.Insert(workingDay);
            TheUnitOfWork.SaveChanges();
            createWorkingDayDTO.Id = result.Id;
            return createWorkingDayDTO;
        }

        public IEnumerable<GetWorkingDayDTO> GetWorkingDaysForDoctor(string doctorId)
        {
            var workingDays = TheUnitOfWork.WorkingDayRepo.GetWorkingDaysForDoctor(doctorId);
            return Mapper.Map<IEnumerable<GetWorkingDayDTO>>(workingDays);
        }

        public void DeleteList(IEnumerable<GetWorkingDayDTO> workingDaysDTOs)
        {
            var workingDays = Mapper.Map<IEnumerable<WorkingDay>>(workingDaysDTOs);
            TheUnitOfWork.WorkingDayRepo.DeleteList(workingDays);
            TheUnitOfWork.SaveChanges();
        }
        #endregion
    }
}
