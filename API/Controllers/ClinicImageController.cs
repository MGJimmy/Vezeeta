using BL.AppServices;
using BL.DTOs.ClinicImagesDto;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ClinicImageController : ControllerBase
    {
        
        private GeneralAppService _generalAppService;
        private ClinicImagesAppService _clinicImagesAppService;
        IHttpContextAccessor _httpContextAccessor;
        public ClinicImageController(GeneralAppService generalAppService, IHttpContextAccessor httpContextAccessor, ClinicImagesAppService clinicImagesAppService)
        {
            _generalAppService = generalAppService;
            _clinicImagesAppService = clinicImagesAppService;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpPost]
        public IActionResult Create(List<CreateClinicImagesDto> clinicImgs)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
               
                List<CreateClinicImagesDto>newsclinicImgs = _clinicImagesAppService.InsertList(clinicImgs,DoctorId);
                     _generalAppService.CommitTransaction();
                    return Created("Images added", newsclinicImgs);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetAllImages")]
        public IActionResult GetClinicImages()
        {
            var DoctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            return Ok(_clinicImagesAppService.GetAllWhere(DoctorId));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _clinicImagesAppService.Delete(id);
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
