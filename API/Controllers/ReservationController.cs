using API.helpers;
using BL.AppServices;
using BL.DTOs.ReversationDto;
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
    public class ReservationController : ControllerBase
    {
        ReservationAppService _reservationAppService;
        GeneralAppService _generalAppService;
        IHttpContextAccessor _httpContextAccessor;
        public ReservationController(ReservationAppService reservationAppService, GeneralAppService generalAppService, IHttpContextAccessor httpContextAccessor)
        {
            _reservationAppService = reservationAppService;
            _generalAppService = generalAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetToShowInContinuePage(int id)
        {
            try
            {
                return Ok(_reservationAppService.GetToShowInContinuePage(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("showReserveToPatient")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult showReserveToPatient()
        {
            try
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var UserId = "6cb36d29-d1ec-4f06-a0a4-52e0be861dc1";
                List<GetAllReservationToPatientDTO> Reservetiondto = _reservationAppService.GetAllReservationToPationt(UserId);
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
                var Reservetiondto = _reservationAppService.GetAllReservationToDoctor(UserId);
                return Ok(Reservetiondto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(CreateReservationDTO createDto)
        {
            try
            {
                createDto.Id = 0;
                createDto.State = true;
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Reservation reservation = _reservationAppService.CreateReservation(UserId, createDto);
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
            catch(Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult update(CreateReservationDTO createDto)
        {
            try
            {
                _reservationAppService.updateReservation(createDto);
                _generalAppService.CommitTransaction();
                return Ok(new Response { Message="Updated" });
            }
            catch(Exception ex)
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
                _reservationAppService.CancelReservation(reserveId);
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
