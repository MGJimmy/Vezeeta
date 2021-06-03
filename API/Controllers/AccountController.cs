using BL.AppServices;
using BL.DTOs.AccountDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountAppService _accountAppService;
        public AccountController(AccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
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
