using API.helpers;
using BL.AppServices;
using BL.DTOs;
using BL.DTOs.Doctor_DoctorServiceDto;
using BL.DTOs.DoctorDTO;
using BL.DTOs.DoctorServiceDtos;
using BL.StaticClasses;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        DoctorAppService _doctorAppService;
        AccountAppService _accountAppService;
        GeneralAppService _generalAppService;
        IHttpContextAccessor _httpContextAccessor;
        Doctor_DoctorServiceAppService _doctor_DoctorServiceAppService;
        public DoctorController(
            DoctorAppService doctorAppService, 
            AccountAppService accountAppService,
            GeneralAppService generalAppService,
             IHttpContextAccessor httpContextAccessor,
             Doctor_DoctorServiceAppService doctor_DoctorServiceAppService)
        {
            _doctorAppService = doctorAppService;
            _accountAppService = accountAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
            _doctor_DoctorServiceAppService = doctor_DoctorServiceAppService;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Doctor doctor = _doctorAppService.GetById(doctorId);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }
        //[HttpGet("subSpecialty")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public IActionResult GetSubSpecialtyOfCurrentDoctor()
        //{
        //    try
        //    {
        //        var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        List<DoctorSubSpecialtyDTO> doctor = _doctorAppService.GetSubSpecialtyByDoctorId(doctorId);
        //        return Ok(doctor);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new Response { Message = ex.Message });
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> RegisterDoctor(CreateDoctorDTO registerDoctorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool isUsernameExist = await _accountAppService.checkUsernameExist(registerDoctorDTO.UserName);
            if (isUsernameExist)
                return BadRequest(new Response { Message = "Username already exist" });
            bool isEmailExist = await _accountAppService.checkEmailExist(registerDoctorDTO.Email);
            if (isEmailExist)
                return BadRequest(new Response { Message = "Email already exist" });
            try
            {
                registerDoctorDTO.IsDoctor = true;
                ApplicationUserIdentity registerUser = await _accountAppService.Register(registerDoctorDTO);
                await _accountAppService.AssignToRole(registerUser.Id, UserRoles.Doctor);
                _doctorAppService.Create(registerUser.Id, registerDoctorDTO);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="Doctor created successfully" });
            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }
            
        }

        
        [HttpPost("addServices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AddServiceForDoctor(List<CreateDoctor_DoctorService> doctorservives)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                _doctor_DoctorServiceAppService.InsertList(doctorservives,DoctorId);
                _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }
        [HttpGet("Myservices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult myServices()
        {
            var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var services = _doctor_DoctorServiceAppService.GetDoctorServices(DoctorId);
            return Ok(services);
        }

        [HttpPut("UpdateDoctorServices")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult UpdateDoctorServices(List<CreateDoctor_DoctorService> NewdoctorservivesList)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _doctor_DoctorServiceAppService.UpdateServicesList(NewdoctorservivesList, DoctorId);
                 _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }


        

        //[HttpPost("assignSpecialty")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> InsertSpecialtyToDoctor(SpecialtyDTO specialtDto)
        //{
        //    try
        //    {
        //        var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        _doctorAppService.InsertSpecialtyToDoctor(doctorId, specialtDto);
        //        _generalAppService.CommitTransaction();
        //        return Ok("created");
        //    }
        //    catch (Exception ex)
        //    {
        //        _generalAppService.RollbackTransaction();
        //        return BadRequest(new Response { Message = ex.Message });
        //    }
        //}
        //[HttpPost("assignSubSpecialty")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //public async Task<IActionResult> InsertSubSpecialtyToDoctor(List<SupSpecailization> subSpecialtDto)
        //{
        //    try
        //    {
        //        var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        _doctorAppService.EmptySubSpecialtyInDoctor(doctorId);
        //        //_generalAppService.CommitTransaction();
        //        _doctorAppService.InsertSubSpecialtyToDoctor(doctorId, subSpecialtDto);
        //        _generalAppService.CommitTransaction();
        //        return Ok(new Response { Message="subspecialty inserted to doctor" });
        //    }
        //    catch (Exception ex)
        //    {
        //        _generalAppService.RollbackTransaction();
        //        return BadRequest(new Response { Message = ex.Message });
        //    }
        //}
    }
}
