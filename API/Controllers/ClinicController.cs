using BL.AppServices;
using BL.DTOs.ClinicDto;
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
    public class ClinicController : ControllerBase
    {
        private ClinicAppService _clinicAppService;
        private GeneralAppService _generalAppService;
        private ClinicImagesAppService _clinicImagesAppService;
        public ClinicController(ClinicAppService clinicAppService, GeneralAppService generalAppService, ClinicImagesAppService clinicImagesAppService)
        {
            _clinicAppService = clinicAppService;
            _generalAppService = generalAppService;
            _clinicImagesAppService = clinicImagesAppService;
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok(_clinicAppService.Get(id));
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
                CreateClinicDto newClinicDTO = _clinicAppService.Insert(clinicDto);
                //string clinicID = newClinicDTO.DoctorId;
                //if (clinicImgs == null)
                //{
                    _generalAppService.CommitTransaction();
                    return Created("clinic created", newClinicDTO);
                //}
                //else
                //{
                //    _clinicImagesAppService.InsertList(clinicImgs, clinicID);
                //    _generalAppService.CommitTransaction();
                //    return Created("clinic & Images created", newClinicDTO);
                //}
                
            }
            catch (Exception ex)
            {
                _generalAppService.RollbackTransaction();
                return BadRequest(ex.Message);
            }

        }
    }
}
