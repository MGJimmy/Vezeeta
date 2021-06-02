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
        public WorkingDay Insert(CreateWorkingDayDTO createWorkingDayDTO)
        {
            WorkingDay workingDay = Mapper.Map<WorkingDay>(createWorkingDayDTO);
            var result = TheUnitOfWork.WorkingDayRepo.Insert(workingDay);
            TheUnitOfWork.SaveChanges();
            return result;
        }
        #endregion
    }
}
