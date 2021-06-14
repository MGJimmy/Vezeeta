using API.helpers;
using BL.AppServices;
using BL.DTOs.AccountDTO;
using BL.DTOs.DoctorDTO;
using BL.StaticClasses;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountAppService _accountAppService;
        GeneralAppService _generalAppService;
        IHttpContextAccessor _httpContextAccessor;
        public AccountController(AccountAppService accountAppService, GeneralAppService generalAppService, IHttpContextAccessor httpContextAccessor)
        {
            _accountAppService = accountAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUserIdentity user = await _accountAppService.GetUserById(userId);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterAccountDTO registerAccountDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool isUsernameExist = await _accountAppService.checkUsernameExist(registerAccountDTO.UserName);
            if (isUsernameExist)
                return BadRequest(new Response { Message = "Username already exist" });
            bool isEmailExist = await _accountAppService.checkEmailExist(registerAccountDTO.Email);
            if (isEmailExist)
                return BadRequest(new Response { Message = "Email already exist" });
            try
            {
                registerAccountDTO.IsDoctor = false;
                ApplicationUserIdentity registerUser = await _accountAppService.Register(registerAccountDTO);
                await _accountAppService.AssignToRole(registerUser.Id, UserRoles.Patient);
              
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "Account created successfully" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }

        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _accountAppService.Find(model.UserName, model.PasswordHash);
            if (user != null)
            {
                dynamic token = await _accountAppService.CreateToken(user);

                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
