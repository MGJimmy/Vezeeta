using BL.AppServices;
using BL.DTOs.WorkingDayDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingDayController : ControllerBase
    {
        WorkingDayAppService _workingDayAppService;
        GeneralAppService _generalAppService;
        DayShiftAppService _dayShiftAppService;
        public WorkingDayController(
            WorkingDayAppService workingDayAppService, 
            GeneralAppService generalAppService,
            DayShiftAppService dayShiftAppService)
        {
            _workingDayAppService = workingDayAppService;
            _generalAppService = generalAppService;
            _dayShiftAppService = dayShiftAppService;
        }
        [HttpPost]
        public IActionResult Insert(ICollection<CreateWorkingDayDTO> createWorkingDayDTOs)
        {
            try
            {
                foreach (var createWorkingDayDTO in createWorkingDayDTOs)
                {
                    var insertedWorkingDay = _workingDayAppService.Insert(createWorkingDayDTO);
                }
                _generalAppService.CommitTransaction();
                return Ok("added");
            }
            catch
            {
                _generalAppService.RollbackTransaction();
                return BadRequest("Error");
            }
            
        }
        [HttpGet]
        public IActionResult get()
        {

            return Ok(_dayShiftAppService.getDay());
        }
    }
}
















