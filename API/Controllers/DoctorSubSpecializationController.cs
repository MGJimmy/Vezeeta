using API.helpers;
using BL.AppServices;
using BL.DTOs;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DoctorSubSpecializationController : ControllerBase
    {
        DoctorSubSpecializationAppService _doctorSubSpecialization;
        GeneralAppService _generalAppService;
        IHttpContextAccessor _httpContextAccessor;
        public DoctorSubSpecializationController(DoctorSubSpecializationAppService doctorSubSpecialization, GeneralAppService generalAppService,
            IHttpContextAccessor httpContextAccessor)
        {
            _doctorSubSpecialization = doctorSubSpecialization;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult GetSubSpecialtyByDoctorID()
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var doctorId = "41746536-3759-44da-959e-ffdcab6bf875";
                var x = _doctorSubSpecialization.GetSubSpecialtyByDoctorId(doctorId);
                return Ok(x);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AssignSubSpecialtiesToDoctor(List<SupSpecailization> supSpecailizationsDto)
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var doctorId = "41746536-3759-44da-959e-ffdcab6bf875";
                _doctorSubSpecialization.create(doctorId, supSpecailizationsDto);
                _generalAppService.CommitTransaction();
                return Created("created",null);
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateSubSpecialtiesToDoctor(List<SupSpecailization> supSpecailizationsDto)
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var doctorId = "41746536-3759-44da-959e-ffdcab6bf875";
                _doctorSubSpecialization.UpdateList(doctorId, supSpecailizationsDto);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message= "Updated" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
    }
}
