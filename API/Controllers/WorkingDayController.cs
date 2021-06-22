using BL.AppServices;
using BL.DTOs.WorkingDayDTO;
using BL.StaticClasses;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = UserRoles.Doctor)]
        public IActionResult Insert(ICollection<CreateWorkingDayDTO> createWorkingDayDTOs)
        {
            try
            {
                string ClinicId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                IEnumerable<GetWorkingDayDTO> workingDaysDTOs = _workingDayAppService.GetWorkingDaysForDoctor(ClinicId);
                _workingDayAppService.DeleteList(workingDaysDTOs);
                List<CreateWorkingDayDTO> res = new List<CreateWorkingDayDTO>();
                foreach (var createWorkingDayDTO in createWorkingDayDTOs)
                {
                    if (createWorkingDayDTO != null)
                    {
                        createWorkingDayDTO.ClinicId = ClinicId;
                        res.Add(_workingDayAppService.Insert(createWorkingDayDTO));
                    }
                }
                _generalAppService.CommitTransaction();
                return Created("added",res);
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public IActionResult GetWorkingDaysForLoggedDoctor()
        {
            string doctorId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<GetWorkingDayDTO> workingDaysDTOs = _workingDayAppService.GetWorkingDaysForDoctor(doctorId);
            return Ok(workingDaysDTOs);
        }

        [HttpGet("byDoctorId/{doctorId}")] 
        public IActionResult GetWorkingDayWithDayShiftForSpecificDoctor(string doctorId)
        {
            try
            {
                IEnumerable<GetWorkingDayDTO> workingDaysDTOs = _workingDayAppService.GetWorkingDaysForDoctor(doctorId);
                return Ok(workingDaysDTOs);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
















