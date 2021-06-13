using AutoMapper;
using BL.Bases;
using BL.DTOs.DayShiftDTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class DayShiftAppService: BaseAppService
    {
        public DayShiftAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }
        #region CRUD
        public DayShift Insert(CreateDayShiftDTO createDayShiftDTO)
        {
            DayShift dayShift = Mapper.Map<DayShift>(createDayShiftDTO);
            var result = TheUnitOfWork.DayShiftRepo.Insert(dayShift);
            TheUnitOfWork.SaveChanges();
            return result;
        }
        public CreateDayShiftDTO getDay()
        {
            return Mapper.Map<CreateDayShiftDTO>(TheUnitOfWork.DayShiftRepo.getDay());
        }
        public DayShift GetById(int id)
        {
            return TheUnitOfWork.DayShiftRepo.GetById(id);
        }
        #endregion
    }
}
