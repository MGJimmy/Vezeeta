using API.helpers;
using BL.AppServices;
using BL.DTOs.ReserveOfferDTO;
using DAL.Models;
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
    public class ReserveOfferController : ControllerBase
    {
        ReserveOfferAppService _reserveOfferAppService;
        GeneralAppService _generalAppService;
        IHttpContextAccessor _httpContextAccessor;
        public ReserveOfferController(ReserveOfferAppService reserveOfferAppService, GeneralAppService generalAppService, IHttpContextAccessor httpContextAccessor)
        {
            _reserveOfferAppService = reserveOfferAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("showReserveToPatient")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult showReserveToPatient()
        {
            try
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<GetAllReserveOfferToPatientDTO> Reservetiondto = _reserveOfferAppService.GetAllReservationToPationt(UserId);
                return Ok(Reservetiondto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("showReserveToDoctor")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult showReserveToDoctor()
        {
            try
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var Reservetiondto = _reserveOfferAppService.GetAllReservationToDoctor(UserId);
                return Ok(Reservetiondto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(CreateReserveOfferDTO createDto)
        {
            try
            {
                createDto.Id = 0;
                createDto.State = true;
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                ReserveOffer reservation = _reserveOfferAppService.CreateReservation(UserId, createDto);
                if (reservation != null)
                {
                    _generalAppService.CommitTransaction();
                    return Created("created", reservation.Id);
                }
                else
                {
                    _generalAppService.RollbackTransaction();
                    return BadRequest("no exists recesation to this day");
                }
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{reserveId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult CancelReservation(int reserveId)
        {
            try
            {
                _reserveOfferAppService.CancelReservation(reserveId);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message = "Canceled" });
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }
    }
}
