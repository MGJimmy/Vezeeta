using API.helpers;
using BL.AppServices;
using BL.DTOs.AccountDTO;
using BL.DTOs.DoctorDTO;
using BL.Interfaces;
using BL.StaticClasses;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private IMailService _mailService;
        private readonly UserManager<ApplicationUserIdentity> _userManager;
        public AccountController(AccountAppService accountAppService, GeneralAppService generalAppService, IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUserIdentity> userManager, IMailService mailService)
        {
            _accountAppService = accountAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
            _mailService = mailService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUserIdentity user = await _accountAppService.GetUserById(userId);
            return Ok(user);
        }

        [HttpGet("getForUpdate")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetCurrentUserForUpdate()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _accountAppService.GetUserByIdForUpdate(userId);
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



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUserIdentity user = await _userManager.FindByIdAsync(userId);
            var result =await _userManager.ChangePasswordAsync(user, model.oldPassword, model.newPassword);
            _generalAppService.CommitTransaction();

            if (!result.Succeeded)
            {
                return Ok("Password cant change");
            }
            return Ok(result);
        }

        [HttpPost("updateUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAccount(UpdateUserDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var result = await _accountAppService.UpdateAccount(userid, model);

                if (result != null)
                {
                    _generalAppService.CommitTransaction();
                    return Ok(model);
                }

                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = " Failed to update" });
            }
            catch (Exception e)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = e.Message });
            }
        }

        [HttpPost("RegisterForAdmin")]
        public async Task<IActionResult> RegisterForAdmin(RegisterAccountDTO registerAccountDTO)
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
                await _accountAppService.AssignToRole(registerUser.Id, UserRoles.Admin);

                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "Account created successfully" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }

        }


        //ForgetPassword
        [HttpPost("ForgetPassword/{email}")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _accountAppService.ForgetPassword(email);

            if (result)
                return Ok(new Response { Message="an email send you to reset password" }); // 200

            return BadRequest(new Response { Message = "your email didn't submit in the websit" }); // 400
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountAppService.ResetPasswordAsync(model);

                if (result)
                {
                    _generalAppService.CommitTransaction();
                    return Ok(new Response { Message = "your passwored has been changed" });
                }

                return BadRequest(new Response { Message = "an error occur" });
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
