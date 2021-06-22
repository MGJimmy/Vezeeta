using API.helpers;
using BL.AppServices;
using BL.DTOs.OfferRatingDTO;
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
    public class OfferRatingController : ControllerBase
    {
        private GeneralAppService _generalAppService;
        private OfferRatingAppService _offerRatingAppService;
        private IHttpContextAccessor _httpContextAccessor;
        public OfferRatingController(OfferRatingAppService offerRatingAppService, GeneralAppService generalAppService, IHttpContextAccessor httpContextAccessor)
        {
            _offerRatingAppService = offerRatingAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{DoctorOfferId}/{commentnumber}")]
        public IActionResult GetDoctorRate(int DoctorOfferId, int commentnumber)
        {

            try
            {
                return Ok(_offerRatingAppService.GetRatingForDoctorOffer(DoctorOfferId, commentnumber));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }

        }



        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(CreateOfferRatingDto rateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                rateDto.UserId = UserId;
                var result = _offerRatingAppService.Insert(rateDto);
                _generalAppService.CommitTransaction();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(new Response { Message = ex.Message });
            }

        }
    }
}
