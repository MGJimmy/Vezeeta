using API.helpers;
using BL.AppServices;
using BL.DTOs.RatingDtos;
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
    public class RatingController : ControllerBase
    {
        private GeneralAppService _generalAppService;
        private RatingAppService _ratingAppService;
        private IHttpContextAccessor _httpContextAccessor;
        public RatingController(RatingAppService ratingAppService, GeneralAppService generalAppService, IHttpContextAccessor httpContextAccessor)
        {
            _ratingAppService = ratingAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        //[HttpGet("{DoctorId}")]
        //public IActionResult GetDoctorRate(String DoctorId) {

        //    try
        //    {
        //        return Ok(_ratingAppService.GetRatingForDoctor(DoctorId));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new Response { Message = ex.Message });
        //    }

        //}
        [HttpGet("{DoctorId}/{commentnumber}")]
        public IActionResult GetDoctorRate(String DoctorId,int commentnumber)
        {

            try
            {
                return Ok(_ratingAppService.GetRatingForDoctor(DoctorId, commentnumber));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }

        }



        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(CreateRatingDto rateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                rateDto.UserId = UserId;
                var result=_ratingAppService.Insert(rateDto);
                _generalAppService.CommitTransaction();
                return Ok(result);


            }
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }
            
        }
    }
}
