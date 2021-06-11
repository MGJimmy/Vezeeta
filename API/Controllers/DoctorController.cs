﻿using API.helpers;
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

        [HttpGet("GetWithClinicForReservetionCart/{name}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetWithClinicForReservetionCart(string name)
        {
            try
            {
                GetDoctorWithClinicForReservetionCartDTO doctor = _doctorAppService.GetWithClinicForReservetionCart(name);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }

        [HttpGet("doctorByName/{doctorName}")]
        public IActionResult GetDoctorWithName(string doctorName)
        {
            try
            {
                GetDoctorDTO doctor = _doctorAppService.GetByName(doctorName);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }



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


        

       
    }
}
