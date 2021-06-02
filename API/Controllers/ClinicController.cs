using BL.AppServices;
using BL.DTOs.ClinicDto;
using BL.DTOs.ClinicImagesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private ClinicAppService _clinicAppService;
        private GeneralAppService _generalAppService;
        private ClinicImagesAppService _clinicImagesAppService;
        IHttpContextAccessor _httpContextAccessor;
        public ClinicController(ClinicAppService clinicAppService, GeneralAppService generalAppService,
            ClinicImagesAppService clinicImagesAppService, IHttpContextAccessor httpContextAccessor)
        {
            _clinicAppService = clinicAppService;
            _generalAppService = generalAppService;
            _clinicImagesAppService = clinicImagesAppService;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Myclinic")]
        public IActionResult GetById()
        {
            //var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var DoctorId = "8ccd318c-a9e2-407a-b649-bd6f847ccde6";

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
                //var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var DoctorId = "8ccd318c-a9e2-407a-b649-bd6f847ccde6";
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
                //var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                 var DoctorId = "8ccd318c-a9e2-407a-b649-bd6f847ccde6";
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
    }
}
