﻿using API.helpers;
using BL.AppServices;
using BL.DTOs.MakeOfferDTO;
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
    public class MakeOfferController : ControllerBase
    {
        GeneralAppService _generalAppService;
        MakeOfferAppService _makeOfferAppService;
        IHttpContextAccessor _httpContextAccessor;

        public MakeOfferController(GeneralAppService generalAppService,MakeOfferAppService makeOfferAppService, IHttpContextAccessor httpContextAccessor)
        {
            _generalAppService = generalAppService;
            _makeOfferAppService = makeOfferAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_makeOfferAppService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("relatedToDoctor")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetAllByDoctor()
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(_makeOfferAppService.GetAllByDoctorId(doctorId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_makeOfferAppService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Create(CreateMakeOfferDTO offerDTO)
        {
            try
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var inserted = _makeOfferAppService.create(doctorId, offerDTO);
                _generalAppService.CommitTransaction();
                return Created("created", inserted);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Update(CreateMakeOfferDTO offerDTO)
        {
            try
            {
                _makeOfferAppService.update(offerDTO);
                _generalAppService.CommitTransaction();
                return Ok(new Response{Message="updated"});
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }
        }





    }
}