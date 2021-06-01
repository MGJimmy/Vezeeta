using BL.AppServices;
using BL.DTOs.ClinicImagesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicImageController : ControllerBase
    {
        
        private GeneralAppService _generalAppService;
        private ClinicImagesAppService _clinicImagesAppService;
        public ClinicImageController(GeneralAppService generalAppService, ClinicImagesAppService clinicImagesAppService)
        {
            _generalAppService = generalAppService;
            _clinicImagesAppService = clinicImagesAppService;
        }
        [HttpPost("{clinicId}")]
        public IActionResult Create(List<CreateClinicImagesDto> clinicImgs,string clinicId)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                  List<CreateClinicImagesDto>newsclinicImgs = _clinicImagesAppService.InsertList(clinicImgs, clinicId);
                     _generalAppService.CommitTransaction();
                    return Created("Images added", newsclinicImgs);
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }

        }
    }
}
