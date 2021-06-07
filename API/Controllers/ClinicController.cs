﻿using BL.AppServices;
using BL.DTOs.ClinicDto;
using BL.DTOs.ClinicImagesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BL.StaticClasses;
using BL.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ClinicController : ControllerBase
    {
        private ClinicAppService _clinicAppService;
        private GeneralAppService _generalAppService;
        private ClinicImagesAppService _clinicImagesAppService;
        private ClinicClinicServiceAppService _clinicClinicServiceAppService;
        IHttpContextAccessor _httpContextAccessor;
        public ClinicController(ClinicAppService clinicAppService, GeneralAppService generalAppService,
            ClinicImagesAppService clinicImagesAppService,
            ClinicClinicServiceAppService clinicClinicServiceAppService,
            IHttpContextAccessor httpContextAccessor)
        {
            _clinicAppService = clinicAppService;
            _generalAppService = generalAppService;
            _clinicImagesAppService = clinicImagesAppService;
            _httpContextAccessor = httpContextAccessor;
            _clinicClinicServiceAppService = clinicClinicServiceAppService;
        }

        [HttpGet("Myclinic")]
        public IActionResult GetById()
        {
            var DoctorId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(_clinicAppService.GetByStringId(DoctorId));
        }

        [HttpPost]
        public IActionResult Create(CreateClinicDto clinicDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var DoctorId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                CreateClinicDto newClinicDTO = _clinicAppService.Insert(clinicDto, DoctorId);
              
                _generalAppService.CommitTransaction();
                return Created("clinic created", newClinicDTO);
                

            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Update(UpdateClinicDto clinicDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var DoctorId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _clinicAppService.Update(clinicDto,DoctorId);
                _generalAppService.CommitTransaction();
                return NoContent();
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();

                return BadRequest(ex.Message);

            }
        }
        [HttpPost("addClinicServices")]
        public IActionResult addClinicServices(IEnumerable<ClinicServiceDto> clinicServiceDtos)
        {
            //var doctorId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var doctorId = "c3d6e55b-5e5a-42f1-85b6-be9f18cb1941";
            _clinicClinicServiceAppService.AddOrUpdateClinicClinicService(doctorId, clinicServiceDtos);
            _generalAppService.CommitTransaction();
            return Ok("done");
        }

    }
}
