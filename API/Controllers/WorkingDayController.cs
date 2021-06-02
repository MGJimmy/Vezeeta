using BL.AppServices;
using BL.DTOs.WorkingDayDTO;
using DAL.Models;
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
                List<CreateWorkingDayDTO> res = new List<CreateWorkingDayDTO>();
                foreach (var createWorkingDayDTO in createWorkingDayDTOs)
                {
                    if(createWorkingDayDTO != null)
                       res.Add(_workingDayAppService.Insert(createWorkingDayDTO));
                }
                _generalAppService.CommitTransaction();
                return Created("added",res);
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
















