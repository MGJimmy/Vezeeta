using API.helpers;
using BL.AppServices;
using BL.DTOs.DoctorDTO;
using BL.StaticClasses;
using DAL;
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
    public class DoctorController : ControllerBase
    {
        DoctorAppService _doctorAppService;
        AccountAppService _accountAppService;
        GeneralAppService _generalAppService;
        public DoctorController(
            DoctorAppService doctorAppService, 
            AccountAppService accountAppService,
            GeneralAppService generalAppService)
        {
            _doctorAppService = doctorAppService;
            _accountAppService = accountAppService;
            _generalAppService = generalAppService;

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
    }
}
