using BL.AppServices;
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
    public class DayShiftController : ControllerBase
    {
        GeneralAppService _generalAppService;
        DayShiftAppService _dayShiftAppService;
        public DayShiftController(GeneralAppService generalAppService,DayShiftAppService dayShiftAppService)
        {
            _generalAppService = generalAppService;
            _dayShiftAppService = dayShiftAppService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                DayShift dayShift = _dayShiftAppService.GetById(id);
                return Ok(dayShift);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

    }
}
